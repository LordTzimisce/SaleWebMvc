using SalesWebMvc.Data;
using SalesWebMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;
using SalesWebMvc.Services.Exceptions;
using Microsoft.AspNetCore.Mvc;

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

        public List<Seller> FindAll()
        {
            return _context.Seller.ToList();
        }

        public void Insert(Seller seller)
        {
            _context.Add(seller);
            _context.SaveChanges();
        }

        public Seller FindById(int id)
        {
            return _context.Seller.Include(x => x.Department).FirstOrDefault(obj => obj.Id == id);
        }

        public void Remove(int id)
        {
            var obj = _context.Seller.Find(id);
            _context.Remove(obj);
            _context.SaveChanges();
        }

        public void Update(Seller seller)
        {
            //se não existir seller com id informado, retorna exception
            if (!_context.Seller.Any(x => x.Id == seller.Id))
            {
                throw new NotFoundException("Seller id not found");
            }

            //tenta atualizar o dado, caso não funcione, chamado a excessao a nivel de servico
            try
            {
                //Atualizar informações de seller
                _context.Update(seller);
                //Salva mudanças - commit
                _context.SaveChanges();
            }
            catch(DbUpdateConcurrencyException e)
            {
                //Chamando exc excessao a nivel de servico
                throw new DbConcurrencyException(e.Message);
            }
        }

    }
}
