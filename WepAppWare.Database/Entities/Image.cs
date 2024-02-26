using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppWare.Database.Entities;
using WepAppWare.Database.Entities.Base;

namespace WepAppWare.Database.Entities
{
	public class Image : BaseEntity
	{
        public string Name { get; set; }
		public string Path { get; set; }
		public string AbsolutePath { get; set; }

		public IEnumerable<Product> Products { get; set; }
    }
}
