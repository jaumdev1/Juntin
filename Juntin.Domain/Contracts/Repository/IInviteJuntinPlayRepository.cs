using Domain.Dtos.JuntinPlay;
using Domain.Entities;

namespace Domain.Contracts.Repository;

public interface IInviteJuntinPlayRepository : IBaseRepository<InviteJuntinPlay>
{
    Task<InviteJuntinPlay> GetByCode(string code);
}