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
            //query -->IQueryable<TEntity> Iclude--> méthode de EntityFramework
            query = specification.Includes.Aggregate(query,(current, include)
                //current--> 
                =>current.Include(include));
            return query;    
        }
    }
}