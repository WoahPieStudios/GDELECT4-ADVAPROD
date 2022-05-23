# Enemy Spawning

## Drones

## Totems
### Creating a totem
1. Add a model to the scene and reset its position. This will be important when we turn this gameobject into a prefab.
   ![](https://i.imgur.com/bW8I2mM.gif)

2. Add a `Drone Spawner` component. By default, this will add a `Drone Pool` and a `Drone Spawner` component to the object.
    ![](https://i.imgur.com/as9JkWI.gif)

3. Drag a `Drone` prefab to the `Drone Prefab` field of the `Drone Pool` component and adjust the other fields if necessary.
    ![](https://i.imgur.com/J8B3wRW.gif)

4. Create an empty game object and drag it to the `Spawn Point` field of the `Drone Spawner` component. ***Make sure that the radius of the spawn point would not intersect with the totem as this can lead to issues.***
    ![](https://i.imgur.com/K5WsY3Y.gif)

5. Once you are satisfied with the totem, drag it into a prefab folder in order to turn it into a prefab.
    ![](https://i.imgur.com/G2zHa8c.gif)