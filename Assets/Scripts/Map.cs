using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map {
  private Room[,] grid;

  public int width {
    get { return grid.GetLength(0); }
  }
  public int height {
    get { return grid.GetLength(1); }
  }

  private System.Random pseudoRandom;

  public Map(int seed) {
    this.pseudoRandom = new System.Random(seed);
  }

  public void Init(int width, int height) {
    grid = new Room[width, height];

    for(int x = 0; x < width; x++) {
      for(int y = 0; y < height; y++) {
        Room room = new Room(x, y);

        for(int i = 0; i < room.exits.GetLength(0); i++) {
          room.exits[i] = false;
        }

        grid[x,y] = room;
      }
    }
  }

  public Room GetRoom(int x, int y) {
    if(x < 0 || x > width - 1 || y < 0 || y > height - 1) {
      return null;
    }
    return grid[x, y];
  }

  public Room GetRoom(Room room, Room.Direction dir) {
    Room neighbor = null;

    if(dir == Room.Direction.North) {
      neighbor = GetRoom(room.x, room.y + 1);
    } else if(dir == Room.Direction.South) {
      neighbor = GetRoom(room.x, room.y - 1);
    } else if(dir == Room.Direction.East) {
      neighbor = GetRoom(room.x + 1, room.y);
    } else if(dir == Room.Direction.West) {
      neighbor = GetRoom(room.x - 1, room.y);
    }

    return neighbor;
  }

  public Room GetRandomRoom() {
    Room result = null;

    while(true) {
      int x = pseudoRandom.Next(0, width - 1);
      int y = pseudoRandom.Next(0, height - 1);

      result = GetRoom(x, y);

      if(result != null) {
        break;
      }
    }

    return result;
  }

  public void ConnectRooms(Room room1, Room room2) {
    if(room1 != null && room2 != null) {
      if(room1.x == room2.x-1) {
        room1.eastExit = true;
        room2.westExit = true;
      } else if(room1.x == room2.x+1) {
        room1.westExit = true;
        room2.eastExit = true;
      } else if(room1.y == room2.y+1) {
        room1.southExit = true;
        room2.northExit = true;
      } else if(room1.y == room2.y-1) {
        room1.northExit = true;
        room2.southExit = true;
      } else {
        Debug.Log("Rooms are not adjacent!");
      }
    }
  }

  public void ConnectRoomTo(Room room, Room.Direction dir) {
    Room adjacentRoom = GetRoom(room, dir);
    ConnectRooms(room, adjacentRoom);
  }

  public bool IsValidExit(Room room, Room.Direction dir) {
    if(room != null) {
      Room neighbor = GetRoom(room, dir);
      if(neighbor != null) {
        return room.exits[(int)dir] && neighbor.exits[(int)Room.GetOppositeDirection(dir)];
      }
    }

    return false;
  }

  public System.Random GetRandomGenerator() {
    return pseudoRandom;
  }
}