using PTSL.Ovidhan.Common.Entity;
using PTSL.Ovidhan.Common.Entity.GeneralSetup;
using PTSL.Ovidhan.DAL.Repositories;
using PTSL.Ovidhan.DAL.Repositories.Interface.GeneralSetup;

namespace PTSL.Ovidhan.DAL.Repositories.Implementation.GeneralSetup
{
    public class ReminderRepository : BaseRepository<Reminder>, IReminderRepository
    {
        public ReminderRepository(
            GENERICWriteOnlyCtx writeOnlyCtx,
            GENERICReadOnlyCtx readOnlyCtx)
            : base(writeOnlyCtx, readOnlyCtx) { }
    }
}