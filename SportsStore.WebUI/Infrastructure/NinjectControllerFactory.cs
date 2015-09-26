using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Moq;
using Ninject;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Concrete;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Infrastructure.Abstract;
using SportsStore.WebUI.Infrastructure.Concrete;

namespace SportsStore.WebUI.Infrastructure
{
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        private IKernel ninjectKernel;

        public NinjectControllerFactory()
        {
            ninjectKernel = new StandardKernel();
            AddBindings();
        }

        void AddBindings()
        {
            #region IProductRepository
            ninjectKernel.Bind<IProductRepository>().To<EFProductRepository>();
            #endregion

            #region e-mail
            EmailSettings emailSettings = new EmailSettings
            {
                WriteAsFile = Boolean.Parse(ConfigurationManager
                .AppSettings["Email.WriteAsFile"] ?? "false")
            };


            ninjectKernel.Bind<IOrderProcessor>()
                .To<EmailOrderProcessor>()
                .WithConstructorArgument("settings", emailSettings);
            #endregion

            #region Authentication

            ninjectKernel.Bind<IAuthProvider>().To<FormsAuthProvider>();

            #endregion
        }

        protected override IController GetControllerInstance(System.Web.Routing.RequestContext requestContext, Type controllerType)
        {
            return controllerType == null ? null : (IController) ninjectKernel.Get(controllerType);
        }
    }
}