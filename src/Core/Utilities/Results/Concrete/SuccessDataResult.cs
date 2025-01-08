using Core.Utilities.Results.Concrete;

namespace Core.Utilities.Results.Concrete
{
    public class SuccessDataResult<T> : DataResult<T>
    {
        public SuccessDataResult(T data, string message) : base(data, data != null, message)
        {
        }

        public SuccessDataResult(T data) : base(data, data != null)
        {
        }

        public SuccessDataResult(string message) : base(default, false, message)
        {
        }
    }
}