using System;
using System.Collections.Generic;

namespace EFCoreDBFirst.MyStore.DAL
{
    public partial class ProductView
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Views { get; set; }

        public Product Product { get; set; }
    }
}
