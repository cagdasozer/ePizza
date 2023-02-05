using ePizza.Shared.Utilities.Results.ComplexType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePizza.Shared.Entities
{
	public abstract class DtoGetBase
	{
		public virtual ResultStatus	ResutStatus { get; set; }

		public virtual string Message { get; set; }
	}
}
