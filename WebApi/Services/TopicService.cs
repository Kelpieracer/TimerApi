using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WebApi.Entities;
using WebApi.Helpers;
using WebApi.Models.Topics;
using WebApi.Repositories;

namespace WebApi.Services
{
    public interface ITopicService
    {
        public Task<TopicResponse> Create(CreateTopicRequest model, int accountId);
        public Task<TopicResponse> Read(int id);
        public Task<TopicResponse> Delete(int id, int accountId);
        public Task<TopicResponse> Update(UpdateTopicRequest model, int accountId);
    }

    public class TopicService : ITopicService
    {
        private readonly ITopicRepository _repository;
        private readonly IMapper _mapper;

        public TopicService(
            ITopicRepository repository,
             IMapper mapper
            )
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<TopicResponse> Create(CreateTopicRequest model, int accountId)
        {
            var entity = _mapper.Map<Topic>(model);
            entity.AccountId = accountId;
            var response = await _repository.AddAsync(entity);
            return _mapper.Map<TopicResponse>(response);
        }

        public async Task<TopicResponse> Read(int id)
        {
            var entity = await _repository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
            return _mapper.Map<TopicResponse>(entity);
        }

        public async Task<TopicResponse> Update(UpdateTopicRequest model, int accountId)
        {
            var entity = await AuthorizedEntity(model.Id, accountId);
            _mapper.Map(model, entity);
            var response = await _repository.UpdateAsync(entity);
            return _mapper.Map<TopicResponse>(response);
        }

        public async Task<TopicResponse> Delete(int id, int accountId)
        {
            await AuthorizedEntity(id, accountId);
            var response = await _repository.DeleteAsync(id, accountId);
            return _mapper.Map<TopicResponse>(response);
        }

        public async Task<Topic> AuthorizedEntity(int id, int accountId)
        {
            var entityToUpdate = await _repository.GetByIdAsync(id);
            if (entityToUpdate.AccountId != accountId)
                ErrorMessages.Throw(ErrorMessages.Code.UnAuthorized);
            return entityToUpdate;
        }
    }
}
