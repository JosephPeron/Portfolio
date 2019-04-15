using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace Labs.ACW
{
    class SceneGraphTransformation : SceneGraphGroup
    {
        Matrix4 LTM;
        public SceneGraphTransformation(Matrix4 pLTM)
        {
            LTM = pLTM;
        }

        public Matrix4 GetLTM()
        {
            return LTM;
        }

        public void SetLTM(Matrix4 pLTM)
        {
            LTM = pLTM;
        }
    }
}
