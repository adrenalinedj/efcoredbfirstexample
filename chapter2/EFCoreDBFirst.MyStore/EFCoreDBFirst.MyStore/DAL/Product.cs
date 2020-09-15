using System;
using System.Collections.Generic;

namespace EFCoreDBFirst.MyStore.DAL
{
    public partial class Product
    {
        private DateTime _createdDate = DateTime.Now;

        public Product()
        {
            ProductView = new HashSet<ProductView>();
        }

        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public DateTime CreatedDate
        {
            get
            {
                return _createdDate;
            }
        }
        public DateTime ModifiedDate { get; set; }

        public Category Category { get; set; }
        public ICollection<ProductView> ProductView { get; set; }
    }
}
