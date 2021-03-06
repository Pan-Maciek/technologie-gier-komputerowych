using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static RoomEntry;
using Random = UnityEngine.Random;

public class MapGenerator : MonoBehaviour {
    public Room[] roomPrefabs;
    public Room firstRoom;
    public Transform instanceParent;
    public Player player;
    
    private readonly Dictionary<(int, int), Room> _generatedRooms = new();
    private readonly HashSet<(int, int)> _visited = new();
    
    void Start() {
        var room = Instantiate(firstRoom, instanceParent, true);
        _generatedRooms[(0, 0)] = room;
    }

    private Room RandomRoom(IReadOnlyList<Room> rooms) {
        var random = Random.value * rooms.Sum(x => x.randomWeight);
        var sum = 0f;
        foreach (var room in rooms) {
            sum += room.randomWeight;
            if (sum >= random) return room;
        }
        return rooms[0];
    }
    
    private static bool TestMustHave(RoomEntry entry, RoomEntry actual) => 
        (entry, actual) switch {
            (Undefined, _) => true,
            var (x, y) => x == y
        };
    
    private void GenerateRoomAt(int x, int y) {
        if (_generatedRooms.ContainsKey((x, y))) return;
        
        // 1. Find all neighbour states
        var mustHaveLeftEntry = _generatedRooms.ContainsKey((x - 1, y)) ? _generatedRooms[(x - 1, y)].right : Undefined;
        var mustHaveRightEntry = _generatedRooms.ContainsKey((x + 1, y)) ? _generatedRooms[(x + 1, y)].left : Undefined;
        var mustHaveTopEntry = _generatedRooms.ContainsKey((x, y + 1)) ? _generatedRooms[(x, y + 1)].bottom : Undefined;
        var mustHaveBottomEntry = _generatedRooms.ContainsKey((x, y - 1)) ? _generatedRooms[(x, y - 1)].top : Undefined;

        // 2. Find all suitable rooms
        var goodRooms = roomPrefabs.Where(room => 
            TestMustHave(mustHaveLeftEntry, room.left) && 
            TestMustHave(mustHaveRightEntry, room.right) &&
            TestMustHave(mustHaveTopEntry, room.top) && 
            TestMustHave(mustHaveBottomEntry, room.bottom)).ToArray();
        
        if (goodRooms.Length == 0) return;

        // 3. Select random room and instantiate it
        var room = Instantiate(RandomRoom(goodRooms), instanceParent, true);
        room.transform.position = new Vector3(Room.Size.x * x, Room.Size.y * y, 1);
        _generatedRooms[(x, y)] = room;

    }
    
    public void Generate(int x, int y) {
        if (_visited.Contains((x, y))) return;
        _visited.Add((x, y));
        var baseRoom = _generatedRooms[(x, y)];
        if (baseRoom.left is not None) GenerateRoomAt(x - 1, y);
        if (baseRoom.right is not None) GenerateRoomAt(x + 1, y);
        if (baseRoom.top is not None) GenerateRoomAt(x, y + 1);
        if (baseRoom.bottom is not None) GenerateRoomAt(x, y - 1);
    }

    public void FixedUpdate() {
        var pos = player.transform.position;
        var x = pos.x;
        var y = pos.y;

        var xx = (int) (Math.Sign(x) * Math.Floor((Math.Abs(x) + Room.Size.x / 2) / Room.Size.x));
        var yy = (int) (Math.Sign(y) * Math.Floor((Math.Abs(y) + Room.Size.y / 2) / Room.Size.y));
        
        Generate(xx, yy);
    }
}
