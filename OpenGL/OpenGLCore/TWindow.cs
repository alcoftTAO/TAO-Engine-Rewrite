﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using StbImageSharp;

namespace TAO.Engine.OpenGLCore
{
    public class TWindow : GameWindow
    {
        public Action OnRenderFrameAction = null;
        public Action OnLoadAction = null;
        public Action OnUnloadAction = null;
        public Action OnResizeAction = null;
        public Action<KeyboardKeyEventArgs> OnKeyDownAction = null;
        public Action<KeyboardKeyEventArgs> OnKeyUpAction = null;
        public List<EnableCap> EnableOnStart = new List<EnableCap>()
        {
            EnableCap.AlphaTest,
            EnableCap.Blend,
            EnableCap.DepthTest,
            EnableCap.Fog,
            EnableCap.Texture2D,
            EnableCap.TextureCubeMap
        };
        public List<TObject> Objs = new List<TObject>();
        public Dictionary<string, TTexture> Textures = new Dictionary<string, TTexture>();
        public Color4 BackgroundColor = Color4.CornflowerBlue;
        public Matrix4 Camera = Matrix4.Identity;
        public float DeltaTime = 0;
        public bool FullScreen = false;
        public bool WideScreen = true;
        public TVector2 MousePosition = TVector2.Zero;
        public TVector2 MousePosition01 = TVector2.Zero;

        public void BasicStart(VSyncMode VSync = VSyncMode.On)
        {
            //Setting the parameters
            OSInfo info = OSInfo.GetClientInfo();

            this.VSync = VSync;

            if (this.Title.Trim() == "OpenTK Game Window")
            {
                this.Title = "OpenGL Core | " + info.Platform + " | " + info.ProcessArchitecture;
            }

            //Starting the window
            Run();
        }

        protected void LoadTexture(string TextureName)
        {
            TTexture texture;

            if (Textures.TryGetValue(TextureName, out texture))
            {
                GL.BindTexture(TextureTarget.Texture2D, texture.ID);
                StbImage.stbi_set_flip_vertically_on_load(1);

                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)texture.MinFilter);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)texture.MagFilter);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)texture.WrapMode);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)texture.WrapMode);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapR, (int)texture.WrapMode);

                GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, texture.Result.Width, texture.Result.Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, texture.Result.Data);
                GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
            }
        }

        protected void UnloadTexture(string TextureName)
        {
            TTexture texture;

            if (Textures.TryGetValue(TextureName, out texture))
            {
                GL.BindTexture(TextureTarget.Texture2D, texture.ID);
                GL.ClearTexImage(texture.ID, 0, PixelFormat.Rgba, PixelType.UnsignedByte, texture.Result.Data);
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            //Flip the textures
            StbImage.stbi_set_flip_vertically_on_load(1);

            //Check data
            DiskData.CheckData();

            //Load textures and other assets
            //Assets
            string[] textures = DiskData.GetAssetsFromDirectory("Assets/Textures/");

            for (int i = 0; i < textures.Length; i++)
            {
                if (!Textures.ContainsKey(textures[i]))
                {
                    Log.WriteMessage("Loading texture from 'Assets' (" + textures[i] + ")...");
                    Textures.Add(textures[i], new TTexture("Assets/Textures/" + textures[i]));
                }
            }

            //Mods (if allowed)
            if (DiskData.AllowMods)
            {
                string[] texturesMods = DiskData.GetAssetsFromDirectory("Mods/Textures/");

                for (int i = 0; i < texturesMods.Length; i++)
                {
                    if (!Textures.ContainsKey(texturesMods[i]))
                    {
                        Log.WriteMessage("Loading texture from 'Mods' (" + texturesMods[i] + ")...");
                        Textures.Add(texturesMods[i], new TTexture("Mods/Textures/" + texturesMods[i]));
                    }
                }
            }

            //Enabling everything specified
            Log.WriteMessage("Loading OpenGL addons...");

            for (int i = 0; i < EnableOnStart.Count; i++)
            {
                GL.Enable(EnableOnStart[i]);
            }

            //Loading the camera
            Log.WriteMessage("Loading camera Matrix4...");
            GL.LoadMatrix(ref Camera);

            //Execute the action
            OSInfo.ExecuteAction(OnLoadAction);
        }

        protected override void OnUnload(EventArgs e)
        {
            base.OnUnload(e);

            //Execute the action
            OSInfo.ExecuteAction(OnUnloadAction);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);

            Log.WriteMessage("Closing window...");
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            Log.WriteMessage("Window closed!");
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            //Changing the viewport size on resizing
            GL.Viewport(0, 0, Width, Height);

            //Execute the action
            OSInfo.ExecuteAction(OnResizeAction);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            //Delta time
            DeltaTime = (float)e.Time;

            //Check full screen
            if (FullScreen)
            {
                this.WindowState = WindowState.Fullscreen;
                this.WindowBorder = WindowBorder.Hidden;
            }
            else
            {
                this.WindowState = WindowState.Normal;
                this.WindowBorder = WindowBorder.Resizable;
            }

            //Clear and set background color
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.ClearColor(BackgroundColor);

            //Objects drawing
            for (int obj = 0; obj < Objs.Count; obj++)
            {
                //Load texture
                if (Objs[obj].Texture.Trim() != "")
                {
                    LoadTexture(Objs[obj].Texture);
                }

                //Push the camera
                GL.PushMatrix();

                //Translate and scale the object
                GL.Translate(OpenGLMath.TVectorToVector(Objs[obj].Position));
                GL.Scale(OpenGLMath.TVectorToVector(Objs[obj].Scale));
                //Begin drawing
                GL.Begin(Objs[obj].RenderMode);

                //Drawing vertices
                for (int vert = 0; vert < Objs[obj].Vertices.Length; vert++)
                {
                    //Calculating vert vector and color
                    Vector3 v = OpenGLMath.TVectorToVector(Objs[obj].Vertices[vert]);
                    Color4 c;

                    if (Objs[obj].Colors.Length > vert)
                    {
                        c = Objs[obj].Colors[vert];
                    }
                    else
                    {
                        c = Color4.White;
                    }

                    //Drawing the vert with the color and texture coords
                    if (Objs[obj].Texture.Trim() != "")
                    {
                        TVector2 tc;

                        if (Objs[obj].TextureCoords.Length > vert)
                        {
                            tc = Objs[obj].TextureCoords[vert];
                        }
                        else
                        {
                            tc = TVector2.Zero;
                        }

                        GL.TexCoord2(OpenGLMath.TVectorToVector(tc));
                    }

                    GL.Color4(c);
                    GL.Vertex3(v);
                }

                //Stop drawing
                GL.End();

                //Pop the camera
                GL.PopMatrix();

                //Unload texture
                if (Objs[obj].Texture.Trim() != "")
                {
                    UnloadTexture(Objs[obj].Texture);
                }
            }

            //Execute the action
            OSInfo.ExecuteAction(OnRenderFrameAction, false);

            //Swap buffers and flush
            SwapBuffers();
            GL.Flush();
        }

        protected override void OnKeyDown(KeyboardKeyEventArgs e)
        {
            base.OnKeyDown(e);

            //Close the program if the combination is ALT+F4
            if (e.Alt && e.Key == Key.F4)
            {
                Close();
            }

            //Execute the action
            OSInfo.ExecuteAction(OnKeyDownAction, e);
        }

        protected override void OnKeyUp(KeyboardKeyEventArgs e)
        {
            base.OnKeyUp(e);

            //Execute the action
            OSInfo.ExecuteAction(OnKeyUpAction, e);
        }

        protected override void OnMouseMove(MouseMoveEventArgs e)
        {
            base.OnMouseMove(e);

            //Calculate and set the mouse position
            /*
             *          |            
             *    -+    |     ++
             *          |
             *  --------|--------
             *          |
             *    --    |     +-
             *          |
            */
            MousePosition = new TVector2(e.X - Width / 2, e.Y - Height / 2);
            MousePosition01 = (MousePosition + new TVector2(Width, Height) / 2) / new TVector2(Width, Height);
        }
    }
}