namespace SurveyBasket.API.Abstractions
{
    public class Result
    {
        public Result(bool IsSuccess, Error Error)
        {
            if((IsSuccess && Error != Error.None) || (!IsSuccess && Error == Error.None))
            {
                throw new InvalidOperationException(); 
            }
            this.IsSuccess = IsSuccess;
            this.Error = Error;

        }
        public bool IsSuccess { get; }
        public bool IsFailure => !IsSuccess;
        public Error Error { get; } = default!;

        public static Result Success() => new Result(true, Error.None);
        public static Result Failure(Error error) => new Result(false, error);
        public static Result<TValue> Success<TValue>(TValue value) => new (value,true, Error.None);
        public static Result<TValue> Failure<TValue>(Error error) => new (default,false, error);
    }

    public class Result<TValue> : Result
    {
        private readonly TValue? _value;
        public Result(TValue? value, bool IsSuccess, Error Error) : base(IsSuccess, Error)
        {
            _value = value;
        }

        public TValue Value => IsSuccess ? _value! : throw new InvalidOperationException();
    }
}
