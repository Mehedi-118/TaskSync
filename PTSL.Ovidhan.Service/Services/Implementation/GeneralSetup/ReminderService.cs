using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;

using PTSL.Ovidhan.Business.Businesses.Interface.GeneralSetup;
using PTSL.Ovidhan.Common.Entity.GeneralSetup;
using PTSL.Ovidhan.Common.Enum;
using PTSL.Ovidhan.Common.Model.EntityViewModels.GeneralSetup;
using PTSL.Ovidhan.Service.BaseServices;
using PTSL.Ovidhan.Service.Services.Interface.GeneralSetup;

namespace PTSL.GENERIC.Service.Services.Output
{
    public class ReminderService : BaseService<ReminderVM, Reminder>, IReminderService
    {
        private readonly IReminderBusiness _business;
        public IMapper _mapper;

        public ReminderService(IReminderBusiness business, IMapper mapper) : base(business, mapper)
        {
            _business = business;
            _mapper = mapper;
        }

        public override Reminder CastModelToEntity(ReminderVM model)
        {
            return _mapper.Map<Reminder>(model);
        }

        public override ReminderVM CastEntityToModel(Reminder entity)
        {
            return _mapper.Map<ReminderVM>(entity);
        }

        public override IList<ReminderVM> CastEntityToModel(IQueryable<Reminder> entity)
        {
            return _mapper.Map<IList<ReminderVM>>(entity).ToList();
        }

        
    }
}