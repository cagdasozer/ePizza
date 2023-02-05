using ePizza.Shared.Utilities.Results.Abstract;
using ePizza.Shared.Utilities.Results.ComplexType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePizza.Shared.Utilities.Results.Concrete
{
	public class DataResult<T> : IDataResult<T>
	{
		public DataResult(ResultStatus resultStatus, T data)
		{
			ResultStatus = resultStatus; //success
			Data = data; //data list
		}

		public DataResult(ResultStatus resultStatus, string message, T data)
		{
			ResultStatus = resultStatus; //error
			Message = message; //message
			Data = data; //data null
		}

		public DataResult(ResultStatus resultStatus, string message, T data, Exception exception)
		{
			ResultStatus = resultStatus; //error
			Message = message; //error message
			Data = data; //data list
			Exception = exception; //exception divideZero
		}


		public T Data { get; }

		public ResultStatus ResultStatus { get; set; }

		public string Message { get; }

		public Exception Exception { get; }
	}
}
