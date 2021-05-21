# GE2-Assignment
Games Engines 2 Assignment

## Babylon 5, Battle of Gorash VII

### Original video:  
[![YouTube](http://img.youtube.com/vi/TzPxVdL528Y/0.jpg)](https://www.youtube.com/watch?v=TzPxVdL528Y)
  


  
## Assets used
- ### Narn big ship:
  [https://3dwarehouse.sketchup.com/model/9cf3f95cc398149432f2476608648548/Narn-Gquan-Class-Heavy-Cruiser?hl=en](https://3dwarehouse.sketchup.com/model/9cf3f95cc398149432f2476608648548/Narn-Gquan-Class-Heavy-Cruiser?hl=en)

- ### Narn small ship:
  [https://3dwarehouse.sketchup.com/model/e15f4c98857b67e41e287f9f679ab620/Narn-Heavy-Fighter](https://3dwarehouse.sketchup.com/model/e15f4c98857b67e41e287f9f679ab620/Narn-Heavy-Fighter)

- ### Shadow big ship:
  [https://3dwarehouse.sketchup.com/model/71244a232082c6e11418790bb7cbd/Shadow-Battlecrab-class-Heavy-Cruiser](https://3dwarehouse.sketchup.com/model/71244a232082c6e11418790bb7cbd/Shadow-Battlecrab-class-Heavy-Cruiser)

- ### Shadow small ship:
  [https://3dwarehouse.sketchup.com/model/a5dba847d5918a137b131c30c84e0b7a/Shadow-Scout](https://3dwarehouse.sketchup.com/model/a5dba847d5918a137b131c30c84e0b7a/Shadow-Scout)

  
  
## Storyboard
1. Vortex portals open in the space
2. Some Narn ships spawn from the portal (big and small)
3. Ships start to follow patrol path in formation with some offset
4. Mid way Narn big ships shoot shiny thing into opposed part of the space (not implemented)
5. Shadow crabs appear (not implemented)
6. Shadow crab shoot spiky sphere of shadow small ships (not implemented)
7. Narn ships start to attack shadow ships with red rays, shadow ships start to attack narn ships with purple rays (not implemented)
8. Small ships engage each other (not implemented)
9. Shadow crabs destroy 2 narn ships in halves (not implemented)
10. Narn big ships graze shadow crab (not implemented)
11. Shadow crabs destroy all Narn big ships, except 2 (not implemented)
12. Narn ships retreating by turning 180 degrees and fleeing (not implemented)
13. Narn ships open vortex to flee (not implemented)
14. Shadow crabs shoot glowing thing into vorteces (not implented)
15. Narn ships escaping destroyed (not implemented)
16. Shadow ships tow damaged crab away (not implemented)


## Implemented scripts
- ### States for FSM for Narn big ships:  
  1. PatrolState
  2. AttackState (not implemented)
  3. FleeState (not fully implemented)

- ### States represent behaviours  
  1. PatrolState -> PathFollowBehaviour  
     Verty similar to what we implemented in class, except each ship follows common path for the group with pre-defined but randomized offset from each navpoint. Each ship tracks progress against common path independently
  2. FleeState -> ShipFleeBehaviour  
     Turn 180 degrees and start escape sequence.

- ### CameraFollows  
  camera selects random ship on the screen, finds a random point in the sphere around this ship, jumps there and starts following this ship movement for 5-25 seconds (selected randomly) 

- ### FleetEntrance  
  Script which creates a common path for a group of ships, randomly selecting navpoints in a space. Navpoints are constant distance from each other, but next navpoint is located within cone with 160 degrees of freedom in x and y axis around local rotation of previous cone. After that it spawns ships, one by one, only when it detects that closest ship is far enough and assigns to them randomized offset for path following within predifined radius around navpoints. When all ships spawned, portal vortex is removed

- ### PlayScene  
  Scened driver, which creates initial vortex portals and enables camera scenic movement

- ### ShipController  
  Enables initial state of the ship for FSM

## Scripts re-used from the class
- Final State Machine (simplified, no global state version)
- Steering Behaviour abstract class
- Boid
- Path (simplified, no looping)

## Portal Vortex VFX
Made out of particle effect with lots and lots of particles emitted per second (100) in a cone with dynamic size (small-big-small) and trails of different colour. Additionally whole particle is rotated slowly over Z (forward) axis to add "spinning" effect.

## Resulting Video