using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room {
    public enum Direction {
        North = 0,
        East = 1,
        South = 2,
        West = 3
    }

    public Coord coord;
    public int x {
        get { return coord.x; }
        set { coord.x = value; }
    }
    public int y {
        get { return coord.y; }
        set { coord.y = value; }
    }

    public bool [] exits;
    public bool northExit {
        get { return exits[(int)Direction.North]; }
        set { exits[(int)Direction.North] = value; }
    }

    public bool eastExit {
        get { return exits[(int)Direction.East]; }
        set { exits[(int)Direction.East] = value; }
    }

    public bool southExit {
        get { return exits[(int)Direction.South]; }
        set { exits[(int)Direction.South] = value; }
    }

    public bool westExit {
        get { return exits[(int)Direction.West]; }
        set { exits[(int)Direction.West] = value; }
    }

    public Room(int x, int y) {
        this.coord = new Coord(x, y);
        exits = new bool[4];
        for(int i = 0; i < exits.GetLength(0); i++) {
            exits[i] = true;
        }
    }

    public override int GetHashCode() {
        int hash = ((x+y) * (x+y+1))/2+y;
        return hash;
    }

    public override bool Equals(object obj) {
        if(obj.GetType() == typeof(Room)) {
            return ((Room)obj).GetHashCode() == GetHashCode();
        }
        return false;
    }

    public static Direction GetOppositeDirection(Direction dir) {

        if(dir == Direction.North) {
            return Direction.South;
        } else if(dir == Direction.South) {
            return Direction.North;
        } else if(dir == Direction.East) {
            return Direction.West;
        } else {
            return Direction.East;
        }
    }

    public override string ToString() {
        return "(" + x + ", " + y + ")";
    }
}

public struct Coord {
    public int x;
    public int y;

    public Coord(int x, int y) {
        this.x = x;
        this.y = y;
    }
}
