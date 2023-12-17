using PTSL.Ovidhan.Common.Enum;
using PTSL.Ovidhan.Common.QuerySerialize.Interfaces;
using Serialize.Linq.Nodes;

namespace PTSL.Ovidhan.Common.QuerySerialize.Implementation
{
    public class FilterOptionsNodes : BaseSerializeLinq, IFilterOptionsNodes
    {
        public ExpressionNode IncludeExpressionNode { get; set; }
        public ExpressionNode FilterExpressionNode { get; set; }
        public List ListCondition { get; set; }
    }
}