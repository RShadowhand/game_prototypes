using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;

namespace Cybernoire
{
    class Item
    {
        public string Name;

        public Item(string Name) {
            this.Name = Name;
        }
    }

    class Player
    {
        bool Switch(bool a)
        {
            return (a ? false : true);
        }

        World world;
        List<Item> inventory = new List<Item>();

        public Player(World world) {
            this.world = world;
            pos.Y = world.height;
            pos.X = world.position;
        }

        enum playerState {
            idle,
            walking,
            smoking,
            smokingidle,
            gunpull,
            gunidle,
            shooting,
            crouchingDown,
            crouching,
            crouchingUp,
            jumping
        }

        playerState pState = playerState.smokingidle;
        Texture2D currFrame;

        Dictionary<playerState, Animation> animSets = new Dictionary<playerState, Animation>();

        Point absPos = new Point(0, 0);
        Point pos = new Point(0,0);
        Rectangle pRect = new Rectangle();
        SpriteEffects Flipper = SpriteEffects.None;

        SpriteFont font;
        public void ContentLoad(ContentManager Content)
        {
            animSets.Add(playerState.smokingidle, new Animation(Content, "Player/Smoke/Loop/Sm", 2, 500, 1));
            animSets.Add(playerState.idle, new Animation(Content, "Player/Idle/player_idle_", 2, 500, 1));
            animSets.Add(playerState.walking, new Animation(Content, "Player/Move/player_walk_", 8, 75, 1));

            animSets.Add(playerState.crouchingDown, new Animation(Content, "Player/Crouch/C", 6, 50, 2));
            animSets[playerState.crouchingDown].skipLast = true;

            animSets.Add(playerState.crouchingUp, new Animation(Content, "Player/Crouch/C", 6, 50, 2, true));
            animSets[playerState.crouchingUp].skipLast = true;

            animSets.Add(playerState.crouching, new Animation(Content, "Player/Crouch/C", 6, 50, 2));
            animSets[playerState.crouching].fixedFrame = 5;

            #region oldcode
            /*for (int i = 1; i <= 2; i++)
            {
                idle.Add(Content.Load<Texture2D>("Player/Idle/player_idle_"+i.ToString()));
            }
            for (int i = 1; i <= 14; i++)
            {
                shoot.Add(Content.Load<Texture2D>("Player/Shoot/G" + i.ToString()));
            }
            for (int i = 1; i <= 8; i++)
            {
                jump.Add(Content.Load<Texture2D>("Player/Jump/J" + i.ToString()));
            }
            for (int i = 1; i <= 20; i++)
            {
                smoke.Add(Content.Load<Texture2D>("Player/Smoke/Sm" + i.ToString()));
            }
            for (int i = 1; i <= 3; i++)
            {
                crouch.Add(Content.Load<Texture2D>("Player/Crouch/C" + i.ToString()));
            }*/
            #endregion

            currFrame = animSets[pState].firstFrame;

            font = Content.Load<SpriteFont>("font");
        }

        /*bool isIdle = true;
        
        bool isStartSmoking = false;
        bool isIdleSmoking = false;*/

        bool isWalking = false;

        //bool isClimbing = false;

        bool isCrouching = false; 
        bool isCrouchingDown = false;
        bool isCrouchingUp = false;

        int size = 2;
        void calcPos()
        {
            if (pRect.X < 0)
            {
                pos.X = 0;
            }
            if (pRect.X >= 800 - pRect.Width)
            {
                pos.X = 799 - pRect.Width;
            }
            pRect.X = pos.X;
            pRect.Y =  600 - currFrame.Height*size - pos.Y;
            pRect.Height = currFrame.Height*size;
            pRect.Width = currFrame.Width*size;
        }

        KeyboardState ks;
        MouseState ms;
        bool keyDown = false;
        string oText = "";
        public void Update(GameTime gT) 
        {
            ks = Keyboard.GetState();
            ms = Mouse.GetState();
            
            #region AnimDebug
            if (Keyboard.GetState().IsKeyDown(Keys.Space) && keyDown == false)
            {
                keyDown = true;
            }
            #endregion

            #region Controls
            if (ks.IsKeyDown(Keys.End))
            {
                isCrouching = false;
                isCrouchingDown = false;
                isCrouchingUp = false;
            }
            foreach (Object o in world.WorldObjects.Values)
            {
                if (pRect.Intersects(o.rect))
                {
                    if (o.Type != ObjectType.background)
                    {
                        oText = o.Name;    
                    }
                    else
                    {
                        oText = "";
                    }
                }
            }
            if (!keyDown && ks.IsKeyDown(Keys.Up))
            {
                foreach (Object d in world.WorldObjects.Values)
                {
                    if (pRect.Intersects(d.rect))
                    {
                        if (d.Type == ObjectType.door)
                        {
                            world = (d as Door).Use((d as Door).connectsTo);
                            pos.Y = world.height;
                            pos.X = world.position;
                        }
                        if (d.Type == ObjectType.container)
                        {
                            inventory.Add((d as Container).Use());
                        }
                        if (d.Type == ObjectType.npc)
                        {
                            if (inventory.Count > 0)
                            {
                                (d as NPC).Use(inventory[0]);
                                inventory.RemoveAt(0);
                            }
                        }
                    }
                }
                keyDown = true;
            }
            if (ks.IsKeyDown(Keys.Right) || ks.IsKeyDown(Keys.Left))
            {
                int direction = 0;
                if (ks.IsKeyDown(Keys.Left))
                {
                    Flipper = SpriteEffects.FlipHorizontally;
                    direction = -1*size;
                }
                if (ks.IsKeyDown(Keys.Right))
                {
                    Flipper = SpriteEffects.None;
                    direction = 1*size;
                }
                isWalking = true;
                if (!isCrouching && !isCrouchingDown && !isCrouchingUp)
                {
                    pos.X += direction;
                }
            }
            else if (ks.IsKeyUp(Keys.Right) && ks.IsKeyUp(Keys.Left))
            {
                isWalking = false;
            }
            if (ks.IsKeyDown(Keys.Down) && !keyDown)
            {
                if (isCrouching)
                {
                    isCrouchingUp = true;
                }
                else
                {
                    isCrouchingDown = true;
                }
                
                keyDown = true;
                //pos.Y--;
            }
            else if (ks.IsKeyUp(Keys.Down) && ks.IsKeyUp(Keys.Up))
            {
                keyDown = false;
            }
            #endregion



            handleAnim(gT);
            calcPos();
            world.Update(gT);
        }

        void handleAnim(GameTime t)
        {

            if (isWalking)
            {
                pState = playerState.walking;
            }

            if (isCrouchingUp)
            {
                isCrouching = false;
                pState = playerState.crouchingUp;
                if (animSets[pState].animEnded)
                {
                    isCrouchingUp = false;
                    animSets[playerState.crouchingUp].reset();
                }
            }
            else if (isCrouching)
            {
                pState = playerState.crouching;
                animSets[playerState.crouchingDown].reset();
            }
            else if (isCrouchingDown)
            {
                pState = playerState.crouchingDown;
                if (animSets[pState].animEnded)
                {
                    isCrouchingDown = false;
                    isCrouching = true;
                }
            }


            if (!isWalking && !isCrouching && !isCrouchingDown && !isCrouchingUp)
            {
                pState = playerState.smokingidle;
            }

            currFrame = animSets[pState].getFrame(t);
        }

        void switchState() {
            switch (pState)
            {
                case playerState.idle:
                    pState = playerState.walking;
                    break;
                case playerState.walking:
                    pState = playerState.shooting;
                    break;
                case playerState.shooting:
                    pState = playerState.jumping;
                    break;
                case playerState.jumping:
                    pState = playerState.smoking;
                    break;
                case playerState.smoking:
                    pState = playerState.smokingidle;
                    break;
                case playerState.smokingidle:
                    pState = playerState.crouchingDown;
                    break;
                case playerState.crouchingDown:
                    pState = playerState.idle;
                    break;
            }
        }

        public void Draw(SpriteBatch sb) 
        {
            world.Draw(sb);
            sb.Draw(currFrame, pRect, null, Color.White, .0f, Vector2.Zero, Flipper, .0f);
            //sb.DrawString(font, isCrouchingUp.ToString()+ " - "+ isCrouching.ToString() + " - " +isCrouchingDown.ToString(), new Vector2(0, 0), Color.White);
            sb.DrawString(font, oText, new Vector2(pRect.X, pRect.Y - 30), Color.White);
            
            if (inventory.Count > 0)
            {
                sb.DrawString(font, "Holding: " + inventory[0].Name, new Vector2(0, 20), Color.White);
            }
        }
    }
}
