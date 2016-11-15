using System;
using AutoMapper;
using Com.Pinz.DomainModel;
using Com.Pinz.Server.DataAccess;
using Com.Pinz.Server.DataAccess.Model;
using Com.Pinz.Server.TaskService.InviteUser;
using Common.Logging;
using Ninject;

namespace Com.Pinz.Server.TaskService.SubscriptionApi
{
    public class SubscriptionUtils
    {
        private readonly ICategoryDAO _categoryDao;
        private readonly ICompanyDAO _companyDao;
        private readonly ILog _log = LogManager.GetLogger<SubscriptionUtils>();
        private readonly IMapper _mapper;
        private readonly IProjectDAO _projectDao;
        private readonly IProjectStaffDAO _projectStaffDao;
        private readonly ISubscriptionDAO _subscriptionDao;
        private readonly ITaskDAO _taskDao;
        private readonly IUserDAO _userDao;

        [Inject]
        public SubscriptionUtils(IUserDAO userDao, ITaskDAO taskDao, ISubscriptionDAO subscriptionDao,
            IProjectStaffDAO projectStaffDao, IProjectDAO projectDao, IMapper mapper, ICompanyDAO companyDao,
            ICategoryDAO categoryDao)
        {
            _userDao = userDao;
            _taskDao = taskDao;
            _subscriptionDao = subscriptionDao;
            _projectStaffDao = projectStaffDao;
            _projectDao = projectDao;
            _mapper = mapper;
            _companyDao = companyDao;
            _categoryDao = categoryDao;
        }

        
        public UserDO CreateSubscription(Subscription subscription, string originalEmail)
        {
            //create Subscription
            var subscriptionDo = _mapper.Map<SubscriptionDO>(subscription);
            subscriptionDo.OriginalEmail = originalEmail;
            _log.InfoFormat("Subscription ID:{0}, subscription created in DB. {1}", subscription.SubscriptionReference,
                subscriptionDo);

            //create Company
            var company = new CompanyDO();
            if (string.IsNullOrEmpty(subscription.Customer.Company))
                company.Name = $"{subscription.Customer.FirstName} {subscription.Customer.LastName}";
            else
                company.Name = subscription.Customer.Company;
            company.SubscriptionReference = subscriptionDo.SubscriptionReference;
            company.Subscription = subscriptionDo;
            _companyDao.Create(company);
            _log.InfoFormat("Subscription ID:{0}, company created in DB. {1}", subscription.SubscriptionReference,
                company);

            //create master User
            var user = new UserDO();
            user.EMail = subscription.Customer.Email;
            user.FirstName = subscription.Customer.FirstName;
            user.FamilyName = subscription.Customer.LastName;
            user.PhoneNumber = subscription.Customer.PhoneNumber;
            user.IsCompanyAdmin = true;
            user.CompanyId = company.CompanyId;
            user.IsPinzSuperAdmin = false;
            user.Password = RandomPassword.Generate();
            user = _userDao.Create(user);
            _log.InfoFormat("Subscription ID:{0}, user created in DB. {1}", subscription.SubscriptionReference, user);

            //create Project
            var project = new ProjectDO
            {
                CompanyId = company.CompanyId,
                Name = "GetStarted",
                Description = "Project to get you started with PINZ! Feel free to rename and use as you like."
            };
            project = _projectDao.Create(project);

            //staff user as admin on project
            var ps = new ProjectStaffDO
            {
                ProjectId = project.ProjectId,
                UserId = user.UserId,
                IsProjectAdmin = true
            };
            _projectStaffDao.Create(ps);

            //create tasks
            var categoryAdministration = CreateCategory(project, "Administration");

            CreateTask(categoryAdministration, user, "Rename this project",
                "In the Outlook Ribbon, click on PINZ tab. Fromo there you can navigate to Administration panel.");

            CreateTask(categoryAdministration, user, "Invite more users",
                "In the Outlook Ribbon, click on PINZ tab. Fromo there you can navigate to Administration panel.");

            CreateTask(categoryAdministration, user, "Close Tasks",
                "Close Tasks by ticking the checkboxes in the task list.");

            CreateTask(categoryAdministration, user, "Delete Categories",
                "Only empty categories can be deleted.");

            var educationAdministration = CreateCategory(project, "Education");
            CreateTask(educationAdministration, user, "Visit Support Page",
                "Visit our Support Senter at https://pinzonline.zendesk.com for more information.");

            return user;
        }

        private TaskDO CreateTask(CategoryDO category, UserDO user, string taskName, string body)
        {
            var task1 = new TaskDO
            {
                CategoryId = category.CategoryId,
                UserId = user.UserId,
                TaskName = taskName,
                Body = body,
                Status = TaskStatus.TaskNotStarted,
                ActualWork = 0,
                CreationTime = DateTime.Today,
                IsComplete = false
            };
            _taskDao.Create(task1);
            return task1;
        }

        private CategoryDO CreateCategory(ProjectDO project, string name)
        {
            var category1 = new CategoryDO
            {
                ProjectId = project.ProjectId,
                Name = name
            };
            _categoryDao.Create(category1);
            return category1;
        }
    }
}