# 3dWorldBuilder
Focus of the test project

When working on the test task I was trying to experiment with architecture design, that would be scalable, easy to use&understand and would be following the solid principles. I decided to implement the modular system, where behaviors can be developed using modular, simple components. Also the focus was to make the system, where most of components are processed in similar fashion which will help to enforce the architecture decisions on the developers.  So I decided to implement the system where developers can create a behaviors for actors. The actor is a container for the actions that can be performed. The action is a container of activators, deactivators and the behaviors. The behaviors would be run when the action is active. The action will be activated by the activator functions (when they are evaluated to true), and would be deactivated by evaluating to true the deactivator actions.

After the implementation I tried to collect my thoughts on what is the pros and cons of the chosen design.

The pros of the design:
- Small, reusable components;
- Easy to change the behavior, without changing a lot of code;
- One top system that update everything will help to enforce the design;
- The behaviors are easily expandable.

The cons of the design:

- Hard to pass the needed classes (maybe attaching the data to the actor would help);
- Might be hard to understand and debug the code (the behavior is build using small components, to understand what it does developer would need to open each one);
- Actors are active all the time, so the update will try to call every actor update.

There are more optimizations will make sense to do in the test such as pooling, not recalculating the boundaries of the outline all the time, or create the outline in the more efficient manner

