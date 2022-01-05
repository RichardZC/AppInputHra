using Hra.Infraestructure.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.DirectoryServices;
using System.Security.Claims;
using Hra.Transversal.Common;
using Hra.App.Models;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;

namespace Hra.App.Controllers
{
    public class SeguridadController : Controller
    {
        private readonly INPUTContext context;
        private readonly IConstante constante;

        public SeguridadController(INPUTContext context,IConstante constante)
        {
            this.context = context;
            this.constante = constante;
        }

        public IActionResult Index(string mensaje="")
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Mensaje= mensaje;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Autenticar(string pUsuario, string pClave)
        {
            bool permiso;
            if (constante.UsaDominio == "1")
                permiso = AutenticaUsuarioAD(constante.Ldap, constante.Dominio + @"\" + pUsuario, pClave);
            else
                permiso = await AutenticaUsuario(pUsuario, pClave);

            if (permiso)
            {
                List<Claim> claims = new List<Claim> {
                    new Claim(ClaimTypes.NameIdentifier, 1.ToString())
                };
                var identity = new ClaimsIdentity(claims, "Hra");
                var userPrincipal = new ClaimsPrincipal(new[] { identity });

                await HttpContext.SignInAsync(userPrincipal);

                //var str = JsonConvert.SerializeObject(obj);
                //HttpContext.Session.SetString(key, str);

            }
            else
            {
                return RedirectToAction("Index", "Seguridad", new { mensaje = "Credenciales Incorrecta!" });
            }

            return RedirectToAction("Index", "Home");
        }
        private async Task<bool> AutenticaUsuario(String user, String pass)
        {            
            var param1 = new SqlParameter("@Usuario", user);
            var param2 = new SqlParameter("@Clave", pass);
            var result = await context.UspAutenticarUsuario.FromSqlRaw("exec Maestro.uspAutenticarUsuario @Usuario", param1, param2).ToListAsync();
            return result[0].Permiso;
        }
        private bool AutenticaUsuarioAD(String path, String user, String pass)
        {
            DirectoryEntry de = new DirectoryEntry(path, user, pass, AuthenticationTypes.Secure);
            try
            {
                DirectorySearcher ds = new DirectorySearcher(de);
                var ccc = ds.FindOne();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<IActionResult> Logout()
        {
            if (User.Identity.IsAuthenticated)
            {
                await HttpContext.SignOutAsync();
            }
            return RedirectToAction("Index", "Seguridad");
        }
    }
}
