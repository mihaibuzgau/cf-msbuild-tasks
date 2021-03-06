﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.QualityTools.Testing.Fakes;
using CloudFoundry.Build.Tasks.Test.Properties;
using System.Threading.Tasks;
using System.IO;

namespace CloudFoundry.Build.Tasks.Test
{
    [TestClass]
    public class PushAppTaskTest
    {
        [TestMethod]
        public void PushApp_Test()
        {
            using (ShimsContext.Create())
            {
                CloudFoundry.CloudController.V2.Client.Fakes.ShimCloudFoundryClient.AllInstances.LoginCloudCredentials = TestUtils.CustomLogin;

                CloudFoundry.CloudController.V2.Client.Fakes.ShimAppsEndpoint.AllInstances.PushGuidStringBoolean = CustomPushJob;

                PushApp task = new PushApp();
                task.CFAppGuid = Guid.NewGuid().ToString();
                task.CFAppPath = Path.GetTempPath();
                task.CFStart = true;
                task.CFUser = Settings.Default.User;
                task.CFPassword = Settings.Default.Password;
                task.CFServerUri = Settings.Default.ServerUri;
                task.BuildEngine = new FakeBuildEngine();

                Assert.IsTrue(task.Execute());
            }
        }

        private System.Threading.Tasks.Task CustomPushJob(CloudController.V2.Client.AppsEndpoint arg1, Guid arg2, string arg3, bool arg4)
        {
            var task = Task.Run(() => { });
            return task;
        }
    }
}
