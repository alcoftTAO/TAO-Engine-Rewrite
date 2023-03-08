using System;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace TAO.Engine.OpenGL.OpenGLCore
{
    public class TObject
    {
        private static List<TObject> Objects = new List<TObject>();
        public readonly int ID = GetID();
        public TVector3 Position = TVector3.Zero;
        public TVector3 Scale = TVector3.One;
        public TVector3[] Vertices = new TVector3[0];
        public Color4[] Colors = new Color4[0];
        public string Texture = "";
        public TVector2[] TextureCoords = new TVector2[0];
        public PrimitiveType RenderMode = PrimitiveType.Polygon;

        public static TObject Triangle2D()
        {
            return new TObject()
            {
                Vertices = new TVector3[]
                {
                    new TVector3(-0.5f, -0.5f),
                    new TVector3(0.5f, -0.5f),
                    new TVector3(0, 0.5f)
                },
                TextureCoords = new TVector2[]
                {
                    new TVector2(0, 0),
                    new TVector2(1, 0),
                    new TVector2(0.5f, 1)
                },
                RenderMode = PrimitiveType.Triangles
            };
        }

        public static TObject Cube2D()
        {
            return new TObject()
            {
                Vertices = new TVector3[]
                {
                    new TVector3(-0.5f, -0.5f),
                    new TVector3(-0.5f, 0.5f),
                    new TVector3(0.5f, 0.5f),
                    new TVector3(0.5f, -0.5f)
                },
                TextureCoords = new TVector2[]
                {
                    new TVector2(0, 0),
                    new TVector2(0, 1),
                    new TVector2(1, 1),
                    new TVector2(1, 0)
                },
                RenderMode = PrimitiveType.Quads
            };
        }

        private static int GetID()
        {
            int ID = new Random().Next(0, 99999);

            while (Exists(ID))
            {
                ID = GetID();
            }

            return ID;
        }

        public static TObject GetByID(int ID)
        {
            for (int i = 0; i < Objects.Count; i++)
            {
                if (Objects[i].ID == ID)
                {
                    return Objects[i];
                }
            }

            return null;
        }

        public static bool Exists(int ID)
        {
            for (int i = 0; i < Objects.Count; i++)
            {
                if (Objects[i].ID == ID)
                {
                    return true;
                }
            }

            return false;
        }

        public static TObject FromModel(TModelData Data)
        {
            TObject obj = new TObject();

            obj.Vertices = Data.Vertices;
            obj.Colors = OpenGLMath.VectorToColor(Data.Colors);
            obj.TextureCoords = Data.TextureCoords;
            obj.Texture = Data.TextureName;

            if (Data.RenderMode == "quads")
            {
                obj.RenderMode = PrimitiveType.Quads;
            }
            else if (Data.RenderMode == "triangles")
            {
                obj.RenderMode = PrimitiveType.Triangles;
            }
            else if (Data.RenderMode == "lines")
            {
                obj.RenderMode = PrimitiveType.Lines;
            }
            else if (Data.RenderMode == "points")
            {
                obj.RenderMode = PrimitiveType.Points;
            }
            else if (Data.RenderMode == "patches")
            {
                obj.RenderMode = PrimitiveType.Patches;
            }
            else
            {
                obj.RenderMode = PrimitiveType.Polygon;
            }

            return obj;
        }

        public static TObject FromModel(string Name, bool FromMods = false)
        {
            return FromModel(ModelImportation.GetModelData(Name, FromMods));
        }

        public void ChangeColor(Color4 ColorToChange)
        {
            Colors = new Color4[Vertices.Length];

            for (int i = 0; i < Colors.Length; i++)
            {
                Colors[i] = ColorToChange;
            }
        }

        public bool IsMouseOver(TWindow window)
        {
            return false;
        }
    }
}