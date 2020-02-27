using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrimsAlgorithm : MazeGenerator
{
    private HashSet<Room> inRooms;
    private List<Frontier> frontier;


    public PrimsAlgorithm(Map map) : base(map){
    }

    public override void generate() {
        inRooms = new HashSet<Room>();
        frontier = new List<Frontier>();

        Room firstRoom = map.GetRandomRoom();
        Mark(firstRoom);

        while(frontier.Count > 0 ) {
            Frontier frontierRoom = frontier[pseudoRandom.Next(0, frontier.Count - 1)];
            MarkFrontier(frontierRoom);
        }
    }

    private void Mark(Room room) {
        inRooms.Add(room);

        AddFrontier(room, Room.Direction.North);
        AddFrontier(room, Room.Direction.East);
        AddFrontier(room, Room.Direction.South);
        AddFrontier(room, Room.Direction.West);
    }

    private void MarkFrontier(Frontier frontierRoom) {
        map.ConnectRoomTo(frontierRoom.room, frontierRoom.fromDirection);

        frontier.Remove(frontierRoom);

        Mark(frontierRoom.room);
    }

    private void AddFrontier(Room room, Room.Direction dir) {
        Room neighbor = map.GetRoom(room, dir);
        if(neighbor == null) {
            return;
        }

        Frontier frontierRoom = new Frontier(neighbor, Room.GetOppositeDirection(dir));
        if(!inRooms.Contains(neighbor) && !frontier.Contains(frontierRoom)) {
            frontier.Add(frontierRoom);
        }
    }

    struct Frontier {
        public Room room;
        public Room.Direction fromDirection;

        public Frontier(Room room, Room.Direction fromDirection) {
            this.room = room;
            this.fromDirection = fromDirection;
        }

        public override int GetHashCode() {
            if(room == null) {
                return 0;
            }
            return room.GetHashCode();
        }

        public override bool Equals(object obj) {
            if(obj.GetType() == typeof(Frontier)) {
                return ((Frontier)obj).GetHashCode() == GetHashCode();
            }
            return false;
        }
    }
}
