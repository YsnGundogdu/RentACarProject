using Business.Abstract;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.JWT;
using Core.Utilities.Securtiy.Hashing;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private IUserService _userService;
        private ITokenHelper _tokenHelper;

        public AuthManager(IUserService userService, ITokenHelper tokenHelper)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
        }

        public IDataResult<User> Register(UserForRegisterDto userForRegisterDto, string password)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);
            var user = new User
            {
                UserEmail = userForRegisterDto.UserEmail,
                UserFirstName = userForRegisterDto.UserFirstName,
                UserLastName = userForRegisterDto.UserLastName,
                UserPasswordHash = passwordHash,
                UserPasswordSalt = passwordSalt,
                UserStatus = true
            };
            _userService.Add(user);
            return new SuccessDataResult<User>(user, "Kayıt olundu");
        }

        public IDataResult<User> Login(UserForLoginDto userForLoginDto)
        {
            var userToCheck = _userService.GetByMail(userForLoginDto.UserEmail);
            if (userToCheck.Data == null)
            {
                return new ErrorDataResult<User>("Bu mail adresine ait kullanıcı bulunamadı");
            }

            if (!HashingHelper.VerifyPasswordHash(userForLoginDto.UserPassword, userToCheck.Data.UserPasswordHash, userToCheck.Data.UserPasswordSalt))
            {
                return new ErrorDataResult<User>("Girilen parola hatalı");
            }

            return new SuccessDataResult<User>(userToCheck.Data, "Başarı giriş yapıldı");
        }

        public IResult UserExists(string email)
        {
            if (_userService.GetByMail(email).Data != null)
            {
                return new ErrorResult("Kullanıcı mevcut");
            }
            return new SuccessResult();
        }

        public IDataResult<AccessToken> CreateAccessToken(User user)
        {
            var claims = _userService.GetClaims(user);
            var accessToken = _tokenHelper.CreateToken(user, claims.Data);
            return new SuccessDataResult<AccessToken>(accessToken, "Token oluşturuldu");
        }
    }
}
