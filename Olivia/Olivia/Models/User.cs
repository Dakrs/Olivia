using System;
namespace Olivia.Models
{
    public class Utilizador
    {
        public Utilizador(int id,string u,string p,string e,int ty,int pre,string nome)
        {
            this.Id_utilizador = id;
            this.Username = u;
            this.Password = p;
            this.Email = e;
            this.Type = ty;
            this.Preferencia = pre;
            this.Nome = nome;
        }

        public int Id_utilizador { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int  Type { get; set; }
        public int Preferencia { get; set; }
        public string Nome { get; set; }
    }
}
