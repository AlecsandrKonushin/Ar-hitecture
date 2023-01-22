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

        private static Dictionary<Type, object> data = new Dictionary<Type, object>();

        private static Controller[] controllers;

        public static object GetMan { get; internal set; }

        #region INIT

        public static void InitControllers(Controller[] controllers)
        {
            BoxControllers.controllers = controllers;

            Coroutines.StartRoutine(InitGameRoutine());
        }

        private static IEnumerator InitGameRoutine()
        {
            CreateControllers();
            yield return null;

            InitControllers();
            yield return null;

            StartControllers();
            yield return null;
        }

        private static void CreateControllers()
        {
            foreach (var controller in controllers)
            {
                if (!CheckContainsController(controller.GetType()))
                {
                    var add = Instantiate(controller);

                    data.Add(add.GetType(), add);
                }
            }
        }

        private static void InitControllers()
        {
            foreach (var controller in data.Values)
            {
                (controller as IController).OnInitialize();
            }
        }

        private static void StartControllers()
        {
            foreach (var controller in data.Values)
            {
                (controller as IController).OnStart();
            }
        }

        #endregion

        public static T GetController<T>()
        {
            object controller;
            data.TryGetValue(typeof(T), out controller);

            return (T)controller;
        }

        public static void AddMonoController(Type type, MonoController monoController)
        {
            if (!CheckContainsController(type))
            {
                data.Add(type, monoController);
            }
        }

        private static bool CheckContainsController(Type type)
        {
            return data.ContainsKey(type);
        }
    }
}
