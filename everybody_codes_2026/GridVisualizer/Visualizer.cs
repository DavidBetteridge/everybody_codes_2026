using Raylib_cs;
using System.Numerics;
using System.Collections.Generic;
using System.Linq;

namespace GridVisualizer;

public class Visualizer
{
    public static unsafe void Run(IEnumerable<char[,]> frames)
    {
        var frameList = frames.ToList();
        if (frameList.Count == 0) return;

        int width = frameList[0].GetLength(0);
        int height = frameList[0].GetLength(1);

        Raylib.InitWindow(1280, 720, "3D Grid Visualizer");
        Raylib.SetTargetFPS(60);

        // Create procedural textures
        Image waterImage = Raylib.GenImageChecked(64, 64, 32, 32, Color.Blue, Color.DarkBlue);
        Texture2D waterTexture = Raylib.LoadTextureFromImage(waterImage);
        Raylib.UnloadImage(waterImage);

        Image landImage = Raylib.GenImageChecked(64, 64, 32, 32, Color.Brown, Color.DarkBrown);
        Texture2D landTexture = Raylib.LoadTextureFromImage(landImage);
        Raylib.UnloadImage(landImage);

        Image sandImage = Raylib.GenImageChecked(64, 64, 8, 8, Color.Yellow, Color.Gold);
        Texture2D sandTexture = Raylib.LoadTextureFromImage(sandImage);
        Raylib.UnloadImage(sandImage);
        
        Image leafImage = Raylib.GenImageChecked(64, 64, 16, 16, Color.Green, Color.DarkGreen);
        Texture2D leafTexture = Raylib.LoadTextureFromImage(leafImage);
        Raylib.UnloadImage(leafImage);

        // Load cube model for textured drawing
        Mesh cubeMesh = Raylib.GenMeshCube(1.0f, 1.0f, 1.0f);
        Model cubeModel = Raylib.LoadModelFromMesh(cubeMesh);

        Camera3D camera = new Camera3D();
        camera.Position = new Vector3(width, width, height);
        camera.Target = new Vector3(width / 2f, 0, height / 2f);
        camera.Up = new Vector3(0, 1, 0);
        camera.FovY = 45.0f;
        camera.Projection = CameraProjection.Perspective;

        int currentFrame = 0;
        float frameTime = 0;
        float frameDuration = 0.1f;

        while (!Raylib.WindowShouldClose())
        {
            frameTime += Raylib.GetFrameTime();
            if (frameTime >= frameDuration)
            {
                frameTime = 0;
                currentFrame = (currentFrame + 1) % frameList.Count;
            }

            // Manual camera control for 3-plane movement
            float cameraSpeed = 10.0f * Raylib.GetFrameTime();
            if (Raylib.IsKeyDown(KeyboardKey.W)) Raylib.UpdateCameraPro(ref camera, new Vector3(cameraSpeed, 0, 0), Vector3.Zero, 0);
            if (Raylib.IsKeyDown(KeyboardKey.S)) Raylib.UpdateCameraPro(ref camera, new Vector3(-cameraSpeed, 0, 0), Vector3.Zero, 0);
            if (Raylib.IsKeyDown(KeyboardKey.D)) Raylib.UpdateCameraPro(ref camera, new Vector3(0, cameraSpeed, 0), Vector3.Zero, 0);
            if (Raylib.IsKeyDown(KeyboardKey.A)) Raylib.UpdateCameraPro(ref camera, new Vector3(0, -cameraSpeed, 0), Vector3.Zero, 0);
            if (Raylib.IsKeyDown(KeyboardKey.E)) Raylib.UpdateCameraPro(ref camera, new Vector3(0, 0, cameraSpeed), Vector3.Zero, 0);
            if (Raylib.IsKeyDown(KeyboardKey.Q)) Raylib.UpdateCameraPro(ref camera, new Vector3(0, 0, -cameraSpeed), Vector3.Zero, 0);

      //      Vector2 mouseDelta = Raylib.GetMouseDelta();
          //  Raylib.UpdateCameraPro(ref camera, Vector3.Zero, new Vector3(mouseDelta.X * 0.1f, mouseDelta.Y * 0.1f, 0), 0);

            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.SkyBlue);

            Raylib.BeginMode3D(camera);

            char[,] grid = frameList[currentFrame];
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    char cell = grid[x, y];
                    Vector3 position = new Vector3(x, 0, y);
                    
                    switch (cell)
                    {
                        case '.': // Water
                            cubeModel.Materials[0].Maps[(int)MaterialMapIndex.Albedo].Texture = waterTexture;
                            Raylib.DrawModelEx(cubeModel, position, Vector3.Zero, 0, new Vector3(1.0f, 0.2f, 1.0f), Color.White);
                            break;
                        case '#': // Land
                            cubeModel.Materials[0].Maps[(int)MaterialMapIndex.Albedo].Texture = landTexture;
                            Raylib.DrawModelEx(cubeModel, position, Vector3.Zero, 0, new Vector3(1.0f, 0.5f, 1.0f), Color.White);
                            break;
                        case '=': // Sand
                            cubeModel.Materials[0].Maps[(int)MaterialMapIndex.Albedo].Texture = sandTexture;
                            Raylib.DrawModelEx(cubeModel, position, Vector3.Zero, 0, new Vector3(1.0f, 0.4f, 1.0f), Color.White);
                            break;
                        case '@': // Tree
                            cubeModel.Materials[0].Maps[(int)MaterialMapIndex.Albedo].Texture = landTexture;
                            Raylib.DrawModelEx(cubeModel, position, Vector3.Zero, 0, new Vector3(1.0f, 0.5f, 1.0f), Color.White);
                            cubeModel.Materials[0].Maps[(int)MaterialMapIndex.Albedo].Texture = leafTexture;
                            Raylib.DrawModelEx(cubeModel, new Vector3(x, 0.75f, y), Vector3.Zero, 0, new Vector3(0.6f, 1.0f, 0.6f), Color.White);
                            break;
                    }
                }
            }

            Raylib.EndMode3D();

            Raylib.DrawText($"Frame: {currentFrame + 1}/{frameList.Count}", 10, 10, 20, Color.Black);
            Raylib.DrawText("WASD: Move, Q/E: Up/Down, Mouse: Look", 10, 40, 20, Color.DarkGray);

            Raylib.EndDrawing();
        }

        Raylib.UnloadTexture(waterTexture);
        Raylib.UnloadTexture(landTexture);
        Raylib.UnloadTexture(sandTexture);
        Raylib.UnloadTexture(leafTexture);
        Raylib.UnloadModel(cubeModel);

        Raylib.CloseWindow();
    }
}
