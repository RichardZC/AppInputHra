namespace Hra.App.Models
{
    public interface IConstante
    {
        public string UsaDominio { get; }
        public string Dominio { get; }
        public string Ldap { get; }

    }
    public class Constante : IConstante
    {
        public string UsaDominio { get; set; }
        public string Dominio { get; set; }
        public string Ldap { get; set; }
    }
}
