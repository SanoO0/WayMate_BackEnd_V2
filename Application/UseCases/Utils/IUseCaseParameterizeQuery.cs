﻿namespace Application.UseCases.Utils;

public interface IUseCaseParameterizeQuery<out TOutput, in TParam>
{
    TOutput Execute(TParam param);
}

public interface IUseCaseParameterizeQuery<out TOutput, in TParam1, in TParam2>
{
    TOutput Execute(TParam1 param1, TParam2 param2);
}

public interface IUseCaseParameterizeQuery<out TOutput, in TParam1, in TParam2, in TParam3>
{
    TOutput Execute(TParam1 param1, TParam2 param2, TParam3 param3);
}