using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Core
{
    public class BoxControllers : MonoBehaviour
    {
        public static UnityEvent OnInit = new UnityEvent();

        private Dictionary<Type, object> data = new Dictionary<Type, object>();

        private Controller[] controllers;

        private static BoxControllers instance;

        private void Awake()
        {
            instance = this;
        }

        #region INIT

        public static void InitControllers(Controller[] controllers)
        {
            instance.controllers = controllers;

            Coroutines.StartRoutine(InitGameRoutine());
        }

        public static void StartControllers()
        {
            Coroutines.StartRoutine(StartGameRoutime());
        }

        private static IEnumerator InitGameRoutine()
        {
            CreateControllers();
            yield return null;

            InitControllers();
            yield return null;

            OnInit?.Invoke();
        }

        private static IEnumerator StartGameRoutime()
        {
            StartControllersIn();
            yield return null;

            OnInit?.Invoke();

            SetPause(false);
        }

        private static void CreateControllers()
        {
            foreach (var controller in instance.controllers)
            {
                if (!CheckContainsController(controller.GetType()))
                {
                    var add = Instantiate(controller);

                    instance.data.Add(add.GetType(), add);
                }
            }
        }

        private static void InitControllers()
        {
            foreach (var controller in instance.data.Values)
            {
                (controller as IController).OnInitialize();
            }
        }

        private static void StartControllersIn()
        {
            foreach (var controller in instance.data.Values)
            {
                (controller as IController).OnStart();
            }
        }

        #endregion

        public static void SetPause(bool value)
        {
            foreach (var controller in instance.data)
            {
                (controller.Value as IController).SetPause(value);
            }
        }

        public static T GetController<T>()
        {
            object controller;
            instance.data.TryGetValue(typeof(T), out controller);

            return (T)controller;
        }

        public static void AddMonoController(Type type, MonoController monoController)
        {
            if (!CheckContainsController(type))
            {
                instance.data.Add(type, monoController);
            }
        }

        private static bool CheckContainsController(Type type)
        {
            return instance.data.ContainsKey(type);
        }

        public static void SaveGame()
        {
            foreach (var controller in instance.data)
            {
                (controller.Value as IController).Save();
            }
        }
    }
}
