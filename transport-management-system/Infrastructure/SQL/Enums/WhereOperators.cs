using System;

namespace transport_management_system.Infrastructure.SQL.Enums
{
    public static class WhereOperatorsExtension
    {
        public static string GetSqlWhereOperatorString(this WhereOperators @operator) => @operator switch
        {
            WhereOperators.Equal => " = ",
            WhereOperators.GreaterThan => " > ",
            WhereOperators.LessThan => " < ",
            WhereOperators.GreaterThanOrEqual => " => ",
            WhereOperators.LessThanOrEqual => " <= ",
            WhereOperators.NotEqual => " <> ",
            _ => throw new ArgumentOutOfRangeException(nameof(@operator), @operator, null)
        };
    }
    
    public enum WhereOperators
    {
        Equal,
        GreaterThan,
        LessThan,
        GreaterThanOrEqual,
        LessThanOrEqual,
        NotEqual,
    }
}
