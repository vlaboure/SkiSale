using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Core.Specifications
{
    public class BaseSpecification<T> : ISpecification<T>
    {
        public BaseSpecification()
        {
        }

        public BaseSpecification(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }

        public Expression<Func<T, bool>> Criteria{get;}

        public List<Expression<Func<T, object>>> Includes{get;} = 
        //valeur par defaut
            new List<Expression<Func<T, object>>>();

        public Expression<Func<T, object>> OrderBy {get;private set;}

        public Expression<Func<T, object>> OrderByDescending {get;private set;}
       
        public int Take {get; private set;}

        public int Skip {get; private set;}

        public bool PaginationEnable {get; private set;}

        // ajout de critères dans la liste de requetes
        protected void AddInclude ( Expression<Func<T, object>> includeExpression)
        {
            Includes.Add(includeExpression);
        }
        //ajouté dans ProductBrandAndTypeSpecification
        protected void AddOrderBy(Expression<Func<T, object>> orderByExperession)
        {
            OrderBy = orderByExperession;
        }
        //ajouté dans ProductBrandAndTypeSpecification
        protected void AddOrderByDescending(Expression<Func<T, object>> orderByExperession)
        {
            OrderByDescending = orderByExperession;
        }
        protected void ApplyPagination(int take, int skip)
        {
            Take = take;
            Skip = skip;
            PaginationEnable = true;
        }
    }
}