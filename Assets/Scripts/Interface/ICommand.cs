using Cysharp.Threading.Tasks;
using System.Threading;

public interface ICommand
{
    public void Execute();

    public UniTask AnimationCommand(CancellationToken cancellationToken);
}
