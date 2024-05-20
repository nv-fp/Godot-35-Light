using Godot;

using System;

public class Player : Node2D {
    [Export]
    private int speed = 150;

    public override void _Ready() {

    }

    public override void _Process(float delta) {
        var dx = 0;
        var dy = 0;
        if (Input.IsActionPressed("ui_left")) {
            dx -= 1;
        }
        if (Input.IsActionPressed("ui_right")) {
            dx += 1;
        }
        if (Input.IsActionPressed("ui_up")) {
            dy -= 1;
        }
        if (Input.IsActionPressed("ui_down")) {
            dy += 1;
        }
        var dxy = (new Vector2(dx, dy)).Normalized() * speed * delta;
        Position = Position + dxy;
    }
}
