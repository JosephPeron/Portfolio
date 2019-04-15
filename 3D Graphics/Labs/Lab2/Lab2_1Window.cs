using Labs.Utility;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using System;

namespace Labs.Lab2
{
    class Lab2_1Window : GameWindow
    {        
        private int[] mTriangleBufferObjectIDArray = new int[2];
        private int[] mSquareBufferObjectIDArray = new int[2];
        private int[] mVertexArrayObjectIDs = new int[2];
        private ShaderUtility mShader;

        public Lab2_1Window()
            : base(
                800, // Width
                600, // Height
                GraphicsMode.Default,
                "Lab 2_1 Linking to Shaders and VAOs",
                GameWindowFlags.Default,
                DisplayDevice.Default,
                3, // major
                3, // minor
                GraphicsContextFlags.ForwardCompatible
                )
        {
        }

        protected override void OnLoad(EventArgs e)
        {
            GL.Enable(EnableCap.DepthTest);
            GL.ClearColor(Color4.CadetBlue);

            float[] verticesTriangle = new float[] { -0.8f, 0.8f, 0.4f, 1.0f, 0.0f, 0.0f,
                                                     -0.6f, -0.4f, 0.4f, 0.0f, 1.0f, 0.0f,
                                                      0.2f, 0.2f, 0.4f, 0.0f, 0.0f, 1.0f};

            uint[] indicesTriangle = new uint[] { 0, 1, 2};

            float[] verticesSquare = new float[]  { -0.2f, -0.4f, 0.2f, 1.0f, 1.0f, 0.0f,
                                                     0.8f, -0.4f, 0.2f, 1.0f, 0.0f, 1.0f,
                                                     0.8f, 0.6f, 0.2f, 1.0f, 1.0f, 0.0f,
                                                    -0.2f, 0.6f, 0.2f, 1.0f, 0.0f, 1.0f};



            uint[] indicesSquare = new uint[] { 0, 1, 2, 3};

            #region Shader Loading Code

            mShader = new ShaderUtility(@"Lab2/Shaders/vLab21.vert", @"Lab2/Shaders/fSimple.frag");
            int vColourLocation = GL.GetAttribLocation(mShader.ShaderProgramID, "vColour");
            GL.EnableVertexAttribArray(vColourLocation);
            int vPositionLocation = GL.GetAttribLocation(mShader.ShaderProgramID, "vPosition");
            GL.EnableVertexAttribArray(vPositionLocation);

            #endregion

            GL.GenVertexArrays(2, mVertexArrayObjectIDs);
            GL.GenBuffers(2, mTriangleBufferObjectIDArray);
            GL.BindBuffer(BufferTarget.ArrayBuffer, mTriangleBufferObjectIDArray[0]);
            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(verticesSquare.Length * sizeof(float)), verticesSquare, BufferUsageHint.StaticDraw);

            int sizeTriangle;
            GL.GetBufferParameter(BufferTarget.ArrayBuffer, BufferParameterName.BufferSize, out sizeTriangle);

            if (verticesSquare.Length * sizeof(float) != sizeTriangle)
            {
                throw new ApplicationException("Vertex data not loaded onto graphics card correctly");
            }

            GL.BindBuffer(BufferTarget.ElementArrayBuffer, mTriangleBufferObjectIDArray[1]);
            GL.BufferData(BufferTarget.ElementArrayBuffer, (IntPtr)(indicesSquare.Length * sizeof(int)), indicesSquare, BufferUsageHint.StaticDraw);

            GL.GetBufferParameter(BufferTarget.ElementArrayBuffer, BufferParameterName.BufferSize, out sizeTriangle);

            if (indicesSquare.Length * sizeof(int) != sizeTriangle)
            {
                throw new ApplicationException("Index data not loaded onto graphics card correctly");
            }

            GL.BindVertexArray(mVertexArrayObjectIDs[0]);
            GL.VertexAttribPointer(vPositionLocation, 3, VertexAttribPointerType.Float, false, 6 *
            sizeof(float), 0);
            GL.VertexAttribPointer(vColourLocation, 3, VertexAttribPointerType.Float, false, 6 *
            sizeof(float), 3 * sizeof(float));
            GL.EnableVertexAttribArray(vColourLocation);
            GL.EnableVertexAttribArray(vPositionLocation);

            GL.GenBuffers(2, mSquareBufferObjectIDArray);
            GL.BindBuffer(BufferTarget.ArrayBuffer, mSquareBufferObjectIDArray[0]);
            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(verticesSquare.Length * sizeof(float)), verticesSquare, BufferUsageHint.StaticDraw);

            int sizeSquare;
            GL.GetBufferParameter(BufferTarget.ArrayBuffer, BufferParameterName.BufferSize, out sizeSquare);

            if (verticesSquare.Length * sizeof(float) != sizeSquare)
            {
                throw new ApplicationException("Vertex data not loaded onto graphics card correctly");
            }

            GL.BindBuffer(BufferTarget.ElementArrayBuffer, mSquareBufferObjectIDArray[1]);
            GL.BufferData(BufferTarget.ElementArrayBuffer, (IntPtr)(indicesSquare.Length * sizeof(int)), indicesSquare, BufferUsageHint.StaticDraw);

            GL.GetBufferParameter(BufferTarget.ElementArrayBuffer, BufferParameterName.BufferSize, out sizeSquare);

            if (indicesSquare.Length * sizeof(int) != sizeSquare)
            {
                throw new ApplicationException("Index data not loaded onto graphics card correctly");
            }


            GL.BindVertexArray(mVertexArrayObjectIDs[1]);
            GL.VertexAttribPointer(vPositionLocation, 3, VertexAttribPointerType.Float, false, 6 *
            sizeof(float), 0);
            GL.VertexAttribPointer(vColourLocation, 3, VertexAttribPointerType.Float, false, 6 *
            sizeof(float), 3 * sizeof(float));
            GL.EnableVertexAttribArray(vColourLocation);
            GL.EnableVertexAttribArray(vPositionLocation);

            base.OnLoad(e);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.BindVertexArray(mVertexArrayObjectIDs[1]);
            GL.DrawElements(PrimitiveType.TriangleFan, 4, DrawElementsType.UnsignedInt, 0);

            GL.BindVertexArray(mVertexArrayObjectIDs[0]);
            GL.DrawElements(PrimitiveType.Triangles, 3, DrawElementsType.UnsignedInt, 0);

            GL.BindVertexArray(0);
            this.SwapBuffers();
        }


        protected override void OnUnload(EventArgs e)
        {
            base.OnUnload(e);
            GL.DeleteBuffers(2, mSquareBufferObjectIDArray);
            GL.DeleteBuffers(2, mTriangleBufferObjectIDArray);;
            GL.BindVertexArray(0);
            GL.DeleteVertexArrays(2, mVertexArrayObjectIDs);
            GL.UseProgram(0);
            mShader.Delete();
        }
    }
}
