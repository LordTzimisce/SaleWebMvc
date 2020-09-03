using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Data;
using SalesWebMvc.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMvc.Services
{
    public class DepartmentService
    {
        private readonly SalesWebMvcContext _context;
        public DepartmentService(SalesWebMvcContext context)
        {
            _context = context;
        }

        public async Task<List<Department>> FindAllAsync() 
            //Necessário implementar a assinatura async
            //Implementar o Task
            //Adicionar ao nome do método o prefixo Async que é um padrão da linguagem
        {
            return await _context.Department.OrderBy(x => x.Name).ToListAsync();
            //Mudar o método que é chamado para lista, savechange e etc para a versao async
            //anotar com a palavra await
        }
    }
}
