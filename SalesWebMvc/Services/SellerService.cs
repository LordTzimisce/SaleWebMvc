using SalesWebMvc.Data;
using SalesWebMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;
using SalesWebMvc.Services.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace SalesWebMvc.Services
{
    public class SellerService
    {
        //ReadOnly serve para restringir o acesso a variável só para leitura
        private readonly SalesWebMvcContext _context;

        public SellerService(SalesWebMvcContext context)
        {
            _context = context;
        }

        public async Task<List<Seller>> FindAllAsync()
        {
            return await _context.Seller.ToListAsync();
        }

        public async Task InsertAsync(Seller seller)
        {
            _context.Add(seller);
            await _context.SaveChangesAsync();
        }

        public async Task<Seller> FindByIdAsync(int id)
        {
            return await _context.Seller.Include(x => x.Department).FirstOrDefaultAsync(obj => obj.Id == id);
        }

        public async Task RemoveAsync(int id)
        {
            try
            {
                var obj = _context.Seller.Find(id);
                _context.Remove(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {

                throw new IntegrityException("It's not possible to delete this user because he/she has salles");
            }
        }

        public async Task UpdateAsync(Seller seller)
        {
            //se não existir seller com id informado, retorna exception
            bool hasAny = await _context.Seller.AnyAsync(x => x.Id == seller.Id);
            if (!hasAny)
            {
                throw new NotFoundException("Seller id not found");
            }

            //tenta atualizar o dado, caso não funcione, chamado a excessao a nivel de servico
            try
            {
                //Atualizar informações de seller
                _context.Update(seller);
                //Salva mudanças - commit
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException e)
            {
                //Chamando exc excessao a nivel de servico
                throw new DbConcurrencyException(e.Message);
            }
        }

    }
}
