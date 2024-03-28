using GameEngine.GameData;
using GameEngine.Systems.Main;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace GameEngine
{
    public class GameWindow : Game
    {
        GraphicsDeviceManager graphics;
        public SpriteBatch spriteBatch;

        public Vector2 Size { get => new Vector2(this.graphics.PreferredBackBufferWidth, this.graphics.PreferredBackBufferHeight); }

        public static float MemoryUse { get => (Process.GetCurrentProcess().WorkingSet64 / 1024 / 1024); }
        public static int FramePerSecond { get => SystemManager.deltaTime != 0 ? (int)(1 / SystemManager.deltaTime) : 0; }
        public static Point MousePos { get => InputCore.mousePos; }
        public static Point MouseTilePos { get => new Point((int)(((float)InputCore.mousePos.X + Camera.position.X) / (float)Tile.tileSize), (int)(((float)InputCore.mousePos.Y + Camera.position.Y) / (float)Tile.tileSize)); }

        public GameWindow()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            this.Window.Title = "Honor";
            this.IsMouseVisible = true;
            this.IsFixedTimeStep = false;
            this.graphics.PreferMultiSampling = false;
            this.graphics.SynchronizeWithVerticalRetrace = false;
            this.graphics.PreferredBackBufferWidth = 1920;
            this.graphics.PreferredBackBufferHeight = 1080;
        }
        public void ToggleFullScreen()
        {

            if (this.graphics.IsFullScreen)
            {
                this.graphics.PreferredBackBufferWidth = 1920;
                this.graphics.PreferredBackBufferHeight = 1080;
                this.graphics.ToggleFullScreen();
            }
            else
            {
                this.graphics.PreferredBackBufferWidth = this.graphics.GraphicsDevice.DisplayMode.Width;
                this.graphics.PreferredBackBufferHeight = this.graphics.GraphicsDevice.DisplayMode.Height;
                this.graphics.ToggleFullScreen();
            }
        }
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            base.Initialize();
            //Setup Camera
            /*
            camTarget = new Vector3(0, 0, 0);
            camPosition = new Vector3(0f, 0f, 0f);
            projectionMatrix = Matrix.CreateOrthographic(Size.X, Size.Y, 0, 100);

            viewMatrix = Matrix.CreateLookAt(camPosition, camTarget,
                         new Vector3(0f, 0f, 1f));// Y up
            worldMatrix = Matrix.CreateWorld(camTarget, Vector3.
                          Forward, Vector3.Up);
            //BasicEffect
            basicEffect = new BasicEffect(GraphicsDevice);
            basicEffect.Alpha = 1f;
            basicEffect.AmbientLightColor = new Vector3(255, 255, 255);
            //basicEffect.CurrentTechnique = basicEffect.Techniques["Colored"];

            // Want to see the colors of the vertices, this needs to be on
            basicEffect.VertexColorEnabled = true;

            //Lighting requires normal information which VertexPositionColor does not have
            //If you want to use lighting and VPC you need to create a custom def
            basicEffect.LightingEnabled = true;

            //Geometry  - a simple triangle about the origin

            basicEffect.DirectionalLight0.DiffuseColor = new Vector3(0.8f);
            basicEffect.DirectionalLight0.Direction = new Vector3(0, 0.75f, -1);
            basicEffect.DirectionalLight0.SpecularColor = new Vector3(0.95f);*/
            /*
            basicEffect.FogColor = new Vector3(0);
            basicEffect.FogEnabled = true;
            basicEffect.FogStart = mapsize*0.4f/2;
            basicEffect.FogEnd = mapsize*0.8f/2;*/
            /*
            vertices = Utilites.RenderHeightMap(HeightMap.GenSimplexNoise((int)mapsize, height), voxelsize, height).Move(new Vector3(-voxelsize * mapsize/2, 0, voxelsize * mapsize/2));
            SetUpIndices((int)mapsize);
            CalculateNormals();*/
            //model = Helper3D.BasicTransform(new Vector3(0, 0, 0), 50);

            //voxels[2, 1, 2] = true;
            //voxels[1, 1, 2] = true;
            //voxels[2, 1, 1] = true;
            //voxels[1, 1, 1] = true;

            //
            //voxels = HeightMap.To3d(HeightMap.GenSimplexNoise(10,25), 5);
            //

            ////model.AddTriangle(Helper3D.GetVoxelModel(voxels, new Vector3(0, 1, 0)), Color.Aqua);
            //model2.Reset();
            //model.Reset();
            ///*
            //model.AddSquare(Helper3D.BasicSquare(new Vector3(), Helper3D.VoxelSize), Color.Aqua);
            //model.AddSquare(Helper3D.BasicSquare(new Vector3(), Helper3D.VoxelSize).Move(new Vector3(0, 0, Helper3D.VoxelSize)), Color.Red);
            //model.AddSquare(Helper3D.BasicSquare(new Vector3(), Helper3D.VoxelSize).RotateY(new Vector3(), 90).Move(new Vector3(Helper3D.VoxelSize / 2, 0, Helper3D.VoxelSize / 2)), Color.Blue);
            //model.AddSquare(Helper3D.BasicSquare(new Vector3(), Helper3D.VoxelSize).RotateY(new Vector3(), -90).Move(new Vector3(-Helper3D.VoxelSize / 2, 0, Helper3D.VoxelSize / 2)), Color.ForestGreen);
            //*/
            //model.Reset();
            /*
            for (int y = -1; y < voxels.GetLength(1)+1; y++)
                for (int z = -1; z < voxels.GetLength(2)+1; z++)
                    for (int x = -1; x < voxels.GetLength(0)+1; x++)
                    {
                        Color clr = Color.White;
                        /*
                        try
                        {
                            if (voxels[x, y, z])
                                clr = Color.Red;
                            if (x == 1 & y == 1 & z == 1)
                                clr = Color.BlueViolet;
                            if (voxels[x, y, z])
                                model2.AddTriangle(Helper3D.BasicCube(new Vector3(x * Helper3D.VoxelSize, y * Helper3D.VoxelSize, z * Helper3D.VoxelSize), Helper3D.VoxelSize / 4), clr);
                        }
                        catch { }
                        model.AddTriangle(Helper3D.GetVoxelModel(voxels, new Vector3(x, y, z)), clr);
                            
                    }
            model.CalculateNormals();
            model2.CalculateNormals();
            */
            //triangleVertices = Utilites.BasicTransform(new Vector3(0, 0, 0), 20);

            //vertices = Utilites.BasicSquare(new Vector3(0, 0, -10), 20, Color.Crimson).RotateY(new Vector3(0, 0, 0), 90).AddNormals();
            //vertices = Utilites.BasicSquare(new Vector3(0, 0, 10), 20, Color.Aqua).AddNormals();
            //vertices = vertices.RemoveNormals().Add(Utilites.BasicSquare(new Vector3(0, 0, 10), 20, Color.Blue)).AddNormals();
            //vertices = vertices.RemoveNormals().Add(Utilites.BasicSquare(new Vector3(0, 0, -10), 20, Color.Blue)).AddNormals();
            //vertices = vertices.RemoveNormals().Add(Utilites.BasicSquare(new Vector3(0, 0, 10), 20, Color.BlueViolet).RotateY(new Vector3(0, 0, 0), 90)).AddNormals();
            //vertices = vertices.RemoveNormals().Add(Utilites.BasicSquare(new Vector3(0, 0, 10), 20, Color.BlueViolet).RotateZ(new Vector3(0, 0, 0), 90)).AddNormals();
            //vertices = vertices.RemoveNormals().Add(Utilites.BasicSquare(new Vector3(0, 0, 10), 20, Color.DarkOrange).RotateZ(new Vector3(0, 0, 0), 90)).AddNormals();
            /*
                vertexBuffer = new Microsoft.Xna.Framework.Graphics.VertexBuffer(GraphicsDevice, typeof(
                               VertexPositionColor), vertices.Length, BufferUsage.WriteOnly);
                vertexBuffer.SetData<VertexPositionColor>(vertices);*/
        }
        //My3DModel model = new My3DModel();
        //My3DModel model2 = new My3DModel();
        //bool[,,] voxels = new bool[5, 3, 5];

        //float voxelsize = 2;
        //float mapsize = 257;
        //float height = 50;
        //private void SetUpIndices(int val)
        //{
        //    indices = new int[(val - 1) * (val - 1) * 6];
        //    int counter = 0;
        //    for (int y = 0; y < val - 1; y++)
        //    {
        //        for (int x = 0; x < val - 1; x++)
        //        {
        //            int lowerLeft = x + y * val;
        //            int lowerRight = (x + 1) + y * val;
        //            int topLeft = x + (y + 1) * val;
        //            int topRight = (x + 1) + (y + 1) * val;

        //            indices[counter++] = topLeft;
        //            indices[counter++] = lowerRight;
        //            indices[counter++] = lowerLeft;

        //            indices[counter++] = topLeft;
        //            indices[counter++] = topRight;
        //            indices[counter++] = lowerRight;
        //        }
        //    }
        //}

        //private static void CalculateNormals()
        //{
        //    for (int i = 0; i < vertices.Length; i++)
        //        vertices[i].Normal = new Vector3(0, 0, 0);

        //    for (int i = 0; i < indices.Length / 3; i++)
        //    {
        //        int index1 = indices[i * 3];
        //        int index2 = indices[i * 3 + 1];
        //        int index3 = indices[i * 3 + 2];

        //        Vector3 side1 = vertices[index1].Position - vertices[index3].Position;
        //        Vector3 side2 = vertices[index1].Position - vertices[index2].Position;
        //        Vector3 normal = Vector3.Cross(side1, side2);

        //        vertices[index1].Normal += normal;
        //        vertices[index2].Normal += normal;
        //        vertices[index3].Normal += normal;
        //    }

        //    for (int i = 0; i < vertices.Length; i++)
        //        vertices[i].Normal.Normalize();
        //}

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            // TODO: use this.Content to load your game content here
            ResourseManager.Instalize();
        }

        protected override void Update(GameTime gameTime)
        {
            SystemManager.Update();
            SystemManager.deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            base.Update(gameTime);
        }

        BasicEffect basicEffect;

        //Camera
        public Vector3 camTarget { get; set; }
        public Vector3 camPosition { get; set; }
        public Matrix projectionMatrix { get; set; }
        public Matrix viewMatrix { get; set; }
        public Matrix worldMatrix { get; set; }

        //Geometric info
        //static VertexPositionColorNormal[] vertices;
        //static int[] indices;
        //VertexBuffer vertexBuffer;

        //Orbit
        //bool orbit = false;

        protected override void Draw(GameTime gameTime)
        {
            // TODO: Add your drawing code here

            /*
            camTarget = Camera.position.toVector3(0);
            
            camPosition = Camera.position.toVector3(0);
            */
            /*
            graphics.GraphicsDevice.Viewport = new Viewport(Camera.DisplayArea);
            graphics.GraphicsDevice.*/

            /*
            basicEffect.Projection = projectionMatrix;
            basicEffect.View = viewMatrix;
            basicEffect.World = worldMatrix;*/

            GraphicsDevice.Clear(Color.CornflowerBlue);
            RasterizerState rasterizerState = new RasterizerState();
            rasterizerState.MultiSampleAntiAlias = false;
            rasterizerState.FillMode = FillMode.Solid;
            rasterizerState.CullMode = CullMode.None;
            GraphicsDevice.RasterizerState = rasterizerState;
            // GameObjects and World
            SystemManager.System<RenderSystem>().Update();
            // UI
            SystemManager.System<UISystem>().Draw();

            base.Draw(gameTime);
        }

        public void BeginMyDraw()
        {
            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, SamplerState.PointClamp, null, null);
        }
        public void EndMyDraw()
        {
            spriteBatch.End();
        }
    }
}
