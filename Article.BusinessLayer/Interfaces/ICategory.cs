using Article.Dto.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Article.BusinessLayer.Interfaces
{
    public interface ICategory
    {
        List<CategoryDto> GetCategories();
        CategoryDto GetCategoryDetailByCategoryId(int categoryId);
        void Insert(CategoryDto category);
        void Update(CategoryDto category);
        void Delete(int Id);
    }
}
