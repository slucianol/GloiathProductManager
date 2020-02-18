using System;
using System.Collections.Generic;
using System.Linq;

namespace GNB.ProductManager.Helpers {
    public class PaginatedList<T> : List<T> {
        public int PageIndex { get; private set; }
        public int TotalPages { get; private set; }

        public PaginatedList(List<T> items, int count, int pageIndex, int pageSize) {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);

            this.AddRange(items);
        }

        public bool HasPreviousPage {
            get {
                return (PageIndex > 1);
            }
        }

        public bool HasNextPage {
            get {
                return (PageIndex < TotalPages);
            }
        }

        public static PaginatedList<T> CreateAsync(IQueryable<T> source, int pageIndex, int pageSize) {
            int count = source.Count();
            List<T> paginatedItems = source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            return new PaginatedList<T>(paginatedItems, count, pageIndex, pageSize);
        }
    }
}