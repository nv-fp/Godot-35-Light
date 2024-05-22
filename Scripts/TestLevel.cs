using Godot;

using System.Collections.Generic;
using System.Linq;

public class TestLevel : Node2D {
    public List<Node2D> interiorNodes { get; private set; }
    public List<Node2D> exteriorNodes { get; private set; }

    [Signal] public delegate void OnEnter(TestLevel level, string areaName);
    [Signal] public delegate void OnExit(TestLevel level, string areaName);

    public TestLevel() {
        interiorNodes = new List<Node2D>();
        exteriorNodes = new List<Node2D>();
    }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready() {
        findNodes("Interior", interiorNodes);
        findNodes("Exterior", exteriorNodes);

        GD.Print("Interior Nodes:"); 
        foreach (var n in interiorNodes) {
            GD.Print($"  - {n.Name}");
        }
        GD.Print("Exterior Nodes:");
        foreach (var n in exteriorNodes) {
            GD.Print($"  - {n.Name}");
        }
    }

    private void findNodes(string zoneName, List<Node2D> list) {
        var interior = GetNode<Node2D>($"Misc/{zoneName}");
        foreach (var child in interior?.GetChildren()) {
            if (!(child is Node2D)) {
                continue;
            }
            var n = child as Node2D;
            if (n.Name.Equals("ZoneDefinition")) {
                continue;
            }
            list.Add(child as Node2D);
        }
    }

    public List<Node2D> GetObjects(string area) {
        switch (area.ToLower()) {
            case "interior": return interiorNodes;
            case "exterior": return exteriorNodes;
        }
        GD.Print($"fallthrough on {area}");
        return new List<Node2D>();
    }

    public void AreaEnteredSimple(Area2D area, string name) {
        var parentName = area.GetParent().Name;
        GD.Print($"{parentName} -> AreaEnteredSimple({area.Name}, {name})");
        EmitSignal("OnEnter", this, name);
    }

    public void AreaExitedSimple(Area2D area, string name) {
        var parentName = area.GetParent().Name;
        GD.Print($"{parentName} -> AreaExitedSimple({area.Name}, {name})");
        EmitSignal("OnExit", this, name);
    }
}
