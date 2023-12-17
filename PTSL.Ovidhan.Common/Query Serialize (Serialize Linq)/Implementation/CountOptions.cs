using Microsoft.EntityFrameworkCore.Query;
using PTSL.Ovidhan.Common.Enum;
using PTSL.Ovidhan.Common.QuerySerialize.Interfaces;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace PTSL.Ovidhan.Common.QuerySerialize.Implementation
{
    public class CountOptions<TEntity> : ICountOptions<TEntity>
    {
        public Expression<Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>> IncludeExpression { get; set; }
        public Expression<Func<TEntity, bool>> FilterExpression { get; set; }
        public List ListCondition { get; set; }


        public static CountOptions<TEntity> FromCountOptionsNodes(CountOptionsNodes nodes)
        {
            if (nodes == null)
            {
                return null;
            }

            CountOptions<TEntity> countOptions = new CountOptions<TEntity>();

            if (nodes.IncludeExpressionNode != null)
            {
                countOptions.IncludeExpression =
                    nodes.IncludeExpressionNode.ToExpression<Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>>();
            }

            if (nodes.FilterExpressionNode != null)
            {
                countOptions.FilterExpression =
                    nodes.FilterExpressionNode.ToBooleanExpression<TEntity>();
            }

            countOptions.ListCondition = (List)nodes?.ListCondition;

            return countOptions;
        }
    }
}