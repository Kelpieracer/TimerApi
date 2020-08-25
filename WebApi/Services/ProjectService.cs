using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using WebApi.Entities;
using WebApi.Helpers;
using WebApi.Models.Projects;
using WebApi.Repositories;

namespace WebApi.Services
{
    public interface IProjectService
    {
        public Task<ProjectResponse> Create(CreateProjectRequest model, Account account);
        public ProjectResponse Read(int id);
        public Task<ProjectResponse> Delete(int id, int accountId);
        public Task<ProjectResponse> Update(UpdateProjectRequest model, int accountId);
    }

    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _repository;
        private readonly IMapper _mapper;

        public ProjectService(
            IProjectRepository repository,
             IMapper mapper
            )
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ProjectResponse> Create(CreateProjectRequest model, Account account)
        {
            if (model == null) ErrorMessages.Throw(ErrorMessages.Code.BadRequest);
            var entity = _mapper.Map<Project>(model);
            entity.AccountId = account.AccountId;
            var response = await _repository.AddAsync(entity);
            return _mapper.Map<ProjectResponse>(response);
        }

        public ProjectResponse Read(int id)
        {
            var entity = _repository.GetById(id);
            var response = _mapper.Map<ProjectResponse>(entity);
            _mapper.Map(entity.WorkItems, response.WorkItems);
            _mapper.Map(entity.ProjectMembers, response.ProjectMembers);
            foreach(var projectMember in entity.ProjectMembers)
            {
                var i = entity.ProjectMembers.IndexOf(projectMember);
                response.ProjectMembers[i].ProjectName = entity.Name;
                //_mapper.Map(entity.ProjectMembers[i].Account, response.ProjectMembers[i].AccountResponse);
            }
            return response;
        }

        public async Task<ProjectResponse> Update(UpdateProjectRequest model, int accountId)
        {
            if (model == null) ErrorMessages.Throw(ErrorMessages.Code.BadRequest);
            var entity = AuthorizedEntity(model.ProjectId, accountId);
            _mapper.Map(model, entity);
            entity.Modified = DateTime.UtcNow;
            var response = await _repository.UpdateAsync(entity);
            return _mapper.Map<ProjectResponse>(response);
        }

        public async Task<ProjectResponse> Delete(int id, int accountId)
        {
            AuthorizedEntity(id, accountId);
            var response = await _repository.DeleteAsync(id, accountId);
            return _mapper.Map<ProjectResponse>(response);
        }

        public Project AuthorizedEntity(int id, int accountId)
        {
            var entityToUpdate =  _repository.GetById(id);
            if(entityToUpdate == null)
                ErrorMessages.Throw(ErrorMessages.Code.BadRequest);

            if (entityToUpdate.AccountId != accountId)
                ErrorMessages.Throw(ErrorMessages.Code.UnAuthorized);
            return entityToUpdate;
        }
    }
}
