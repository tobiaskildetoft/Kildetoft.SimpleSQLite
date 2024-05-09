﻿using System.Linq.Expressions;

namespace Kildetoft.SimpleSQLite;

public interface IIndex
{
    bool Unique { get; }
}

public interface IIndex<T> : IIndex where T : IEntity
{
    Expression<Func<T,object>> IndexDefinition { get; }
}
