using CodeBase.Data;

namespace CodeBase.Infrastructure.Services.Progress
{
    public interface IProgressWriter : IProgressInteractor
    {
        void UpdateProgress(PlayerProgress progress);
    }
}