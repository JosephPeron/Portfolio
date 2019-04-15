using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labs.ACW
{
    class SceneGraphGroup : SceneGraphNode
    {
        List<SceneGraphNode> mChildren;
        public SceneGraphGroup()
        {
            mChildren = new List<SceneGraphNode>();
        }

        public void AddChild(SceneGraphNode pParameter)
        {
            mChildren.Add(pParameter);
        }

        public int GetChildCount()
        {
            return mChildren.Count;
        }

        public SceneGraphNode GetChild(int index)
        {
            return mChildren[index];
        }
    }
}
