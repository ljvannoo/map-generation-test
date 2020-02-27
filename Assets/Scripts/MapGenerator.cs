using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public int width;
    public int height;
    [Range(0,100)]
    public int randomFillPercent;
    public int seed;
    public bool useRandomSeed = true;

    private Map map;

    void Start()
    {
        GenerateMap();
    }

    private void GenerateMap() {
        if(useRandomSeed) {
            seed = Time.time.GetHashCode();
        }

        // Seed 1082833894 starts at 0,0

        map = new Map(seed);

        map.Init(width, height);

        MazeGenerator generator = new PrimsAlgorithm(map);
        generator.generate();
    }

    void OnValidate() {
        GenerateMap();
    }

    void OnDrawGizmos() {
        Vector3 size = Vector3.one;
        Color activeColor = Color.white;
        Color inactiveColor = Color.black;
        for(int x = 0; x < width; x++) {
            for(int y = 0; y < height; y++) {
                Room room = map.GetRoom(x, y);
                Vector3 location = new Vector3(x-width/2, y-height/2, 0);
                float left = location.x - .5f;
                float right = location.x + .5f;
                float top = location.y + .5f;
                float bottom = location.y - .5f;

                Gizmos.color = Color.white;
                if(!IsolatedRoom(room)) {
                    Gizmos.DrawCube(location, size);
                }

                Gizmos.color = Color.red;

                if(!map.IsValidExit(room, Room.Direction.North)) {
                    // North
                    Gizmos.DrawLine(new Vector3(left, top, 0), new Vector3(right, top, 0));
                }

                if(!map.IsValidExit(room, Room.Direction.South)) {
                    // South
                    Gizmos.DrawLine(new Vector3(left, bottom, 0), new Vector3(right, bottom, 0));
                }

                if(!map.IsValidExit(room, Room.Direction.East)) {
                    // East
                    Gizmos.DrawLine(new Vector3(right, top, 0), new Vector3(right, bottom, 0));
                }

                if(!map.IsValidExit(room, Room.Direction.West)) {
                    // West
                    Gizmos.DrawLine(new Vector3(left, top, 0), new Vector3(left, bottom, 0));
                }
            }
        }
    }

    bool IsolatedRoom(Room room) {
        return (!map.IsValidExit(room, Room.Direction.North) &&
            !map.IsValidExit(room, Room.Direction.East) &&
            !map.IsValidExit(room, Room.Direction.South) &&
            !map.IsValidExit(room, Room.Direction.West));
    }
}
