using System.Linq;
using Core.Entities;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class SpecificationEvaluation<TEntity> where TEntity : BaseEntity
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery,
         ISpecification<TEntity> specification)
        {
            var query = inputQuery;
            //si on fait un get pour une valeur, Criteria contient l'id
            //sinon Criteria est null
            if(specification.Criteria != null)
            {
                query = query.Where(specification.Criteria);
            }
            if(specification.OrderBy != null)
                query = query.OrderBy(specification.OrderBy);
                           //query -->IQueryable<TEntity> Iclude--> méthode de EntityFramework

            if(specification.OrderByDescending != null)
                query = query.OrderByDescending(specification.OrderByDescending);
                           //query -->IQueryable<TEntity> Iclude--> méthode de EntityFramework
            if(specification.PaginationEnable)
                query = query.Skip(specification.Skip).Take(specification.Take);
            query = specification.Includes.Aggregate(query,(current, include)
                //current--> 
                =>current.Include(include));
            return query;    
        }
    }
}