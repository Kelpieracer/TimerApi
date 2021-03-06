using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using WebApi.Entities;
using WebApi.Helpers;
using WebApi.Models.Topics;
using WebApi.Repositories;

namespace WebApi.Services
{
    public interface ITopicService
    {
        public Task<TopicResponse> Create(CreateTopicRequest model, Account account);
        public TopicResponse Read(int id);
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

        public async Task<TopicResponse> Create(CreateTopicRequest model, Account account)
        {
            if (model == null) ErrorMessages.Throw(ErrorMessages.Code.BadRequest);
            var entity = _mapper.Map<Topic>(model);
            entity.AccountId = account.AccountId;
            var response = await _repository.AddAsync(entity);
            return _mapper.Map<TopicResponse>(response);
        }

        public TopicResponse Read(int id)
        {
            var entity = _repository.FetchById(id);
            if (entity == null)
                ErrorMessages.Throw(ErrorMessages.Code.NotFound);
            return _mapper.Map<TopicResponse>(entity);
        }

        public async Task<TopicResponse> Update(UpdateTopicRequest model, int accountId)
        {
            if (model == null) ErrorMessages.Throw(ErrorMessages.Code.BadRequest);
            var entity = AuthorizedEntity(model.TopicId, accountId);
            _mapper.Map(model, entity);
            entity.Modified = DateTime.UtcNow;
            var response = await _repository.UpdateAsync(entity);
            return _mapper.Map<TopicResponse>(response);
        }

        public async Task<TopicResponse> Delete(int id, int accountId)
        {
            AuthorizedEntity(id, accountId);
            var response = await _repository.DeleteAsync(id, accountId);
            return _mapper.Map<TopicResponse>(response);
        }

        public Topic AuthorizedEntity(int id, int accountId)
        {
            var entityToUpdate = _repository.FetchById(id);
            if(entityToUpdate == null)
                ErrorMessages.Throw(ErrorMessages.Code.BadRequest);

            if (entityToUpdate.AccountId != accountId)
                ErrorMessages.Throw(ErrorMessages.Code.UnAuthorized);
            return entityToUpdate;
        }
    }
}
