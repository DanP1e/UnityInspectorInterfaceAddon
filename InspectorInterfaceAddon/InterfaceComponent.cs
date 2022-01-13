using System;
using UnityEngine;

namespace InspectorAddons
{
    [Serializable]
    public class InterfaceField<T>
        where T : class
    {      
        [SerializeField]
        private Component _componentWithInterface;

        public Component Component
        {
            get => _componentWithInterface;
        }      
    }
}
