using PTSL.Ovidhan.Common.Enum;
using PTSL.Ovidhan.Common.QuerySerialize.Interfaces;
using Serialize.Linq.Nodes;

namespace PTSL.Ovidhan.Common.QuerySerialize.Implementation
{
    public class QueryOptionsNodes : BaseSerializeLinq, IQueryOptionsNodes
    {
        public ExpressionNode IncludeExpressionNode { get; set; }
        public ExpressionNode FilterExpressionNode { get; set; }
        public ExpressionNode SortingExpressionNode { get; set; }
        public Pagination Pagination { get; set; }
        public List ListCondition { get; set; }
    }
}