using AutoMapper;
using MVC_Watch_Business.DTO.CategoryDTO;
using MVC_Watch_Data.Contracts;
using MVC_Watch_Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Watch_Business.Services
{
	public class CategoryService
	{
		private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CategoryDTO>> GetAllCategoriesAsync(Expression<Func<Category, bool>>? filter = null)
        {
            try
            {
				var categoriesDomain = await _unitOfWork.Category.GetAllAsync(filter);
				var categories = _mapper.Map<IEnumerable<CategoryDTO>>(categoriesDomain);
				return categories;
			}
            catch (Exception)
            {

                throw;
            }
            
        }
        public async Task<CategoryDTO> GetCategoryAsync(Expression<Func<Category, bool>> filter)
        {
            try
            {
                var categoryDomain = await  _unitOfWork.Category.GetFirstOrDefaultAsync(filter);
                var category = _mapper.Map<CategoryDTO>(categoryDomain);
                return category;
			} catch (Exception) 
            {
                throw;
            }          
        }

        public void AddCategory(AddCateDTO category)
        {
            try
            {
                var categoryDomain = _mapper.Map<Category>(category);
                _unitOfWork.Category.Add(categoryDomain);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void UpdateCategory(UpdateCateDTO category)
        {
            try
            {
                var categoryDomain = _mapper.Map<Category>(category);
                _unitOfWork.Category.Update(categoryDomain);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void DeleteCategory(CategoryDTO category)
        {
            try
            {
                var categoryDomain = _mapper.Map<Category>(category);
                _unitOfWork.Category.Delete(categoryDomain);
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
