using CodeBase.Data;

namespace CodeBase.Infrastructure.Services.Progress
{
    public interface IProgressReader : IProgressInteractor
    {
        void LoadProgress(PlayerProgress progress);
    }
}