using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using GameCore.Configuration;
using Ultraviolet;
using Ultraviolet.Input;

namespace GameCore.Input
{
    public interface IInputManager
    {
        bool IsKeyPressed(Key key);
        void OnKeyPressed(Key key, Action action);
        void OnInputAction(InputAction inputAction, Action action);
    }

    public class InputManager : IInputManager
    {
        private readonly IConfigurationManager _configurationManager;
        private IUltravioletInput InputDevice => _configurationManager.UltravioletContext?.GetInput();
        private KeyboardDevice KeyboardDevice => InputDevice.GetKeyboard();

        private readonly IDictionary<InputAction, Key> _keyMappings;

        public InputManager(IConfigurationManager configurationManager)
        {
            _configurationManager = configurationManager;
            _keyMappings = LoadKeyMappings();
        }

        private static IDictionary<InputAction, Key> LoadKeyMappings()
        {
            var builder = ImmutableDictionary.CreateBuilder<InputAction, Key>();
            builder.Add(InputAction.Exit, Key.Escape);
            return builder.ToImmutable();
        }

        public bool IsKeyPressed(Key key)
        {
            return KeyboardDevice.IsNotNull() && KeyboardDevice.IsKeyPressed(key);
        }

        public void OnKeyPressed(Key key, Action action)
        {
            if (IsKeyPressed(key))
                action();
        }

        public void OnInputAction(InputAction inputAction, Action action)
        {
            if (_keyMappings.TryGetValue(inputAction, out var keyMapping))
                OnKeyPressed(keyMapping, action);
        }
    }
}