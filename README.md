# Unity-Object-Recorder-Playback
A couple of Unity assets that will let you record a gameObject and play its position, rotation, and scale on another.

1. INSTALLATION
   - Download the "Recorder" folder & the files within it.
   - Flace the folder somewhere in your Unity project's assets folder.
   - That's it! You should have a script named Recorder.cs and a prefab named Clone.
   - Note that these were made in Unity 6000.0.36f1. They may or may not work on other versions.

2. USE
   - Attach the Recorder script as a component of the gameObject you want to record.
   - UNCHECK "Is Clone" IN THE OBJECT'S PROPERTIES!!! The script may not work properly if it reads the initial object as a clone.
   - Set the delay between clone spawns in the object's properties. 1 second delay = 600 "delay" ticks.
   - Set the "clone" object reference to the Clone prefab (or a custom clone object, if you prefer.)
   - Set the maximum amount of clones that can be spawned.
   - When set correctly: after the specified delay, a game object will spawn at your recorded object's initial position and follow the same path, rotations, and scale of that object. The delay will then repeat and more will spawn until your set maximum is reached.

3. CUSTOM CLONES
   - If you want to spawn a gameObject that isn't the pre-existing Clone prefab (like if you want to use a 3D gameObject,) you will need to make a custom clone.
   - First, attach the Recorder script as a component of you custom clone. Make sure "Is Clone" is checked for proper clone behaviour.
   - Then set the "clone" object reference in the recorded object's properties to your custom clone prefab.
   - Finally, set the "clone" object reference in your custom clone's properties to itself.
   - Delay and max clones are passed down from the recorded object automatically, you do not need to set them in your custom clone's properties.

OTHER NOTES
  - The Clone prefab comes with a trigger collider. Add a tag to the prefab to detect collisions with it.
  - Functionality in 3D is untested. This system is theoretically compatible with 3D, but was designed for 2D.
  - If you want to add more data to record, make sure you update the RecData class in the Recorder script in addition to the main functionality.
