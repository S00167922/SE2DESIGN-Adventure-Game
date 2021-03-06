﻿using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Assignment_Adventure_Game
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        // Set up graphics and spriteBatch.
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        // Declare Player variable.
        Player player;

        // Declare player sprites. - Idle and Movement textures.
        Texture2D lookingDown, lookingUp, lookingLeft, lookingRight, movingDown, movingUp, movingLeft, movingRight;

        #region Item Experimentation.
        // Create item variables.
        Item key1, key2, key3, key4;

        // Create item lists.
        List<Item> room1Items, room2Items, room3Items;
        #endregion

        #region Wall Experimentation.
        // Create wall textures.        
        // Wall Texture Dictionary.
        Dictionary<string, Texture2D> area1Walls = new Dictionary<string, Texture2D>();
        #endregion

        #region Room and Door Experimentation.
        Door room1NorthDoor, room1WestDoor, room2SouthDoor, room3EastDoor;

        // Create list of doors for room 1 and room 2.
        List<Door> room1Exits, room2Exits, room3Exits;

        Room currentRoom, room1, room2, room3;
        #endregion
      
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);

            #region Set up screen resolution.
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
            graphics.ApplyChanges();
            #endregion

            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here  
             
            // Instantiate lists.
            room1Items = new List<Item>();
            room2Items = new List<Item>();
            room3Items = new List<Item>();

            room1Exits = new List<Door>();
            room2Exits = new List<Door>();
            room3Exits = new List<Door>();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            #region Load Player.
            #region Load Idle Player sprites.
            lookingLeft = Content.Load<Texture2D>("Images/Player/Stand Left");
            lookingRight = Content.Load<Texture2D>("Images/Player/Stand Right");
            lookingUp = Content.Load<Texture2D>("Images/Player/Stand Up");
            lookingDown = Content.Load<Texture2D>("Images/Player/Stand Down");
            #endregion

            #region Load Movement Player sprites.
            movingLeft = Content.Load<Texture2D>("Images/Player/Move Left");
            movingRight = Content.Load<Texture2D>("Images/Player/Move Right");
            movingUp = Content.Load<Texture2D>("Images/Player/Move Up");
            movingDown = Content.Load<Texture2D>("Images/Player/Move Down");                     
            #endregion

            // Load animated player sprite.
            player = new Player(
                this, // Game
                lookingDown, // Image
                new Vector2(300, 300), // Position
                Color.White, // Colour
                2); // Frames          

            // Send these textures to the Player class.
            player.GetAnimations(lookingLeft, lookingRight, lookingUp, lookingDown,
                movingLeft, movingRight, movingUp, movingDown);
            #endregion
           
            // Load wall textures
            area1Walls = Loader.ContentLoad<Texture2D>(Content, "Images/Walls/Area1");

            #region Item list setup
            // Load items.
            key1 = new Item(Content.Load<Texture2D>("Images/Items/Key 1"),
                new Vector2(400, 550),
                Color.White,
                "Opens silver doors.",
                "You found a silver key.",
                1);

            key2 = new Item(Content.Load<Texture2D>("Images/Items/Key 2"),
                new Vector2(450, 540),
                Color.White,
                "Opens gold doors.",
                "You found a gold key.",
                1);

            key3 = new Item(Content.Load<Texture2D>("Images/Items/Key 3"),
                new Vector2(200, 300),
                Color.White,
                "Opens green doors.",
                "You found a green key.",
                1);

            key4 = new Item(Content.Load<Texture2D>("Images/Items/Key 4"),
                new Vector2(100, 500),
                Color.White,
                "Opens blue doors.",
                "You found a blue key.",
                1);

            // Add to the item lists for each room.
            room2Items.Add(key1);
            room3Items.Add(key2);          
            #endregion
           
            // Rooms must be loaded first.
            #region Load Rooms.
            room1 = new Room(Content.Load<Texture2D>("Images/Floors/Wood Flooring"),
               room1Items,               
               area1Walls);

            room2 = new Room(Content.Load<Texture2D>("Images/Floors/Wood Flooring"),
                room2Items,         
                area1Walls);

            room3 = new Room(Content.Load<Texture2D>("Images/Floors/Wood Flooring"),
                room3Items,               
                area1Walls);
            #endregion            

            #region Room 1 doors.
            room1NorthDoor = new Door(Content.Load<Texture2D>("Images/Doors/A Open/Open Door North"),
               new Vector2(780, 47),
               Color.White,
               room2,
               true,
               1);

            room1WestDoor = new Door(Content.Load<Texture2D>("Images/Doors/B Silver/Silver Door West"),
               new Vector2(43, 107),
               Color.White,
               room3,
               false,
               1);

            // Add exits to list.
            room1Exits.Add(room1NorthDoor);
            room1Exits.Add(room1WestDoor);           
            #endregion
           
            #region Room 2 doors.
            room2SouthDoor = new Door(Content.Load<Texture2D>("Images/Doors/A Open/Open Door South"),
                new Vector2(780, 648),
                Color.White,
                room1,
                true,
                1);

            // Add exits to list.
            room2Exits.Add(room2SouthDoor);            
            #endregion

            #region Room 3 doors.
            room3EastDoor = new Door(Content.Load<Texture2D>("Images/Doors/B Silver/Silver Door East"),
               new Vector2(1214, 104),
               Color.White,
               room1,
               false,
               1);

            // Add exits to list.
            room3Exits.Add(room3EastDoor);
            #endregion

            #region Add the exit lists to their appropriate rooms.          
            room1.GetExits(room1Exits);
            room2.GetExits(room2Exits);
            room3.GetExits(room3Exits);
            #endregion

            // Set initial room.
            currentRoom = room1;                               
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
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
            // TODO: Add your update logic here         
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            // Update the player.
            player.Update(gameTime);

            // The following method is for collisions. It should be moved somewhere else.
            foreach (AnimatedSprite wall in currentRoom.Walls)
            {
                player.Collision(wall);
            }

            #region Check for door collisions and change room accordingly.
            foreach(Door exit in currentRoom.Exits)
            {
                if(exit.CheckCollision(player) == true)
                {
                    // Change the current room using the entered door's ChangeRoom() method.                                                       
                    currentRoom = exit.ChangeRoom(player);
                    break;           
                }               
            }
            #endregion

            // Update the current room.
            currentRoom.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
           
            // Draw the current room.
            currentRoom.Draw(spriteBatch);           

            // Draw the player.
            player.Draw(spriteBatch);
         
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
