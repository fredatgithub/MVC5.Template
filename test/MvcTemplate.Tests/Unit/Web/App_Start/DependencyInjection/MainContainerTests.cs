﻿using MvcTemplate.Components.Logging;
using MvcTemplate.Components.Mail;
using MvcTemplate.Components.Mvc;
using MvcTemplate.Components.Security;
using MvcTemplate.Controllers;
using MvcTemplate.Data.Core;
using MvcTemplate.Data.Logging;
using MvcTemplate.Services;
using MvcTemplate.Validators;
using MvcTemplate.Web;
using MvcTemplate.Web.DependencyInjection;
using System;
using System.Data.Entity;
using System.Web.Mvc;
using Xunit;
using Xunit.Extensions;

namespace MvcTemplate.Tests.Unit.Web.DependencyInjection
{
    public class MainContainerTests
    {
        private static MainContainer container;

        static MainContainerTests()
        {
            container = new MainContainer();
            container.RegisterServices();
        }

        #region Method: RegisterServices()

        [Theory]
        [InlineData(typeof(DbContext), typeof(Context))]
        [InlineData(typeof(IUnitOfWork), typeof(UnitOfWork))]

        [InlineData(typeof(ILogger), typeof(Logger))]
        [InlineData(typeof(IAuditLogger), typeof(AuditLogger))]

        [InlineData(typeof(IHasher), typeof(BCrypter))]
        [InlineData(typeof(IMailClient), typeof(SmtpMailClient))]

        [InlineData(typeof(IRouteConfig), typeof(RouteConfig))]
        [InlineData(typeof(IBundleConfig), typeof(BundleConfig))]
        [InlineData(typeof(IExceptionFilter), typeof(ExceptionFilter))]

        [InlineData(typeof(IMvcSiteMapParser), typeof(MvcSiteMapParser))]

        [InlineData(typeof(IRoleService), typeof(RoleService))]
        [InlineData(typeof(IAccountService), typeof(AccountService))]

        [InlineData(typeof(IRoleValidator), typeof(RoleValidator))]
        [InlineData(typeof(IAccountValidator), typeof(AccountValidator))]
        public void RegisterServices_Transient(Type abstraction, Type expectedType)
        {
            Object expected = container.GetInstance(abstraction);
            Object actual = container.GetInstance(abstraction);

            Assert.IsType(expectedType, actual);
            Assert.NotSame(expected, actual);
        }

        [Theory]
        [InlineData(typeof(IAuthorizationProvider), typeof(AuthorizationProvider))]
        public void RegisterServices_Singleton(Type abstraction, Type expectedType)
        {
            Object expected = container.GetInstance(abstraction);
            Object actual = container.GetInstance(abstraction);

            Assert.IsType(expectedType, actual);
            Assert.Same(expected, actual);
        }

        #endregion
    }
}
