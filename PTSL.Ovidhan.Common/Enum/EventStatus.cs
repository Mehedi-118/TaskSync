using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTSL.Ovidhan.Common.Enum
{
    public enum EventStatus
    {
        ChapterClosing = 1,
        NonChapterClosing = 2,
        Terminated = 3,

        #region May not be used in db
        Ended = 4,
        Running = 5,
        Upcomming = 6
        #endregion May not be used in db
    }
}
