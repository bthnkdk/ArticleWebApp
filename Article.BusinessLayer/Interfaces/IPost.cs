using Article.Dto.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Article.BusinessLayer.Interfaces
{
    public interface IPost
    {
        void Update(PostDto post);
        void Insert(PostDto post);
        List<GetPostbyUserIdDto> GetPostsByFilterParams(int userId, int pageNumber, string title, int? categoryId); //kullanıcı id'sine göre makale listesi
        PostDto GetPostDetailsByPostId(int postId); //post id'sine göre detay getir. 
        string GetSlugAnyPost(string slug); //veritabanında aynı isimde url kodu var mı?
        void Delete(int id);
        int GetPostCount();
        bool AnyPostByCategoryId(int categoryId);// kategori numarasına göre makale var mı ?
        List<PostUserDto> GetPostAll(int? categoryId,int pageNumber); //tüm makaleleri getir
        PostDetailDto GetPostDetail(int id); //url koduna makale getir 
        byte[] GetPostImageById(int Id);
        void UpdatePageCount(int id);
    }
}
