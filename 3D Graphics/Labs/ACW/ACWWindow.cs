using Labs.Utility;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace Labs.ACW
{
    public class ACWWindow : GameWindow
    {
        private int[] mVBO_IDs = new int[13];
        private int[] mVAO_IDs = new int[7];
        private ShaderUtility mLightPositionShader, mLightDirectionShader, mTextureShader;
        private ModelUtility mSphereModelUtility, mBunnyModelUtility, mArmadilloModelUtility, mCylinderModelUtility;
        private Matrix4 mView, mSphereModel, mGroundModel, mBunnyModel, mArmadilloModel, mCylinderModel, mCubeModel;
        private Matrix4 catScaleMatrix;
        private Vector4 lightPosition;
        private bool fixedCamera = false;
        private int mTexture_ID, mTexture2_ID, mTexture3_ID;
        private float catScale = 2.5f;
        float scaleChange = 0.02f;
        float translateChange = 0.1f;
        float bunnyYPosition = 1.6f;
        SceneGraphGroup mScene;
        RenderVisitor mRenderVisitor;
        SceneGraphTransformation cubeScale, armadilloRotate, bunnyPosition;

        public ACWWindow()
            : base(
                1280, // Width
                720, // Height
                GraphicsMode.Default,
                "Assessed Coursework",
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
            // Set some GL state
            //Changed the clear colour to see some silhouettes
            GL.ClearColor(Color4.DarkKhaki);
            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.CullFace);

            GL.GenVertexArrays(mVAO_IDs.Length, mVAO_IDs);
            GL.GenBuffers(mVBO_IDs.Length, mVBO_IDs);

            mScene = new SceneGraphGroup();
            mRenderVisitor = new RenderVisitor();

            mView = Matrix4.CreateTranslation(0, -2.5f, -4.0f);
            mGroundModel = Matrix4.CreateTranslation(0f, 0f, -5f);
            mSphereModel = Matrix4.CreateTranslation(0f, 0f, 0f);
            mBunnyModel = Matrix4.CreateTranslation(4.51f, 1.6f, -5.3f);
            mArmadilloModel = Matrix4.CreateTranslation(0f, 3f, -5f);
            mCylinderModel = Matrix4.CreateTranslation(4f, 0.3f, -5f);
            mCubeModel = Matrix4.CreateTranslation(-5f, 5f, 7.5f);
            catScaleMatrix = Matrix4.CreateScale(1);

            #region Vertice and Indice Arrays

            float[] Floorvertices = new float[] {-10, 0, -10, 0, 1,
                                                 -10, 0,  10, 0, 0,
                                                  10, 0,  10, 1, 0,
                                                  10, 0, -10, 1, 1 };

            float[] wallnRoofVertices = new float[] {-10,  0, 10,  0, 0,
                                                     -10,  0, 10,  1, 0,
                                                     -10,  0, -10,  0, 0,
                                                     -10,  0, -10,  1, 0,
                                                     10,  0, -10,  0, 0,
                                                     10,  0, -10,  1, 0,
                                                     10,  0, 10,  0, 0,
                                                     10,  0, 10,  1, 0,
                                                     -10, 10, 10,  0, 1,
                                                     -10, 10, 10,  1, 1,
                                                     -10, 10, 10,  0, 0,
                                                     -10, 10, -10,  0, 1,
                                                     -10, 10, -10,  1, 1,
                                                     -10, 10, -10,  0, 1,
                                                     10, 10, -10,  0, 1,
                                                     10, 10, -10,  1, 1,
                                                     10, 10, -10,  1, 1,
                                                     10, 10, 10,  0, 1,
                                                     10, 10, 10,  1, 1,
                                                     10, 10, 10,  1, 0
                                                     };

                                                     

            uint[] wallnRoofIndices = new uint[] {11,2,15,
                                                  2,5,15,
                                                  14,4,18,
                                                  4,7,18,
                                                  17,6,9,
                                                  6,1,9,
                                                  8,0,12,
                                                  0,3,12,
                                                  10,13,19,
                                                  13,16,19
                                                  };

            float[] CubeverticesTexture = new float[] {-0.5f,-0.5f,-0.5f,1,0,
                                                       -0.5f,-0.5f,-0.5f,0,0,
                                                       -0.5f,-0.5f,-0.5f,0,1,
                                                        0.5f,-0.5f,-0.5f,1,0,
                                                        0.5f,-0.5f,-0.5f,0,0,
                                                        0.5f,-0.5f,-0.5f,1,1,
                                                       -0.5f,-0.5f,0.5f,0,0,
                                                       -0.5f,-0.5f,0.5f,1,0,
                                                       -0.5f,-0.5f,0.5f,0,0,
                                                        0.5f,-0.5f,0.5f,1,0,
                                                        0.5f,-0.5f,0.5f,0,0,
                                                        0.5f,-0.5f,0.5f,1,0,
                                                       -0.5f,0.5f,-0.5f,1,1,
                                                       -0.5f,0.5f,-0.5f,0,1,
                                                       -0.5f,0.5f,-0.5f,0,1,
                                                        0.5f,0.5f,-0.5f,1,1,
                                                        0.5f,0.5f,-0.5f,0,1,
                                                        0.5f,0.5f,-0.5f,1,1,
                                                       -0.5f,0.5f,0.5f,0,1,
                                                       -0.5f,0.5f,0.5f,1,1,
                                                       -0.5f,0.5f,0.5f,0,0,
                                                        0.5f,0.5f,0.5f,1,1,
                                                        0.5f,0.5f,0.5f,0,1,
                                                        0.5f,0.5f,0.5f,1,0 }; 

            uint[] Cubeindices = new uint[] {18,6,21,
                                            21,6,9,
                                            22,10,15,
                                            15,10,3,
                                            16,4,12,
                                            12,4,0,
                                            13,1,19,
                                            19,1,7,
                                            2,5,8,
                                            8,5,11,
                                            20,17,14,
                                            23,17,20};

            #endregion

            #region Lighting Positional
            mLightPositionShader = new ShaderUtility(@"ACW\Shaders\vLighting.vert", @"ACW\Shaders\fLighting.frag");
            GL.UseProgram(mLightPositionShader.ShaderProgramID);

            int vPositionLocationLightPos = GL.GetAttribLocation(mLightPositionShader.ShaderProgramID, "vPosition");
            int vNormalLocationLightPos = GL.GetAttribLocation(mLightPositionShader.ShaderProgramID, "vNormal");

            int uViewLightPos = GL.GetUniformLocation(mLightPositionShader.ShaderProgramID, "uView");
            GL.UniformMatrix4(uViewLightPos, true, ref mView);

            int uLightPositionLocation = GL.GetUniformLocation(mLightPositionShader.ShaderProgramID, "uLightPosition");
            Vector4 normalisedLightPosition;
            lightPosition = new Vector4(2.0f, -1.0f, -10f, 1.0f);
            Vector4.Normalize(ref lightPosition, out normalisedLightPosition);
            GL.Uniform4(uLightPositionLocation, lightPosition);

            #region Bunny
            mBunnyModelUtility = ModelUtility.LoadModel(@"Utility/Models/bunny.obj");

            GL.BindVertexArray(mVAO_IDs[4]);
            GL.BindBuffer(BufferTarget.ArrayBuffer, mVBO_IDs[7]);
            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(mBunnyModelUtility.Vertices.Length * sizeof(float)), mBunnyModelUtility.Vertices, BufferUsageHint.StaticDraw);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, mVBO_IDs[8]);
            GL.BufferData(BufferTarget.ElementArrayBuffer, (IntPtr)(mBunnyModelUtility.Indices.Length * sizeof(float)), mBunnyModelUtility.Indices, BufferUsageHint.StaticDraw);

            int bunnysize;
            GL.GetBufferParameter(BufferTarget.ArrayBuffer, BufferParameterName.BufferSize, out bunnysize);
            if (mBunnyModelUtility.Vertices.Length * sizeof(float) != bunnysize)
            {
                throw new ApplicationException("Vertex data not loaded onto graphics card correctly");
            }

            GL.GetBufferParameter(BufferTarget.ElementArrayBuffer, BufferParameterName.BufferSize, out bunnysize);
            if (mBunnyModelUtility.Indices.Length * sizeof(float) != bunnysize)
            {
                throw new ApplicationException("Index data not loaded onto graphics card correctly");
            }

            GL.EnableVertexAttribArray(vPositionLocationLightPos);
            GL.VertexAttribPointer(vPositionLocationLightPos, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), 0);

            GL.EnableVertexAttribArray(vNormalLocationLightPos);
            GL.VertexAttribPointer(vNormalLocationLightPos, 3, VertexAttribPointerType.Float, true, 6 * sizeof(float), 3 * sizeof(float));

            bunnyPosition = new SceneGraphTransformation(mBunnyModel);
            SceneGraphShader bunnyShader = new SceneGraphShader(mLightPositionShader);
            SceneGraphDrawModel bunnyDrawModel = new SceneGraphDrawModel(mBunnyModelUtility, PrimitiveType.Triangles, mVAO_IDs[4]);
            bunnyPosition.AddChild(bunnyShader);
            bunnyShader.AddChild(bunnyDrawModel);

            #endregion

            #region Armadillo

            mArmadilloModelUtility = ModelUtility.LoadModel(@"Utility/Models/model.bin");

            GL.BindVertexArray(mVAO_IDs[5]);
            GL.BindBuffer(BufferTarget.ArrayBuffer, mVBO_IDs[9]);
            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(mArmadilloModelUtility.Vertices.Length * sizeof(float)), mArmadilloModelUtility.Vertices, BufferUsageHint.StaticDraw);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, mVBO_IDs[10]);
            GL.BufferData(BufferTarget.ElementArrayBuffer, (IntPtr)(mArmadilloModelUtility.Indices.Length * sizeof(float)), mArmadilloModelUtility.Indices, BufferUsageHint.StaticDraw);

            int F16size;
            GL.GetBufferParameter(BufferTarget.ArrayBuffer, BufferParameterName.BufferSize, out F16size);
            if (mArmadilloModelUtility.Vertices.Length * sizeof(float) != F16size)
            {
                throw new ApplicationException("Vertex data not loaded onto graphics card correctly");
            }

            GL.GetBufferParameter(BufferTarget.ElementArrayBuffer, BufferParameterName.BufferSize, out F16size);
            if (mArmadilloModelUtility.Indices.Length * sizeof(float) != F16size)
            {
                throw new ApplicationException("Index data not loaded onto graphics card correctly");
            }

            GL.EnableVertexAttribArray(vPositionLocationLightPos);
            GL.VertexAttribPointer(vPositionLocationLightPos, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), 0);

            GL.EnableVertexAttribArray(vNormalLocationLightPos);
            GL.VertexAttribPointer(vNormalLocationLightPos, 3, VertexAttribPointerType.Float, true, 6 * sizeof(float), 3 * sizeof(float));

            Matrix4 scaleArmadillo = Matrix4.CreateScale(3);
            Matrix4 rotateArmadillo = Matrix4.CreateRotationY((float)Math.PI * -0.5f);
            SceneGraphTransformation armadilloPosition = new SceneGraphTransformation(mArmadilloModel);
            SceneGraphTransformation armadilloScale = new SceneGraphTransformation(scaleArmadillo);
            armadilloRotate = new SceneGraphTransformation(rotateArmadillo);
            SceneGraphShader armadilloShader = new SceneGraphShader(mLightPositionShader);
            SceneGraphDrawModel armadilloDrawModel = new SceneGraphDrawModel(mArmadilloModelUtility, PrimitiveType.Triangles, mVAO_IDs[5]);

            armadilloPosition.AddChild(armadilloScale);
            armadilloScale.AddChild(armadilloRotate);
            armadilloRotate.AddChild(armadilloShader);
            armadilloShader.AddChild(armadilloDrawModel);

            #endregion

            #region Cylinder 
            mCylinderModelUtility = ModelUtility.LoadModel(@"Utility/Models/cylinder.bin");

            GL.BindVertexArray(mVAO_IDs[6]);
            GL.BindBuffer(BufferTarget.ArrayBuffer, mVBO_IDs[11]);
            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(mCylinderModelUtility.Vertices.Length * sizeof(float)), mCylinderModelUtility.Vertices, BufferUsageHint.StaticDraw);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, mVBO_IDs[12]);
            GL.BufferData(BufferTarget.ElementArrayBuffer, (IntPtr)(mCylinderModelUtility.Indices.Length * sizeof(float)), mCylinderModelUtility.Indices, BufferUsageHint.StaticDraw);

            int cylindersize;
            GL.GetBufferParameter(BufferTarget.ArrayBuffer, BufferParameterName.BufferSize, out cylindersize);
            if (mCylinderModelUtility.Vertices.Length * sizeof(float) != cylindersize)
            {
                throw new ApplicationException("Vertex data not loaded onto graphics card correctly");
            }

            GL.GetBufferParameter(BufferTarget.ElementArrayBuffer, BufferParameterName.BufferSize, out cylindersize);
            if (mCylinderModelUtility.Indices.Length * sizeof(float) != cylindersize)
            {
                throw new ApplicationException("Index data not loaded onto graphics card correctly");
            }

            GL.EnableVertexAttribArray(vPositionLocationLightPos);
            GL.VertexAttribPointer(vPositionLocationLightPos, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), 0);

            GL.EnableVertexAttribArray(vNormalLocationLightPos);
            GL.VertexAttribPointer(vNormalLocationLightPos, 3, VertexAttribPointerType.Float, true, 6 * sizeof(float), 3 * sizeof(float));

            SceneGraphTransformation cylinderPosition = new SceneGraphTransformation(mCylinderModel);
            SceneGraphShader cylinderShader = new SceneGraphShader(mLightPositionShader);
            SceneGraphDrawModel cylinderDrawModel = new SceneGraphDrawModel(mCylinderModelUtility, PrimitiveType.Triangles, mVAO_IDs[6]);

            cylinderPosition.AddChild(cylinderShader);
            cylinderShader.AddChild(cylinderDrawModel);

            #endregion

            #endregion

            #region Lighting Directional

            mLightDirectionShader = new ShaderUtility(@"ACW\Shaders\vPassThrough.vert", @"ACW\Shaders\fPassThrough.frag");
            GL.UseProgram(mLightDirectionShader.ShaderProgramID);

            int vPositionLocationLightDirection = GL.GetAttribLocation(mLightDirectionShader.ShaderProgramID, "vPosition");
            int vNormalLocationDirection = GL.GetAttribLocation(mLightDirectionShader.ShaderProgramID, "vNormal");

            int uViewLightDirection = GL.GetUniformLocation(mLightDirectionShader.ShaderProgramID, "uView");
            GL.UniformMatrix4(uViewLightDirection, true, ref mView);

            int uLightDirectionLocation = GL.GetUniformLocation(mLightDirectionShader.ShaderProgramID, "uLightDirection");
            Vector3 normalisedLightDirection, lightDirection = new Vector3(-1, -1, -1);
            Vector3.Normalize(ref lightDirection, out normalisedLightDirection);
            GL.Uniform3(uLightDirectionLocation, normalisedLightDirection);

            #region Sphere
            mSphereModelUtility = ModelUtility.LoadModel(@"Utility/Models/sphere.bin");

            GL.BindVertexArray(mVAO_IDs[3]);
            GL.BindBuffer(BufferTarget.ArrayBuffer, mVBO_IDs[5]);
            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(mSphereModelUtility.Vertices.Length * sizeof(float)), mSphereModelUtility.Vertices, BufferUsageHint.StaticDraw);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, mVBO_IDs[6]);
            GL.BufferData(BufferTarget.ElementArrayBuffer, (IntPtr)(mSphereModelUtility.Indices.Length * sizeof(float)), mSphereModelUtility.Indices, BufferUsageHint.StaticDraw);

            int spheresize;
            GL.GetBufferParameter(BufferTarget.ArrayBuffer, BufferParameterName.BufferSize, out spheresize);
            if (mSphereModelUtility.Vertices.Length * sizeof(float) != spheresize)
            {
                throw new ApplicationException("Vertex data not loaded onto graphics card correctly");
            }

            GL.GetBufferParameter(BufferTarget.ElementArrayBuffer, BufferParameterName.BufferSize, out spheresize);
            if (mSphereModelUtility.Indices.Length * sizeof(float) != spheresize)
            {
                throw new ApplicationException("Index data not loaded onto graphics card correctly");
            }

            GL.EnableVertexAttribArray(vPositionLocationLightPos);
            GL.VertexAttribPointer(vPositionLocationLightPos, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), 0);

            GL.EnableVertexAttribArray(vNormalLocationLightPos);
            GL.VertexAttribPointer(vNormalLocationLightPos, 3, VertexAttribPointerType.Float, true, 6 * sizeof(float), 3 * sizeof(float));

            Matrix4 scaleSphere = Matrix4.CreateScale(3);
            SceneGraphTransformation spherePosition = new SceneGraphTransformation(mSphereModel);
            SceneGraphTransformation sphereScale = new SceneGraphTransformation(scaleSphere); 
            SceneGraphShader sphereShader = new SceneGraphShader(mLightDirectionShader);
            SceneGraphDrawModel sphereDrawModel = new SceneGraphDrawModel(mSphereModelUtility, PrimitiveType.Triangles, mVAO_IDs[3]);

            spherePosition.AddChild(sphereScale);
            sphereScale.AddChild(sphereShader);
            sphereShader.AddChild(sphereDrawModel);
            #endregion

            #endregion

            #region Texture

            mTextureShader = new ShaderUtility(@"ACW\Shaders\vTexture.vert", @"ACW\Shaders\fTexture.frag");
            GL.UseProgram(mTextureShader.ShaderProgramID);

            int vTexCoordsLocation = GL.GetAttribLocation(mTextureShader.ShaderProgramID, "vTexCoords");
            int vPositionLocationTexture = GL.GetAttribLocation(mTextureShader.ShaderProgramID, "vPosition");

            int uView = GL.GetUniformLocation(mTextureShader.ShaderProgramID, "uView");
            GL.UniformMatrix4(uView, true, ref mView);

            #region loading texture images
            string RoofFilepath = @"ACW\planks.jpeg";
            if (System.IO.File.Exists(RoofFilepath))
            {
                Bitmap TextureBitmap = new Bitmap(RoofFilepath);
                BitmapData TextureData = TextureBitmap.LockBits(new System.Drawing.Rectangle(0, 0, TextureBitmap.Width, TextureBitmap.Height), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppRgb);

                GL.ActiveTexture(TextureUnit.Texture0);
                GL.GenTextures(1, out mTexture_ID);
                GL.BindTexture(TextureTarget.Texture2D, mTexture_ID);
                GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, TextureData.Width, TextureData.Height, 0, OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, TextureData.Scan0);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
                TextureBitmap.UnlockBits(TextureData);
                TextureBitmap.RotateFlip(RotateFlipType.RotateNoneFlipY);
                int uTextureSamplerLocation = GL.GetUniformLocation(mTextureShader.ShaderProgramID, "uTextureSampler");
                GL.Uniform1(uTextureSamplerLocation, 0);
            }
            else
            {
                throw new Exception("Could not find file " + RoofFilepath);
            }

            string floorFilepath = @"ACW\floor.png";
            if (System.IO.File.Exists(floorFilepath))
            {
                Bitmap TextureBitmap = new Bitmap(floorFilepath);
                BitmapData TextureData = TextureBitmap.LockBits(new System.Drawing.Rectangle(0, 0, TextureBitmap.Width, TextureBitmap.Height), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppRgb);

                GL.ActiveTexture(TextureUnit.Texture0);
                GL.GenTextures(1, out mTexture2_ID);
                GL.BindTexture(TextureTarget.Texture2D, mTexture2_ID);
                GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, TextureData.Width, TextureData.Height, 0, OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, TextureData.Scan0);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
                TextureBitmap.UnlockBits(TextureData);
                TextureBitmap.RotateFlip(RotateFlipType.RotateNoneFlipY);
                int uTextureSamplerLocation = GL.GetUniformLocation(mTextureShader.ShaderProgramID, "uTextureSampler");
                GL.Uniform1(uTextureSamplerLocation, 0);
            }
            else
            {
                throw new Exception("Could not find file " + floorFilepath);
            }

            string CatFilepath = @"ACW\cat.jpg";
            if (System.IO.File.Exists(CatFilepath))
            {
                Bitmap TextureBitmap = new Bitmap(CatFilepath);
                BitmapData TextureData = TextureBitmap.LockBits(new System.Drawing.Rectangle(0, 0, TextureBitmap.Width, TextureBitmap.Height), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppRgb);

                GL.ActiveTexture(TextureUnit.Texture0);
                GL.GenTextures(1, out mTexture3_ID);
                GL.BindTexture(TextureTarget.Texture2D, mTexture3_ID);
                GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, TextureData.Width, TextureData.Height, 0, OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, TextureData.Scan0);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
                TextureBitmap.UnlockBits(TextureData);
                TextureBitmap.RotateFlip(RotateFlipType.RotateNoneFlipY);
                int uTextureSamplerLocation = GL.GetUniformLocation(mTextureShader.ShaderProgramID, "uTextureSampler");
                GL.Uniform1(uTextureSamplerLocation, 0);
            }
            else
            {
                throw new Exception("Could not find file " + CatFilepath);
            }

            #endregion

            #region WallsnRoof
            GL.BindVertexArray(mVAO_IDs[1]);
            GL.BindBuffer(BufferTarget.ArrayBuffer, mVBO_IDs[1]);
            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(wallnRoofVertices.Length * sizeof(float)), wallnRoofVertices, BufferUsageHint.StaticDraw);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, mVBO_IDs[2]);
            GL.BufferData(BufferTarget.ElementArrayBuffer, (IntPtr)(wallnRoofIndices.Length * sizeof(float)), wallnRoofIndices, BufferUsageHint.StaticDraw);

            int wallsize;
            GL.GetBufferParameter(BufferTarget.ArrayBuffer, BufferParameterName.BufferSize, out wallsize);
            if (wallnRoofVertices.Length * sizeof(float) != wallsize)
            {
                throw new ApplicationException("Vertex data not loaded onto graphics card correctly");
            }

            GL.GetBufferParameter(BufferTarget.ElementArrayBuffer, BufferParameterName.BufferSize, out wallsize);
            if (wallnRoofIndices.Length * sizeof(float) != wallsize)
            {
                throw new ApplicationException("Index data not loaded onto graphics card correctly");
            }

            GL.EnableVertexAttribArray(vPositionLocationTexture);
            GL.VertexAttribPointer(vPositionLocationTexture, 3, VertexAttribPointerType.Float, false, 5 * sizeof(float), 0);

            GL.EnableVertexAttribArray(vTexCoordsLocation);
            GL.VertexAttribPointer(vTexCoordsLocation, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));

            SceneGraphShader wallShader = new SceneGraphShader(mTextureShader);
            SceneGraphTexture wallTexture = new SceneGraphTexture(mTexture_ID);
            SceneGraphDrawElements wallDrawElements = new SceneGraphDrawElements(mVAO_IDs[1],wallnRoofIndices.Length, PrimitiveType.Triangles);

            wallShader.AddChild(wallTexture);
            wallTexture.AddChild(wallDrawElements);

            #endregion

            #region Cube
            GL.BindVertexArray(mVAO_IDs[2]);
            GL.BindBuffer(BufferTarget.ArrayBuffer, mVBO_IDs[3]);
            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(CubeverticesTexture.Length * sizeof(float)), CubeverticesTexture, BufferUsageHint.StaticDraw);

            GL.BindBuffer(BufferTarget.ElementArrayBuffer, mVBO_IDs[4]);
            GL.BufferData(BufferTarget.ElementArrayBuffer, (IntPtr)(Cubeindices.Length * sizeof(uint)), Cubeindices, BufferUsageHint.StaticDraw);
            int cubesize;
            GL.GetBufferParameter(BufferTarget.ArrayBuffer, BufferParameterName.BufferSize, out cubesize);
            if (CubeverticesTexture.Length * sizeof(float) != cubesize)
            {
                throw new ApplicationException("Vertex data not loaded onto graphics card correctly");
            }

            GL.GetBufferParameter(BufferTarget.ElementArrayBuffer, BufferParameterName.BufferSize, out cubesize);
            if (Cubeindices.Length * sizeof(uint) != cubesize)
            {
                throw new ApplicationException("Index data not loaded onto graphics card correctly");
            }

            GL.EnableVertexAttribArray(vPositionLocationTexture);
            GL.VertexAttribPointer(vPositionLocationTexture, 3, VertexAttribPointerType.Float, false, 5 * sizeof(float), 0);

            GL.EnableVertexAttribArray(vTexCoordsLocation);
            GL.VertexAttribPointer(vTexCoordsLocation, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));

            Matrix4 rotateCube = Matrix4.CreateRotationX((float)Math.PI);

            SceneGraphTransformation cubePosition = new SceneGraphTransformation(mCubeModel);
            cubeScale = new SceneGraphTransformation(Matrix4.CreateScale(1.1f));
            SceneGraphTransformation cubeRotate = new SceneGraphTransformation(rotateCube);
            SceneGraphShader cubeShader = new SceneGraphShader(mTextureShader);
            SceneGraphTexture cubeTexture = new SceneGraphTexture(mTexture3_ID);
            SceneGraphDrawElements cubeDrawElements = new SceneGraphDrawElements(mVAO_IDs[2], Cubeindices.Length, PrimitiveType.Triangles);

            cubePosition.AddChild(cubeRotate);
            cubeRotate.AddChild(cubeScale);
            cubeScale.AddChild(cubeShader);
            cubeShader.AddChild(cubeTexture);
            cubeTexture.AddChild(cubeDrawElements);

            #endregion

            #region Floor
            GL.BindVertexArray(mVAO_IDs[0]);
            GL.BindBuffer(BufferTarget.ArrayBuffer, mVBO_IDs[0]);
            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(Floorvertices.Length * sizeof(float)), Floorvertices, BufferUsageHint.StaticDraw);

            int size;
            GL.GetBufferParameter(BufferTarget.ArrayBuffer, BufferParameterName.BufferSize, out size);
            if (Floorvertices.Length * sizeof(float) != size)
            {
                throw new ApplicationException("Vertex data not loaded onto graphics card correctly");
            }

            GL.EnableVertexAttribArray(vPositionLocationTexture);
            GL.VertexAttribPointer(vPositionLocationTexture, 3, VertexAttribPointerType.Float, false, 5 * sizeof(float), 0);

            GL.EnableVertexAttribArray(vTexCoordsLocation);
            GL.VertexAttribPointer(vTexCoordsLocation, 2, VertexAttribPointerType.Float, true, 5 * sizeof(float), 3 * sizeof(float));

            //make ground scenegraph 
            SceneGraphTransformation floorPosition = new SceneGraphTransformation(mGroundModel);
            SceneGraphShader floorShader = new SceneGraphShader(mTextureShader);
            SceneGraphTexture floorTexture = new SceneGraphTexture(mTexture2_ID);
            SceneGraphDraw floorDraw = new SceneGraphDraw(PrimitiveType.TriangleFan, mVAO_IDs[0], 4);
            floorPosition.AddChild(floorShader);
            floorShader.AddChild(floorTexture);
            floorTexture.AddChild(floorDraw);

            //add branches to ground position 
            floorPosition.AddChild(bunnyPosition);
            floorPosition.AddChild(armadilloPosition);
            floorPosition.AddChild(cylinderPosition);
            floorPosition.AddChild(spherePosition);
            floorPosition.AddChild(wallShader);
            floorPosition.AddChild(cubePosition);

            #endregion

            #endregion

            mScene.AddChild(floorPosition);

            GL.BindVertexArray(0);

            base.OnLoad(e);
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);
            if (e.KeyChar == 'f')
            {
                if(!fixedCamera)
                {
                    fixedCamera = true;
                    FixTheCamera();
                }
                else
                {
                    fixedCamera = false;
                }
            }

            if (!fixedCamera)
            {
                if (e.KeyChar == 'w')
                {
                    Matrix4 transform = Matrix4.CreateTranslation(0.0f, 0.0f, 0.1f);
                    MoveCamera(transform);
                }
                if (e.KeyChar == 's')
                {
                    Matrix4 transform = Matrix4.CreateTranslation(0.0f, 0.0f, -0.1f);
                    MoveCamera(transform);
                }
                if (e.KeyChar == 'q')
                {
                    Matrix4 transform = Matrix4.CreateTranslation(0.0f, 0.1f, 0.0f);
                    MoveCamera(transform);
                }
                if (e.KeyChar == 'e')
                {
                    Matrix4 transform = Matrix4.CreateTranslation(0.0f, -0.1f, 0.0f);
                    MoveCamera(transform);
                }
                if (e.KeyChar == 'a')
                {
                    Matrix4 transform = Matrix4.CreateRotationY(-0.1f);
                    MoveCamera(transform);
                }
                if (e.KeyChar == 'd')
                {
                    Matrix4 transform = Matrix4.CreateRotationY(0.1f);
                    MoveCamera(transform);
                }
            }
        }

        private void FixTheCamera()
        {
            Matrix4 PositionView = Matrix4.CreateTranslation(new Vector3(-10f, -2f, -5f));
            Matrix4 cameraRotate = Matrix4.CreateRotationY((float)Math.PI * -0.25f);
            mView = PositionView * cameraRotate;
            lightPosition = new Vector4(-8f, -1f, -14f, 1f);

            GL.UseProgram(mLightPositionShader.ShaderProgramID);
            int uView = GL.GetUniformLocation(mLightPositionShader.ShaderProgramID, "uView");
            GL.UniformMatrix4(uView, true, ref mView);
            //update light position
            int uLightPositionLocation = GL.GetUniformLocation(mLightPositionShader.ShaderProgramID, "uLightPosition");
            GL.Uniform4(uLightPositionLocation, lightPosition);

            GL.UseProgram(mLightDirectionShader.ShaderProgramID);
            int uLightDirectionLocation = GL.GetUniformLocation(mLightDirectionShader.ShaderProgramID, "uView");
            GL.UniformMatrix4(uLightDirectionLocation, true, ref mView);

            GL.UseProgram(mTextureShader.ShaderProgramID);
            uView = GL.GetUniformLocation(mTextureShader.ShaderProgramID, "uView");
            GL.UniformMatrix4(uView, true, ref mView);
        }

        private void MoveCamera(Matrix4 transformation)
        {
            mView = mView * transformation; 

            GL.UseProgram(mLightPositionShader.ShaderProgramID);
            int uView = GL.GetUniformLocation(mLightPositionShader.ShaderProgramID, "uView");
            GL.UniformMatrix4(uView, true, ref mView);

            lightPosition = Vector4.Transform(lightPosition, transformation);
            int uLightPositionLocation = GL.GetUniformLocation(mLightPositionShader.ShaderProgramID, "uLightPosition");
            GL.Uniform4(uLightPositionLocation, lightPosition);

            GL.UseProgram(mLightDirectionShader.ShaderProgramID);
            int uViewLightDirection = GL.GetUniformLocation(mLightDirectionShader.ShaderProgramID, "uView");
            GL.UniformMatrix4(uViewLightDirection, true, ref mView);

            GL.UseProgram(mTextureShader.ShaderProgramID);
            uView = GL.GetUniformLocation(mTextureShader.ShaderProgramID, "uView");
            GL.UniformMatrix4(uView, true, ref mView);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            GL.Viewport(this.ClientRectangle);
            if (mLightPositionShader != null)
            {
                GL.UseProgram(mLightPositionShader.ShaderProgramID);
                int uProjectionLocation = GL.GetUniformLocation(mLightPositionShader.ShaderProgramID, "uProjection");
                Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView(1, (float)ClientRectangle.Width / ClientRectangle.Height, 0.5f, 25);
                GL.UniformMatrix4(uProjectionLocation, true, ref projection);
            }
            if (mLightDirectionShader != null)
            {
                GL.UseProgram(mLightDirectionShader.ShaderProgramID);
                int uProjectionLocation = GL.GetUniformLocation(mLightDirectionShader.ShaderProgramID, "uProjection");
                Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView(1, (float)ClientRectangle.Width / ClientRectangle.Height, 0.5f, 25);
                GL.UniformMatrix4(uProjectionLocation, true, ref projection);
            }
            if (mTextureShader != null)
            {
                GL.UseProgram(mTextureShader.ShaderProgramID);
                int uProjectionLocation = GL.GetUniformLocation(mTextureShader.ShaderProgramID, "uProjection");
                Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView(1, (float)ClientRectangle.Width / ClientRectangle.Height, 0.5f, 25);
                GL.UniformMatrix4(uProjectionLocation, true, ref projection);
            }
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            Matrix4 armadilloRotationChange = Matrix4.CreateRotationY(0.01f);
            Matrix4 currentArmadilloRotation = armadilloRotate.GetLTM();
            Matrix4 newArmadilloRotation = currentArmadilloRotation * armadilloRotationChange;
            armadilloRotate.SetLTM(newArmadilloRotation);

            if (catScale <= 1 || catScale >= 5)
            {
                scaleChange *= -1.0f;
            }
            catScale += scaleChange;
            Matrix4 scale = Matrix4.CreateScale(catScale);
            cubeScale.SetLTM(scale);

            Matrix4 currentBunnyPosition = bunnyPosition.GetLTM();
            if(bunnyYPosition <= 1.5f || bunnyYPosition >= 3)
            {
                translateChange *= -1.0f;
            }
            bunnyYPosition += translateChange;

            Vector3 bunnyPos = mBunnyModel.ExtractTranslation();
            bunnyPos.Y = bunnyYPosition;

            Matrix4 newBunnyPos = Matrix4.CreateTranslation(bunnyPos);
            bunnyPosition.SetLTM(newBunnyPos);          
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            mRenderVisitor.Visit(mScene);

            GL.UseProgram(mLightPositionShader.ShaderProgramID);

            GL.BindVertexArray(0);
            this.SwapBuffers();
        }

        protected override void OnUnload(EventArgs e)
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
            GL.BindVertexArray(0);
            GL.DeleteBuffers(mVBO_IDs.Length, mVBO_IDs);
            GL.DeleteVertexArrays(mVAO_IDs.Length, mVAO_IDs);
            mLightPositionShader.Delete();
            mLightDirectionShader.Delete();
            mTextureShader.Delete();
            base.OnUnload(e);
        }
    }
}
