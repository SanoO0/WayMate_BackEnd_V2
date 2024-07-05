namespace Application.UseCases.Utils;

public interface IUseCaseParameterizeQuery<out TOutput, in TParam>
{
    TOutput Execute(TParam param);
}