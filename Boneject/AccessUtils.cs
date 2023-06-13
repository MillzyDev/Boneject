using System;
using System.Linq.Expressions;

namespace Boneject;

public static class AccessUtils
{
    // haha speedy expression go brrrrrrr
    public static Func<T, R> GetFieldAccessor<T, R>(string fieldName) 
    { 
        var param = Expression.Parameter(typeof(T),"arg");
        var member = Expression.Field(param, fieldName);
        var lambda = Expression.Lambda(typeof(Func<T,R>), member, param);
        var compiled = (Func<T,R>)lambda.Compile(); 
        return compiled; 
    }
}