# Out Of The Box

Out Of The Box is a multi-tool for Windows.

The way it works is that modules are created as Form objects in the <root>.Module namespace, when the entry point Form (Main) is loaded it reflects itself, iterates through all of they types, and finds the modules. From there the modules are added to a ListView so that the user may start a module by double clicking on it in the list.

Currently it is in development and should be considered unstable.
