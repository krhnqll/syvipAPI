﻿namespace DataAccess.Service.Abstract
{
    public interface IResult
    {
        bool Success { get; }
        string Message { get; }
    }
}