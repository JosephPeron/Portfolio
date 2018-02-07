class SceneGraphGroup extends SceneGraphNode {

    constructor() {
        super();
        this.mChildren = [];
    }

    getType() {
        return "group";
    }

    addChild(pParameter) {
        this.mChildren.push(pParameter);
    }

    getChildNum(pParameter) {
        return this.mChildren.length;
    }

    getSpecificChild(pParameter) {
        return this.mChildren[pParameter];
    }
}