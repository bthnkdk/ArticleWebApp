using Article.BusinessLayer.Interfaces;
using Article.Core.Entities;
using Article.DataAccess.Repository;
using Article.DataAccess.UnitOfWork;
using Article.Dto.Entity;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Article.BusinessLayer.Services
{
    public class CategoryService : ICategory
    {
        private readonly IRepository<Category> _categoryRepository;
        private readonly IUnitOfWork _uow;

        public CategoryService(IUnitOfWork uow)
        {
            _uow = uow;
            _categoryRepository = uow.GetRepository<Category>();

        }

        public List<CategoryDto> GetCategories()
        {
            var result = (from c in _categoryRepository.GetAll()
                          select new CategoryDto
                          {
                              Id = c.Id,
                              Name = c.Name,
                              Color = c.Color,
                              Icon = c.Icon
                          }).ToList();
            return result;
        }
        public CategoryDto GetCategoryDetailByCategoryId(int categoryId)
        {
            var result = (from c in _categoryRepository.GetAll()
                          where c.Id == categoryId
                          select new CategoryDto
                          {
                              Color = c.Color,
                              Id = c.Id,
                              Name = c.Name,
                              Icon = c.Icon
                          }).SingleOrDefault();
            return result;
        }

        public void Insert(CategoryDto category)
        {
            var categoryEntity = Mapper.DynamicMap<Category>(category);
            _categoryRepository.Insert(categoryEntity);
        }

        public void Update(CategoryDto category)
        {
            var categoryEntity = _categoryRepository.Find(category.Id);
            Mapper.DynamicMap(category, categoryEntity);
            _categoryRepository.Update(categoryEntity);
        }
        public void Delete(int Id)
        {
            var categoryEntity = _categoryRepository.Find(Id);
            _categoryRepository.Delete(categoryEntity);
        }
    }
}
