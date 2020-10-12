using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Core.Specifications
{
    public interface ISpecification<T>
    {
        //criteria--> critère get(id==id)
        //Expression reçoit un délégué
        Expression<Func<T, bool>> Criteria{get;}
        List<Expression<Func<T, object>>> Includes{get;}
    }
}