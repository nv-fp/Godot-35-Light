using Godot;

using System.Collections.Generic;

public class Player : KinematicBody2D {
    [Export]
    private int speed = 150;
    private Vector2 velocity = Vector2.Zero;

    private List<Node2D> checkObjects = new List<Node2D>();
    private HashSet<string> collisions = new HashSet<string>();

    private HashSet<string> currentZones = new HashSet<string>();
    public TestLevel level;

    public void AreaEntered(TestLevel level, string areaName) {
        currentZones.Add(areaName);
        UpdateObjectList();
    }

    public void AreaExited(TestLevel level, string areaName) {
        currentZones.Remove(areaName);
        UpdateObjectList();
    }

    public override void _PhysicsProcess(float delta) {
        HandleMove(delta);
        MoveAndSlide(velocity);

        var space = GetWorld2d().DirectSpaceState;

        uint layers = 1 << 0;
        foreach (var obj in checkObjects) {
            Godot.Collections.Dictionary results = space.IntersectRay(Position, obj.Position, collisionLayer: layers); ;
            if (results.Count > 0) {
                collisions.Add(obj.Name);
            } else {
                collisions.Remove(obj.Name);
            }
        }
    }

    private void HandleMove(float delta) {
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
        if (dx != 0 || dy != 0) {
            Update();
        }
        velocity = new Vector2(dx, dy).Normalized() * speed;
    }

    private void UpdateObjectList() {
        checkObjects.Clear();
        foreach (var zone in currentZones) {
            checkObjects.AddRange(level.GetObjects(zone));
        }
        Update();
    }

    public override void _Process(float delta) {
        HandleMove(delta);
    }

    public override void _Draw() {
        foreach (var obj in checkObjects) {
            DrawLine(Vector2.Zero, obj.GlobalPosition - GlobalPosition, collisions.Contains(obj.Name) ? Colors.Red : Colors.Green, 2, true);
            obj.Visible = !collisions.Contains(obj.Name);
        }
    }
}
