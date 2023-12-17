using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;

using PTSL.Ovidhan.Business.BaseBusinesses;
using PTSL.Ovidhan.Business.Businesses.Interface.GeneralSetup;
using PTSL.Ovidhan.Common.Entity.GeneralSetup;
using PTSL.Ovidhan.Common.Entity.Tasks;
using PTSL.Ovidhan.Common.Enum;
using PTSL.Ovidhan.Common.QuerySerialize.Implementation;
using PTSL.Ovidhan.DAL.UnitOfWork;

namespace PTSL.Ovidhan.Business.Businesses.Implementation.GeneralSetup
{
    public class ReminderBusiness : BaseBusiness<Reminder>, IReminderBusiness
    {
        private readonly ITodoBusiness _todoBusiness;
        public ReminderBusiness(GENERICUnitOfWork unitOfWork, ITodoBusiness todoBusiness)
            : base(unitOfWork)
        {
            _todoBusiness = todoBusiness;
        }
        public override async Task<(ExecutionState executionState, Reminder entity, string message)> CreateAsync(Reminder entity)
        {
            FilterOptions<Reminder> filterOptions = new FilterOptions<Reminder>()
            {
                FilterExpression = e => e.TodoId == entity.TodoId && e.UserId == entity.UserId

            };
            (ExecutionState executionState, Reminder entity, string message) todo = await base.GetAsync(filterOptions);

            if (todo.executionState == ExecutionState.Retrieved)
            {
                if (todo.entity.RemindAt == entity.RemindAt)
                {
                    return (ExecutionState.Success, todo.entity, "Reminder Already Exists");
                }

                entity.CreatedBy = entity.UserId;
                entity.CreatedAt = DateTime.Now;

                var createNewReminder = await base.CreateAsync(entity);
                if (createNewReminder.executionState == ExecutionState.Created)
                {
                    return (ExecutionState.Success, createNewReminder.entity, createNewReminder.message);
                }
            }

            return (ExecutionState.Failure, null!, todo.message);

        }
    }
}