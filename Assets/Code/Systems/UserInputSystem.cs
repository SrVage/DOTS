using Code.Components;
using Code.Components.Character;
using Unity.Entities;
using Unity.Mathematics;
using UnityEditor;
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
        private InputAction _saveAction;
        private InputAction _loadAction;
        private float2 _moveDirection;
        private float _shootInput;
        private float _jerkInput;
        private float _saveInput;
        private float _loadInput;
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

            _saveAction = new InputAction("save", binding: "<Keyboard>/Q");
            _saveAction.performed += context => { _saveInput = context.ReadValue<float>(); };
            _saveAction.started += context => { _saveInput = context.ReadValue<float>(); };
            _saveAction.canceled += context => { _saveInput = context.ReadValue<float>(); };
            _saveAction.Enable();
            
            _loadAction = new InputAction("load", binding: "<Keyboard>/L");
            _loadAction.performed += context => { _loadInput = context.ReadValue<float>(); };
            _loadAction.started += context => { _loadInput = context.ReadValue<float>(); };
            _loadAction.canceled += context => { _loadInput = context.ReadValue<float>(); };
            _loadAction.Enable();
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
                inputData.Save = _saveInput;
                inputData.Load = _loadInput;
            });
        }
    }
}