using Godot;

using System;

public class Main : Node2D {
    [Export]
    public Godot.Texture EnvironmentTexture;

    private Camera2D cam;
    private Player player;
    private TestLevel level;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready() {
        GD.Print("Main._Ready()");
        player = GetNode<Player>("Player");
        level = GetNode<TestLevel>("TestLevel");

        player.level = level;
        level.Connect("OnEnter", player, "AreaEntered");
        level.Connect("OnExit", player, "AreaExited");
    }

    public override void _Process(float delta) {
    }
}
