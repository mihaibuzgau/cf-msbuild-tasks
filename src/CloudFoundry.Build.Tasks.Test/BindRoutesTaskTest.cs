﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CloudFoundry.Build.Tasks.Test.Properties;
using Microsoft.QualityTools.Testing.Fakes;
using CloudFoundry.UAA;
using System.Threading.Tasks;
using CloudFoundry.CloudController.V2;
using CloudFoundry.CloudController.V2.Client.Data;
using System.Collections.Generic;

namespace CloudFoundry.Build.Tasks.Test
{
    [TestClass]
    public class BindRoutesTaskTest
    {
        [TestMethod]
        public void BindRoutes_Test()
        {
            using (ShimsContext.Create())
            {
                CloudFoundry.CloudController.V2.Client.Fakes.ShimCloudFoundryClient.AllInstances.LoginCloudCredentials = TestUtils.CustomLogin;

                CloudFoundry.CloudController.V2.Client.Base.Fakes.ShimAbstractAppsEndpoint.AllInstances.AssociateRouteWithAppNullableOfGuidNullableOfGuid = CustomAssociate;

                BindRoutes task = new BindRoutes();
                task.CFAppGuid = Guid.NewGuid().ToString();
                task.CFRouteGuids = new string[1] { Guid.NewGuid().ToString() };
                task.CFUser = Settings.Default.User;
                task.CFPassword = Settings.Default.Password;
                task.CFSavedPassword = false;
                task.CFServerUri = Settings.Default.ServerUri;
                task.BuildEngine = new FakeBuildEngine();

                Assert.IsTrue(task.Execute());
            }
        }

        private Task<AssociateRouteWithAppResponse> CustomAssociate(CloudController.V2.Client.Base.AbstractAppsEndpoint arg1, Guid? arg2, Guid? arg3)
        {
            var task = Task.Run(() => { return new AssociateRouteWithAppResponse(); });
            return task;
        }

    }
}
