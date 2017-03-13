using System;
using TechTalk.SpecFlow;
using NUnit.Framework;
using HP.LFT.SDK;
using HP.LFT.Report;
using HP.LFT.SDK.StdWin;
using System.Diagnostics;


namespace CalculatorExample
{
    public partial class CalcSumFeature : UnitTestClassBase
    { }

    [Binding]

    public class CalcSumSteps
    {
        // Create variables to hold the calculator process and window ID that persist across the entire test

        private static Process CalcProcess;
        private static IWindow MyCalc;

        private static CalcAppModel MyCalcModel;


        [BeforeTestRun]
        //*************************************************************************************************************************************
        // Name         : SetUpCalc
        //
        // Description  : Starts the calculator process and attach to it so we can drive it during the test
        //
        //*************************************************************************************************************************************

        public static void SetUpCalc()
        {
            //First check to see if the calculator is open.

            Process[] localByName = Process.GetProcessesByName("calc");

            // It could be open multple times therefore kill ALL of the open processes

            foreach (var MyCalcProcess in localByName)
            {
                MyCalcProcess.Kill();
            }

            // Now that we know the calculator isn't openstart a new calculator process

            CalcProcess = new Process { StartInfo = { FileName = @"C:\Windows\System32\calc.exe" } };
            CalcProcess.Start();

            // Set a variable that can be used to attach to the calculator window.

            MyCalc = Desktop.Describe<IWindow>(new WindowDescription { WindowClassRegExp = @"CalcFrame", WindowTitleRegExp = @"Calculator" });

            // Start a new instance of the App Model

            MyCalcModel = new CalcAppModel();


        }

        [Given(@"I have entered (.*) into the calculator")]
        //*************************************************************************************************************************************
        // Name         :  GivenIHaveEnteredIntotheCalculator
        //
        // Description  :  Enter a number into the calcuator
        //
        //*************************************************************************************************************************************
        public void GivenIHaveEnteredIntoTheCalculator(int intFirstAmt)
        {
            // It's possible that we pass in number more than one character thus we need to push each numeric button 
            // individually. Therefore, for each character in the converted string, loop and push the appropriate button
            string strInputNumber = intFirstAmt.ToString();


            foreach (char MyChar in strInputNumber)
            {
                MyCalc.Describe<IButton>(new ButtonDescription
                {
                    Text = @MyChar.ToString(),
                    NativeClass = @"Button"
                }).Click();
            }
        }

        [Given(@"I press ""(.*)""")]

        //*************************************************************************************************************************************
        // Name         : GivenIPress
        //
        // Description  : Press the appropriate button depending on the operator required (i.e. Add, Subtract etc.)
        //
        //*************************************************************************************************************************************
        public void GivenIPress(string strCalcType)
        {
            // Clear the operator type

            string strOperator = null;

            // Example the passed in variable.  It will state which operated, in english, the test is validating.  Use this to set the appropriate calculator
            // button to be pressed.  For example, to Multiply a number on the calculator you must press the * button.

            switch (strCalcType)
            {
                case "Add":
                    strOperator = "+";
                    break;
                case "Subtract":
                    strOperator = "-";
                    break;
                case "Multiply":
                    strOperator = "*";
                    break;
                case "Divide":
                    strOperator = "/";
                    break;
            }

            // Now we know which button needs to be pressed, go ahread and press it!

            MyCalc.Describe<IButton>(new ButtonDescription { Text = @strOperator, NativeClass = @"Button" }).Click();
        }


        [When(@"I press equals")]
        public void WhenIPressEquals()
        {
            MyCalcModel.CalculatorWindow.MyEqualsButton.Click();
        }


        [Then(@"the result should be (.*) on the screen")]
        //*************************************************************************************************************************************
        // Name         : ThenTheResultShouldBeOntheScreen
        //
        // Description  : Compare the result on the calculator screen with the expected result in the test
        //
        //*************************************************************************************************************************************
        public void ThenTheResultShouldBeOnTheScreen(int intExpectedResult)
        {
            // First extract the result displayed in the calculator window.  The result will be string, thus we need to convert it to an integer
            // so we can compare it to the expected result

            string strResultFromApp = null;
            strResultFromApp = MyCalc.Describe<IStatic>(new StaticDescription { WindowId = 150, NativeClass = @"Static" }).Text;
            int intResultFromScreen = Int32.Parse(strResultFromApp);

            // If the actual and expect result match, write a message to the LeanFT report and pass the test.  If not, do the opposite.

            if (intResultFromScreen == intExpectedResult)
            {
                Reporter.ReportEvent("Calc Result", "The resultant of the calculation is correct", HP.LFT.Report.Status.Passed);
            }
            else
            {
                Reporter.ReportEvent("Calc Result", "The resultant of the calculation is not correct", HP.LFT.Report.Status.Failed);
             //        Assert.Fail();
            };

        }

        [AfterTestRun]
        //*************************************************************************************************************************************
        // Name         : TearDownCalc()
        //
        // Description  : Kill the calculator process
        //
        //*************************************************************************************************************************************
        public static void TearDownCalc()
        {

            CalcProcess.Kill();
        }
    }
}
