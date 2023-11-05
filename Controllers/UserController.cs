using Examen.Models.DAO;
using Examen.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Examen.Controllers
{
    public class UserController : Controller
    {
        private UserDAO userDAO = new UserDAO();
        // GET: User
        public ActionResult Index()
        {
            return View(userDAO.ReadUsers());
        }

        // GET: User/Details/5
        public ActionResult Details(int id)
        {
            return View(userDAO.ReadUser(id));
        }

        // GET: User/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        [HttpPost]
        public ActionResult Create(UserDAO user)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(userDAO.CreateUser(user));
            }
            catch
            {
                return View();
            }
        }

        // GET: User/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: User/Edit/5
        [HttpPost]
        public ActionResult Edit(UserDTO user)
        {
            if (ModelState.IsValid)
            {
                string result = userDAO.UpdateUser(user.Id, user); // Actualiza el usuario en la base de datos.
                if (result == "Success")
                {
                    return RedirectToAction("Index"); // Redirige a la página principal después de la actualización exitosa.
                }
                else
                {
                    ModelState.AddModelError("", "Error al actualizar el usuario."); // Agrega un error al modelo si falla la actualización.
                    return View(user); // Vuelve a mostrar la vista de edición con el mensaje de error.
                }
            }
            else
            {
                return View(user);
            }
        }

        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: User/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, UserDTO user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Validar que el `id` y `user` se pasen correctamente.

                    UserDTO existingUser = userDAO.ReadUser(id);

                    if (existingUser != null)
                    {
                        // Verificar que los datos del usuario en `user` coincidan con los datos existentes antes de eliminarlo.
                        if (existingUser.Name == user.Name && existingUser.Email == user.Email)
                        {
                            string result = userDAO.DeleteUser(id);
                            if (result == "Success")
                            {
                                return RedirectToAction("Index");
                            }
                            else
                            {
                                ModelState.AddModelError("", "Error al eliminar el usuario.");
                                return View(user);
                            }
                        }
                        else
                        {
                            ModelState.AddModelError("", "Los datos del usuario no coinciden con los existentes.");
                            return View(user);
                        }
                    }
                    else
                    {
                        return HttpNotFound(); // Devuelve un error 404 si el usuario no se encuentra.
                    }
                }
                else
                {
                    return View(user);
                }
            }
            catch
            {
                return View();
            }
        }
    }
}
