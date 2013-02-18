#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using RetroPortableUI.Wrapper.LibRetro.Support;
using RetroPortableUI.Wrapper.LibRetro;
#endregion

namespace RetroPortableUI.Wrapper.WinMG
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game, IRenderer
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        IRetroController retroLibController;

        public static void AddEnvironmentPaths(string paths)
        {
            string path = Environment.GetEnvironmentVariable("PATH") ?? string.Empty;
            path += ";" + string.Join(";", paths);

            Environment.SetEnvironmentVariable("PATH", path);
        }

        public Game1()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            retroLibController = new RetroWrapper(this);
            Content.RootDirectory = "Content";

#if X86_64 
            AddEnvironmentPaths("..\\..\\..\\..\\Cores\\x86_64");
#endif
#if X86
            AddEnvironmentPaths("..\\..\\..\\..\\Cores\\x86");
#endif
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();
            retroLibController.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            retroLibController.Update(gameTime.ElapsedGameTime.TotalMilliseconds);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            if (m_pixelData != null)
            {
                Color[] image = new Color[frameWidth * frameHeight];

                for (uint i = 0; i < frameWidth * frameHeight; i++)
                {
                    image[i] = new Color(m_pixelData[i].Red, m_pixelData[i].Green, m_pixelData[i].Blue);
                }
                Texture2D tex = new Texture2D(GraphicsDevice, (int)frameWidth, (int)frameHeight);
                tex.SetData<Color>(image);

                spriteBatch.Begin();

                spriteBatch.Draw(tex, new Rectangle(0, 0, (int)frameWidth, (int)frameHeight), Color.White);

                spriteBatch.End();

            }
            base.Draw(gameTime);
        }

        PixelDefinition[] m_pixelData;
        uint frameWidth, frameHeight, framePitch;

        bool IRenderer.RenderFrame(PixelDefinition[] pixelData, uint width, uint height, uint pitch)
        {
            m_pixelData = pixelData;
            frameWidth = width;
            frameHeight = height;
            framePitch = pitch;
            return true;
        }
    }
}
