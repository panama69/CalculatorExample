using System;
using System.Linq;
using NUnit.Framework;
using HP.LFT.Report;
using HP.LFT.UnitTesting;
using NUnit.Framework.Interfaces;

namespace CalculatorExample
{
    [TestFixture]
    public abstract class UnitTestClassBase : UnitTestBase
    {
        [OneTimeSetUp]
        public void GlobalSetup()
        {
            TestSuiteSetup();
        }

        [OneTimeTearDown]
        public void GlobalTearDown()
        {
            TestSuiteTearDown();
            Reporter.GenerateReport();
        }

        [SetUp]
        public void BasicSetUp()
        {
            TestSetUp();
        }

        [TearDown]
        public void BasicTearDown()
        {
            TestTearDown();
        }

        protected override string GetClassName()
        {
            return TestContext.CurrentContext.Test.FullName;
        }

        protected override string GetTestName()
        {
            return TestContext.CurrentContext.Test.Name;
        }

        protected override Status GetFrameworkTestResult()
        {
            switch (TestContext.CurrentContext.Result.Outcome.Status)
            {
                case TestStatus.Failed:
                    return Status.Failed;
                case TestStatus.Inconclusive:
                case TestStatus.Skipped:
                    return Status.Warning;
                case TestStatus.Passed:
                    return Status.Passed;
                default:
                    return Status.Passed;
            }
        }
    }
}
