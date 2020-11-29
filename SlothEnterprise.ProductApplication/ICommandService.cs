using System.Threading.Tasks;

namespace SlothEnterprise.ProductApplication
{
    public interface ICommandService<in TCommand, TResult>
    {
        Task<TResult> ExecuteAsync(TCommand command);
    }
}