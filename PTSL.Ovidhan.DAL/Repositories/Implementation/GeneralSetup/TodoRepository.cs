using PTSL.Ovidhan.Common.Entity;
using PTSL.Ovidhan.Common.Entity.Tasks;
using PTSL.Ovidhan.DAL.Repositories;
using PTSL.Ovidhan.DAL.Repositories.Interface.GeneralSetup;

namespace PTSL.Ovidhan.DAL.Repositories.Implementation.GeneralSetup
{
    public class TodoRepository : BaseRepository<Todo>, ITodoRepository
    {
        public TodoRepository(
            GENERICWriteOnlyCtx writeOnlyCtx,
            GENERICReadOnlyCtx readOnlyCtx)
            : base(writeOnlyCtx, readOnlyCtx) { }
    }
}