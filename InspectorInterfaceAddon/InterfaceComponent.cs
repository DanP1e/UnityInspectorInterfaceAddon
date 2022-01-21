using System;
using UnityEngine;

namespace InspectorAddons
{
    [Serializable]
    public class InterfaceComponent<T>
        where T : class
    {
        [SerializeField] private Component _componentWithInterface;

        public Component Component
        {
            get => _componentWithInterface;
            set 
            {
                if (_componentWithInterface is T)
                    _componentWithInterface = value;
                else
                    throw new InvalidCastException("The passed object must implement " +
                        $"{value.GetType().Name} interface!");
            }
        }
        public T Interface => (T)Convert.ChangeType(Component, typeof(T));
    }
}
