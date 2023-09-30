using System;
using System.Collections.Generic;
using System.Text;

namespace PTSL.Ovidhan.Common.Enum
{
    public enum ExecutionState
    {
        Failure = 0,
        Success = 10,
        Created = 20,
        Retrieved = 30,
        Updated = 40,
        Activated = 41,
        Inactivated = 42,
        Deleted = 50,
    }
}
