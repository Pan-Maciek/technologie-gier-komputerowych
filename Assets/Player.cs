using System;
using UnityEngine;

public class Player : MonoBehaviour {
    // Start is called before the first frame update
    private MapGenerator _map;
    void Start() {
        _map = FindObjectOfType<MapGenerator>();
    }

    // Update is called once per frame
    void FixedUpdate() {
        var x = Input.GetAxis("Horizontal");
        var y = Input.GetAxis("Vertical");
        var pos = transform.position += new Vector3(x, y) * 0.02f;

        var X = pos.x;
        var Y = pos.y;

        var xx = (int) (Math.Sign(X) * Math.Floor((Math.Abs(X) + Room.Size.x / 2) / Room.Size.x));
        var yy = (int) (Math.Sign(Y) * Math.Floor((Math.Abs(Y) + Room.Size.y / 2) / Room.Size.y));
        
        _map.Generate(xx, yy);
    }
}
