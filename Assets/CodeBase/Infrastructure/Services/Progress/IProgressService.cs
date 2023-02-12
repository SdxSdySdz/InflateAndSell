using CodeBase.Data;

namespace CodeBase.Infrastructure.Services.Progress
{
    public interface IProgressService : IService
    {
        public PlayerProgress Progress { get; set; }
    }
}