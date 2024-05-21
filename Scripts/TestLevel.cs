using Godot;

using System.Collections.Generic;
using System.Linq;

public class TestLevel : Node2D {
    private List<Node> trackedNodes;

    public TestLevel() {
        trackedNodes = new List<Node>();
    }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready() {
        var interior = GetNode<Node>("Misc/Interior");
        foreach (var child in interior?.GetChildren()) {
            if (!(child is CollisionShape)) {
                trackedNodes.Append(child as Node);
            }
        }
    }

    public void AreaEnteredSimple(Area2D area, string name) {
        var parentName = area.GetParent().Name;
        GD.Print($"{parentName} -> AreaEnteredSimple({area.Name}, {name})");
    }

    public void AreaExitedSimple(Area2D area, string name) {
        var parentName = area.GetParent().Name;
        GD.Print($"{parentName} -> AreaExitedSimple({area.Name}, {name})");
    }
}
