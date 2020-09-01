using System;
using System.Collections.Generic;
using System.Linq;

namespace PaginacaoEntityFrameworkCore
{
    public class PagedResult<TModel> : PagedResultBase where TModel : class
    {
        public IList<TModel> Results { get; set; }
        public PagedResult()
        {
            Results = new List<TModel>();
        }
    }
    public static class GetPagedExt
    {
        public static PagedResult<TModel> GetPaged<TModel>(this IQueryable<TModel> models, int currentPage, int pageSize) where TModel : class
        {
            var result = new PagedResult<TModel>
            {
                CurrentPage = currentPage,
                PageSize = pageSize,
                RowCount = models.Count()
            };
            var pageCount = (double)result.RowCount / pageSize;
            result.PageCount = (int)Math.Ceiling(pageCount);
            var skip = (currentPage - 1) - pageSize;
            result.Results = models.Skip(skip).Take(pageSize).ToList();
            return result;
        }
    }
}
