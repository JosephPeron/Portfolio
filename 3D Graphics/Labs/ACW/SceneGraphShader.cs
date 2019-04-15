using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using Labs.Utility;

namespace Labs.ACW
{
    class SceneGraphShader : SceneGraphGroup
    {
        ShaderUtility mShader;
        public SceneGraphShader(ShaderUtility pShader)
        {
            mShader = pShader;
        }

        public int GetShaderID()
        {
            return mShader.ShaderProgramID;
        }
    }
}
