using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _19T1021105.DomainModels
{
    public class ProductEditModel : Product
    {
        public List<ProductPhoto> Photos { get; set; }
        public List<ProductAttribute> Attributes { get; set; }
        public List<Product> Product { get; set; }

    }
}
