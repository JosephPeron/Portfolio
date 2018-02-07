class SceneGraphNode {

    constructor() {
        
    }

    accept(visitor) {
        visitor.visit(this);
    }
}