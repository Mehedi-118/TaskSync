﻿using Microsoft.EntityFrameworkCore.Query;
using PTSL.Ovidhan.Common.Enum;
using PTSL.Ovidhan.Common.QuerySerialize.Implementation;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace PTSL.Ovidhan.Common.QuerySerialize.Interfaces
{
    public interface ICountOptions<TEntity>
    {
        public Expression<Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>> IncludeExpression { get; set; }
        public Expression<Func<TEntity, bool>> FilterExpression { get; set; }
        public List ListCondition { get; set; }

        //CountOptions<TEntity> FromCountOptionsNodes (CountOptionsNodes nodes);
    }
}