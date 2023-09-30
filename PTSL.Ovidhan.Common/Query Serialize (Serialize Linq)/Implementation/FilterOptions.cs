using Microsoft.EntityFrameworkCore.Query;
using PTSL.Ovidhan.Common.Enum;
using PTSL.Ovidhan.Common.QuerySerialize.Interfaces;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace PTSL.Ovidhan.Common.QuerySerialize.Implementation
{
    public class FilterOptions<TEntity> : IFilterOptions<TEntity>
    {
        public Expression<Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>> IncludeExpression { get; set; }
        public Expression<Func<TEntity, bool>> FilterExpression { get; set; }
        public List ListCondition { get; set; }


        public static FilterOptions<TEntity> FromFilterOptionsNodes(FilterOptionsNodes nodes)
        {
            if (nodes == null)
            {
                return null;
            }

            FilterOptions<TEntity> filterOptions = new FilterOptions<TEntity>();

            if (nodes.IncludeExpressionNode != null)
            {
                filterOptions.IncludeExpression =
                    nodes.IncludeExpressionNode.ToExpression<Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>>();
            }

            if (nodes.FilterExpressionNode != null)
            {
                filterOptions.FilterExpression =
                    nodes.FilterExpressionNode.ToBooleanExpression<TEntity>();
            }

            filterOptions.ListCondition = (List)nodes?.ListCondition;

            return filterOptions;
        }
    }
}