using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    public interface IController<T>
    {
        IActionResult Add(T entity);
        IActionResult Delete(T entity);
        IActionResult Update(T entity);
        IActionResult GetAll();
        IActionResult GetById(int id);

    }
}
