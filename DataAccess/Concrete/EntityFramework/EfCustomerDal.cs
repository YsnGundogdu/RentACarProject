using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace DataAccess.Concrete.EntityFramework
{   
    public class EfCustomerDal : EfEntityRepositoryBase<Customer, RentACarContext>, ICustomerDal
    {    
        public List<CustomerDetailDto> GetCustomerDetails()
        {
            using (RentACarContext context = new RentACarContext())
            {
                var result = from c in context.Customers
                             join u in context.Users on c.UserId equals u.UserId
                             select new CustomerDetailDto()
                             {
                                 CustomerId = c.CustomerId,
                                 CustomerIdentityNumber = c.CustomerIdentityNumber,
                                 CustomerStatus = c.CustomerStatus,
                                 UserEmail = u.UserEmail,
                                 UserFirstName = u.UserFirstName,
                                 UserLastName = u.UserLastName
                             };
                return result.ToList();
            }

        }
    }     
}

