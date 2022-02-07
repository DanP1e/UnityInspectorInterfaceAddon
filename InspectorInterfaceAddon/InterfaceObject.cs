using System;
using UnityEngine;

namespace InspectorAddons
{
    /// <typeparam name="InterfaceT">The interface that must 
    /// be implemented by the received object.</typeparam>
    /// <typeparam name="ObjectY">The type from which the received 
    /// object should be inherited</typeparam>
    [Serializable]
    public class InterfaceObject<InterfaceT, ObjectY>
        where InterfaceT : class
        where ObjectY: UnityEngine.Object
    {
        [SerializeField] private ObjectY _objectWithInterface;

        public ObjectY Object
        {
            get => _objectWithInterface;
            set 
            {
                if (value is InterfaceT)
                    _objectWithInterface = value;
                else
                    throw new InvalidCastException("The passed object must implement " +
                        $"{value.GetType().Name} interface!");
            }
        }
        public InterfaceT Interface => (InterfaceT)(object)Object;
    }
}
