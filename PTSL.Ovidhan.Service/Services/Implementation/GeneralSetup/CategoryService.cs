using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;

using Microsoft.AspNetCore.Http;

using Org.BouncyCastle.Crypto.Modes;

using PTSL.Ovidhan.Business.Businesses.Interface.GeneralSetup;
using PTSL.Ovidhan.Common.Entity.GeneralSetup;
using PTSL.Ovidhan.Common.Enum;
using PTSL.Ovidhan.Common.Model.EntityViewModels.GeneralSetup;
using PTSL.Ovidhan.Common.QuerySerialize.Implementation;
using PTSL.Ovidhan.Service.BaseServices;
using PTSL.Ovidhan.Service.Services.Interface.GeneralSetup;

namespace PTSL.Ovidhan.Service.Services.Implementation.GeneralSetup
{
    public class CategoryService : BaseService<CategoryVM, Category>, ICategoryService
    {
        private readonly ICategoryBusiness _business;
        public IMapper _mapper;

        public CategoryService(ICategoryBusiness business, IMapper mapper) : base(business, mapper)
        {
            _business = business;
            _mapper = mapper;
        }

        public override Category CastModelToEntity(CategoryVM model)
        {
            return _mapper.Map<Category>(model);
        }

        public override CategoryVM CastEntityToModel(Category entity)
        {
            return _mapper.Map<CategoryVM>(entity);
        }

        public override IList<CategoryVM> CastEntityToModel(IQueryable<Category> entity)
        {
            return _mapper.Map<IList<CategoryVM>>(entity).ToList();
        }

        public async Task<(ExecutionState executionState, IList<CategoryVM> entity, string message)> CreateInitialCategoryAsync(string userId)
        {
            if (userId == null)
            {
                return (executionState: ExecutionState.Failure, entity: null!, message: "User null");
            }
            (ExecutionState executionState, IList<CategoryVM> entity, string message) returnResponse;

            try
            {
                List<CategoryVM> initialCategory = new List<CategoryVM>
                {
                    new CategoryVM
                    {
                        TitleEn="Work",
                        TitleBn="কাজ",
                        DescriptionEn="Save Work Related Task",
                        DescriptionBn="",
                        UserId=userId,
                        CreatedBy=userId,
                        CreatedAt=DateTime.Now
                    },
                    new CategoryVM
                    {
                        TitleEn="Shopping",
                        TitleBn="",
                        DescriptionEn="Save Shopping Related Task",
                        DescriptionBn="",
                        UserId=userId,
                        CreatedBy=userId,
                        CreatedAt=DateTime.Now
                    },
                    new CategoryVM
                    {TitleEn = "Personal", TitleBn = "", DescriptionEn = "Save Personal Task", DescriptionBn = "", UserId = userId, CreatedBy = userId, CreatedAt = DateTime.Now},
                    new CategoryVM
                    {TitleEn = "Health", TitleBn = "", DescriptionEn = "Save Personal Task", DescriptionBn = "", UserId = userId, CreatedBy = userId, CreatedAt = DateTime.Now}
                }

                    ;
                IList<Category> entity = CastModelToEntity(initialCategory);
                //entity.CreatedBy = 1;
                (ExecutionState executionState, IList<Category> entity, string message) createEntity = await Business.CreateAsync(entity);

                if (createEntity.executionState == ExecutionState.Created)
                {
                    returnResponse = (executionState: createEntity.executionState, entity: CastEntityToModel(createEntity.entity), message: createEntity.message);
                }
                else
                {
                    returnResponse = (executionState: createEntity.executionState, entity: null!, message: createEntity.message);
                }
            }
            catch (Exception ex)
            {
                returnResponse = (executionState: ExecutionState.Failure, entity: null!, message: ex.Message);
            }

            return returnResponse;


        }


        public async Task<(ExecutionState executionState, IList<CategoryVM> entity, string message)> List(string userId, QueryOptions<Category> queryOptions = null)
        {
            try
            {
                var categoryByUser = await _business.List(userId, queryOptions);
                if (categoryByUser.executionState == ExecutionState.Retrieved)
                {
                    IList<CategoryVM> a = CastEntityToModel(categoryByUser.entity);
                    return (ExecutionState.Retrieved, CastEntityToModel(categoryByUser.entity), message: categoryByUser.message);
                }
                return (ExecutionState.Failure, null!, message: categoryByUser.message);

            }
            catch (Exception e)
            {
                return (ExecutionState.Failure, null, null);
            }
        }
    }
}