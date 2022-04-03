using Code.Components;
using Code.Components.Character;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Code.Systems
{
    public class UserInputSystem:ComponentSystem
    {
        private EntityQuery _inputQuery;
        private InputAction _moveAction;
        private InputAction _shootAction;
        private InputAction _jerkAction;
        private float2 _moveDirection;
        private float _shootInput;
        private float _jerkInput;
        protected override void OnCreate()
        {
            _inputQuery = GetEntityQuery(ComponentType.ReadOnly<InputData>());
        }

        protected override void OnStartRunning()
        {
            _moveAction = new InputAction("move", binding: "<Gamepad>/rightStick");
            _moveAction.AddCompositeBinding("Dpad")
                .With("Up", binding: "<Keyboard>/w")
                .With("Down", binding: "<Keyboard>/s")
                .With("Left", binding: "<Keyboard>/a")
                .With("Right", binding: "<Keyboard>/d");
            _moveAction.performed += context => { _moveDirection = context.ReadValue<Vector2>(); };
            _moveAction.started += context => { _moveDirection = context.ReadValue<Vector2>(); };
            _moveAction.canceled += context => { _moveDirection = context.ReadValue<Vector2>(); };
            _moveAction.Enable();

            _shootAction = new InputAction("shoot", binding: "<Keyboard>/space");
            _shootAction.performed += context => { _shootInput = context.ReadValue<float>(); };
            _shootAction.started += context => { _shootInput = context.ReadValue<float>(); };
            _shootAction.canceled += context => { _shootInput = context.ReadValue<float>(); };
            _shootAction.Enable();

            _jerkAction = new InputAction("jerk", binding: "<Keyboard>/E");
            _jerkAction.performed += context => { _jerkInput = context.ReadValue<float>(); };
            _jerkAction.started += context => { _jerkInput = context.ReadValue<float>(); };
            _jerkAction.canceled += context => { _jerkInput = context.ReadValue<float>(); };
            _jerkAction.Enable();
        }

        protected override void OnStopRunning()
        {
            _moveAction.Disable();
            _shootAction.Disable();
            _jerkAction.Disable();
        }

        protected override void OnUpdate()
        {
            Entities.With(_inputQuery).ForEach((Entity entity, ref InputData inputData) =>
            {
                inputData.MoveDirection = _moveDirection;
                inputData.Shoot = _shootInput;
                inputData.Jerk = _jerkInput;
            });
        }
    }
}