namespace Evently.Common.Domain;
public sealed record ValidationError : Error
{
    public ValidationError(Error[] originals) 
        : base(
            "General.Validation",
            "One or more validation errors occurred",
            ErrorType.Validation)
    {
        Errors = originals;
    }

    public Error[] Errors { get; }
    
    public static ValidationError FromResults(IEnumerable<Result> results)
    {
        Error[] errors = results.Where(x => x.IsFailure)
            .Select(x => x.Error)
            .ToArray();
   
        
        return new(errors);
    }

}
