using System;

namespace GameCore
{
    public class OneTimeAction
    {
        private Action _action;

        public static OneTimeAction From(Action action)
        {
            return new(action);
        }

        private OneTimeAction(Action action)
        {
            _action = action;
        }

        public void TryRun()
        {
            _action();
            _action = () => { };
        }
    }
}