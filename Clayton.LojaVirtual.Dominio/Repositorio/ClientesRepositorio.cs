
using System.Collections.Generic;
using System.Linq;
using Clayton.LojaVirtual.Dominio.Entidade;


namespace Clayton.LojaVirtual.Dominio.Repositorio
{
    public class ClientesRepositorio
    {
        private readonly EfDbContext _context = new EfDbContext();

        public Cliente ObterCliente(string id)
        {
            return _context.Users.Find(id);
        }
    }
}
