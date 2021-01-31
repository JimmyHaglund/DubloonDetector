By assigning the float value to a scriptable object, multiple behaviour scripts can refer to the same value without any direct coupling.

As the float value is a scriptable object asset, it doesn't belong to any one scene. This means that it persists between scene changes. It also means that individual object will function just fine as long as they have the reference.

It's possible to assign the float directly using the drop-down button. This allows individual behaviours to override the variable and be edited just like a regular field.