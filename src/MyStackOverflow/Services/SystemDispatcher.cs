using System;
using System.ComponentModel;
using System.Windows.Threading;
using JetBrains.Annotations;
using MyStackOverflow.ViewModels;

namespace MyStackOverflow.Services
{
    internal class SystemDispatcher : ISystemDispatcher
    {
        private Dispatcher _instance;

        private bool? _designer;

        public void BeginInvokeOnUIifNeeded(Action action)
        {
            BeginInvoke(action);
        }

        public void Initialize([NotNull] Dispatcher dispatcher)
        {
            if (dispatcher == null)
            {
                throw new ArgumentNullException("dispatcher");
            }

            _instance = dispatcher;

            if (_designer == null)
            {
                _designer = DesignerProperties.IsInDesignTool;
            }
        }

        private void BeginInvoke(Action a)
        {
            if (_instance.CheckAccess() || _designer == true)
            {
                a();
            }
            else
            {
                _instance.BeginInvoke(a);
            }
        }
    }
}