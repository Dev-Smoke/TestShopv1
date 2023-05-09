using Microsoft.EntityFrameworkCore;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading.Tasks;
using TestShopv1.Models;
using TestShopv1.Models.ViewModel;

namespace TestShopv1.Services
{
    public static class Helper
    {


        public static string GetName(this ClaimsPrincipal user)
        {
            return user.Claims.First(x => x.Type == ClaimTypes.Email).Value;
        }


        public static int GetId(this ClaimsPrincipal user)
        {
            return int.Parse(user.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value);
        }
        public static Product PR_Mapping(ProductVM productVM)
        {
            Product product = new()
            {
                Id = productVM.Product.Id,
                Name = productVM.Product.Name,
                UnitPriceNetto = productVM.Product.UnitPriceNetto,
                Description = productVM.Product.Description,
                CategoryId = productVM.Product.CategoryId,
                ManufacturerId = productVM.Product.ManufacturerId,
                ImagePath = productVM.Product.ImagePath
            };
            return product;
        }
   //     public IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter, string? includeProperties = null)
   //     {
   //         IQueryable<T> query = dbSet;
   //         if (filter != null) {
   //             query = query.Where(filter);
   //         }
			//if (!string.IsNullOrEmpty(includeProperties))
   //         {
   //             foreach(var includeProp in includeProperties
   //                 .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
   //             {
   //                 query = query.Include(includeProp);
   //             }
   //         }
   //         return query.ToList();
   //     }

    }
}
