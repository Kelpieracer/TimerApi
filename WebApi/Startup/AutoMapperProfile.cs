using AutoMapper;
using System.Runtime.InteropServices;
using WebApi.Entities;
using WebApi.Models.Accounts;
using WebApi.Models.Members;
using WebApi.Models.Projects;
using WebApi.Models.Topics;
using WebApi.Models.WorkItems;

namespace WebApi.Helpers
{
    public class AutoMapperProfile : Profile
    {
        // mappings between model and entity objects
        public AutoMapperProfile()
        {
            CreateMap<Account, AccountResponse>();
            CreateMap<Account, AuthenticateResponse>();
            CreateMap<RegisterRequest, Account>();
            CreateMap<CreateRequest, Account>();
            CreateMap<UpdateRequest, Account>()
                .ForAllMembers(x => x.Condition(
                    (src, dest, prop) =>
                    {
                        // ignore null & empty string properties
                        if (prop == null) return false;
                        if (prop.GetType() == typeof(string) && string.IsNullOrEmpty((string)prop)) return false;

                        // ignore null role
                        if (x.DestinationMember.Name == "Role" && src.Role == null) return false;

                        return true;
                    }
                ));
            CreateMap<Account, ShortAccountResponse>();

            CreateMap<Topic, TopicResponse>();
            CreateMap<CreateTopicRequest, Topic>();
            CreateMap<UpdateTopicRequest, Topic>();

            CreateMap<Project, ProjectResponse>();
            CreateMap<CreateProjectRequest, Project>();
            CreateMap<UpdateProjectRequest, Project>();

            CreateMap<ProjectMember, ProjectMemberResponse>();
            CreateMap<CreateProjectMemberRequest, ProjectMember>();
            CreateMap<UpdateProjectMemberRequest, ProjectMember>();
            

            CreateMap<WorkItem, WorkItemResponse>();
            CreateMap<CreateWorkItemRequest, WorkItem>();
            CreateMap<UpdateWorkItemRequest, WorkItem>();
            //CreateMap<WorkItem, TopicResponse>();

        }

    }
}