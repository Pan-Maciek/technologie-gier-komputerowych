using UnityEngine;

public class Room : MonoBehaviour {
    public RoomEntry top = RoomEntry.None;
    public RoomEntry left = RoomEntry.None;
    public RoomEntry bottom = RoomEntry.None;
    public RoomEntry right = RoomEntry.None;

    public float randomWeight = 1;

    public static readonly Vector2 Size = new(18 * 0.16f, 18 * 0.16f) ;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
