# UnityInspectorInterfaceAddon
These 2 small scripts allow you to use the interfaces in the inspector window.
To create a field with an interface, create a regular field using the InterfaceComponent data type 
and pass the name of the desired interface in angle brackets. 
For example: public InterfaceComponent<YouInterfaceName> _fieldName 
After that, a field will appear in the inspector that will accept objects 
inherited from MonoBehaviour and that implements your interface.
To use interface just cast the InterfaceComponent.Component to you interface.

Good luck)
