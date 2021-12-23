using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.DirectoryServices;
using System.Security.Claims;

namespace Hra.App.Controllers
{
    public class SeguridadController : Controller
    {
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
            string path = @"LDAP://adhra.hrayacucho.gob.pe";       //CAMBIAR POR VUESTRO PATH (URL DEL SERVICIO DE DIRECTORIO LDAP)
            string dominio = @"adhra";             //CAMBIAR POR VUESTRO DOMINIO
            string usu = pUsuario;                   //USUARIO DEL DOMINIO
            string pass = pClave;                    //PASSWORD DEL USUARIO
            string domUsu = dominio + @"\" + usu;               //CADENA DE DOMINIO + USUARIO A COMPROBAR

            bool permiso = AutenticaUsuario(path, domUsu, pass);
            
            if (permiso)
            {
                List<Claim> claims = new List<Claim> {
                    new Claim(ClaimTypes.NameIdentifier, 1.ToString())
                };
                var identity = new ClaimsIdentity(claims, "Hra");
                var userPrincipal = new ClaimsPrincipal(new[] { identity });

                await HttpContext.SignInAsync(userPrincipal);
            }
            else
            {
                return RedirectToAction("Index", "Seguridad", new { mensaje = "Credenciales Incorrecta!" });
            }

            return RedirectToAction("Index", "Home");
        }
        private bool AutenticaUsuario(String path, String user, String pass)
        {
            //Los datos que hemos pasado los 'convertimos' en una entrada de Active Directory para hacer la consulta
            DirectoryEntry de = new DirectoryEntry(path, user, pass, AuthenticationTypes.Secure);
            try
            {
                //Inicia el chequeo con las credenciales que le hemos pasado
                //Si devuelve algo significa que ha autenticado las credenciales
                DirectorySearcher ds = new DirectorySearcher(de);
                var ccc = ds.FindOne();
                return true;
            }
            catch
            {
                //Si no devuelve nada es que no ha podido autenticar las credenciales
                //ya sea porque no existe el usuario o por que no son correctas
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
