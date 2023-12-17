using PTSL.Ovidhan.Common.Enum;
using Serialize.Linq.Nodes;

namespace PTSL.Ovidhan.Common.QuerySerialize.Interfaces
{
    public interface IFilterOptionsNodes
    {
        public ExpressionNode IncludeExpressionNode { get; set; }
        public ExpressionNode FilterExpressionNode { get; set; }
        public List ListCondition { get; set; }
    }
}