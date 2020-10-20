using System.Collections.Generic;

namespace Api.Helpers
{
         // comme la liste est de type generique la classe doit être générique
    public class Pagination<T> where T : class
    {
        public Pagination(int pageIndex, int itemPerPage, int count, IReadOnlyList<T> datas)
        {
            PageIndex = pageIndex;
            ItemPerPage = itemPerPage;
            Count = count;// nb total d'items
            Datas = datas;
        }

        public int PageIndex { get; set; }
        public int ItemPerPage { get; set; }
        public int Count { get; set; }
        // comme la liste est de type generique la classe doit être générique
        public IReadOnlyList<T> Datas { get; set; }
    }
}