using System;
using System.Drawing;
using ScreenApi;
using ScreenApi.ScreenManager;
using Serilog;
using StepFunctionEngine.Models;
using StepFunctionEngine.Types;

namespace StepFunctionEngine.States
{
    public class BasicAction : IAction
    {
        private ScreenClickerApi _screenClickerApi;
        private ILogger _log;

        private BasicActionInputModel _input;
        public BasicAction(ILogger logger, BasicActionInputModel input)
        {
            _log = logger.ForContext<BasicAction>();
            
            _screenClickerApi = new ScreenClickerApi(logger);

            _input = input;
        }


        public bool run()
        {
            _log.Information("Starting Action Set");
            bool results; 
            
            results = runMany(1);
            
            _log.Information("Finished Action Set");

            return results;
        }

        private bool runMany(int count)
        {
            var results = true;
            for (int i = 0; i < count; i++)
            {
                results = results && executeBasicAction();
            }

            return results;
        }
        private bool executeBasicAction()
        {
            _log.Information("Starting 1 Action Run");

            _screenClickerApi.FindAndClick(_input.srcImg, _input.templateImg);

            _log.Information("Finished 1 Action Run");
            return true;
        }

    }
}