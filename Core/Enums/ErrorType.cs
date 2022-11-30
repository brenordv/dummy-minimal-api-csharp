namespace Core.Enums;

public enum ErrorType
{
    Undefined = 0,
    BusinessRuleFailed = 1,
    DatabaseFailed = 2,
    UnexpectedDatabaseFailure = 3,
    UnmappedApplicationFailure = 998,
    UnexpectedFailure = 999
}