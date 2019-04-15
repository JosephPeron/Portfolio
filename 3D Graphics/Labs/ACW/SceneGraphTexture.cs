using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace Labs.ACW
{
    class SceneGraphTexture : SceneGraphGroup
    {
        int mTextureID;
        public SceneGraphTexture(int pTextureID)
        {
            mTextureID = pTextureID;
        }

        public int GetTextureID()
        {
            return mTextureID;
        }
    }
}
