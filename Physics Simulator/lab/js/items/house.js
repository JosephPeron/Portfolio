class House {

    constructor(pPosition, pScale, pAngle) {
        this.setPosition(pPosition);
        this.setScale(pScale);
        this.setAngle(pAngle);
        this.setDirection(-1);
        //always last
        this.constructSceneGraph();
    }

    getPosition() {
        return this.mPos;
    }
    setPosition(pPos) {
        this.mPos = pPos;
    }

    getCentreNode() {
        return this.mCentreNode;
    }
    setCentreNode(pCentreNode) {
        this.mCentreNode = pCentreNode;
    }

    getScale() {
        return this.mScale;
    }
    setScale(pScale) {
        this.mScale = pScale;
    }

    getScaleNode() {
        return this.mScaleNode;
    }
    setScaleNode(pScaleNode) {
        this.mScaleNode = pScaleNode;
    }

    getAngle() {
        return this.mAngle;
    }
    setAngle(pAngle) {
        this.mAngle = pAngle;
    }

    getAngleNode() {
        return this.mAngleNode;
    }
    setAngleNode(pAngleNode) {
        this.mAngleNode = pAngleNode;
    } 
    
    getDirection() {
        return this.mDirection;
    }
    setDirection(pDirection) {
        this.mDirection = pDirection;
    }

    getTopNode() {
        return this.mTopNode;
    }
    setTopNode(pNode) {
        this.mTopNode = pNode;
    }

    constructSceneGraph() {
        //scene graph
        var scale = new Matrix.createScale(new Vector(1, 1, 1));
        var scaleNode = new SceneGraphTransform(scale);
        this.setScaleNode(scaleNode);

        var angle = new Matrix.createRotation(this.getAngle());
        var angleNode = new SceneGraphTransform(angle);
        this.setAngleNode(angleNode);

        var centre = new Matrix.createTranslation(this.getPosition());
        var centreNode = new SceneGraphTransform(centre);
        this.setCentreNode(centreNode);

        var wallNode = this.createWall();
        var roofNode = this.createRoof();
        var doorNode = this.createDoor();
        var leftWindow = this.createLeftWindow();
        var rightWindow = this.createRightWindow();
        var leftBars = this.createLeftBars();
        var rightBars = this.createRightBars();

        scaleNode.addChild(wallNode);
        scaleNode.addChild(roofNode);
        scaleNode.addChild(doorNode);
        scaleNode.addChild(leftWindow);
        scaleNode.addChild(rightWindow);
        scaleNode.addChild(leftBars);
        scaleNode.addChild(rightBars);
        angleNode.addChild(scaleNode);
        centreNode.addChild(angleNode);
        var houseNode = centreNode;

        this.setTopNode(houseNode);
    }

    createWall() {
        var pointsNode = new SceneGraphPolygon([new Vector(100, 0), new Vector(100, 100), new Vector(-100, 100), new Vector(-100, 0)]);
        var stateNode = new SceneGraphState("fillStyle", "#FFFFFF");
        var translateNode = new SceneGraphTransform(new Matrix.createTranslation(new Vector(0, 0, 1)));

        stateNode.addChild(pointsNode);
        translateNode.addChild(stateNode);

        return translateNode;
    }

    createRoof() {
        var pointsNode = new SceneGraphPolygon([new Vector(0, -100), new Vector(100, 0), new Vector(-100, 0)]);
        var stateNode = new SceneGraphState("fillStyle", "#FF0000");
        var translateNode = new SceneGraphTransform(new Matrix.createTranslation(new Vector(0, 0, 1)));

        stateNode.addChild(pointsNode);
        translateNode.addChild(stateNode);

        return translateNode;
    }

    createDoor() {
        var pointsNode = new SceneGraphPolygon([new Vector(-20, 25), new Vector(20, 25), new Vector(20, 100), new Vector(-20, 100)]);
        var stateNode = new SceneGraphState("fillStyle", "#FF0000");
        var translateNode = new SceneGraphTransform(new Matrix.createTranslation(new Vector(0, 0, 1)));

        stateNode.addChild(pointsNode);
        translateNode.addChild(stateNode);

        return translateNode;
    }

    createLeftWindow() {
        var pointsNode = new SceneGraphPolygon([new Vector(-80, 30), new Vector(-40, 30), new Vector(-40, 70), new Vector(-80, 70)]);
        var stateNode = new SceneGraphState("fillStyle", "#0000FF");
        var translateNode = new SceneGraphTransform(new Matrix.createTranslation(new Vector(0, 0, 1)));

        stateNode.addChild(pointsNode);
        translateNode.addChild(stateNode);

        return translateNode;
    }

    
    createLeftBars() {
        var pointsNode = new SceneGraphPolygon([new Vector(-60, 30), new Vector(-60, 50), new Vector(-60, 70),  new Vector(-60, 50),  new Vector(-40, 50), new Vector(-60, 50),  new Vector(-80, 50),  new Vector(-60, 50)]);                              
        var translateNode = new SceneGraphTransform(new Matrix.createTranslation(new Vector(0,0,1)));

        translateNode.addChild(pointsNode);

        return translateNode;
    }
    

    createRightWindow() {
        var pointsNode = new SceneGraphPolygon([new Vector(40, 30), new Vector(80, 30), new Vector(80, 70), new Vector(40, 70)]);
        var stateNode = new SceneGraphState("fillStyle", "#0000FF");
        var translateNode = new SceneGraphTransform(new Matrix.createTranslation(new Vector(0, 0, 1)));

        stateNode.addChild(pointsNode);
        translateNode.addChild(stateNode);

        return translateNode;
    }

    
    createRightBars() {
        var pointsNode = new SceneGraphPolygon([new Vector(40, 50), new Vector(60, 50),new Vector(60, 30), new Vector(60, 50), new Vector(80, 50),new Vector(60, 50), new Vector(60, 70),new Vector(60, 50) ]);                
        var translateNode = new SceneGraphTransform(new Matrix.createTranslation(new Vector(0,0,1)));

        translateNode.addChild(pointsNode);

        return translateNode;
    }
    

    update(deltaTime) {
        var previousAngle = this.getAngle();
        var newAngle = previousAngle + ((Math.PI * 0.5) * deltaTime);
        var newAngleMatrix = new Matrix.createRotation(newAngle);
        this.setAngle(newAngle);
        this.getAngleNode().setLTM(newAngleMatrix);
        // new matrix = apply math.pi /36(5 degrees) to the angle 
        //angle node.setLTM(newmatrix)
        var direction = this.getDirection();
        var previousScale = this.getScale();
        var scaleX = previousScale.getX();
        var scaleY = previousScale.getY();
        if (scaleX < 0 && scaleY < 0) {
            direction*=-1;
            this.setDirection(direction);
        }
        else if (scaleX > 1 && scaleY > 1) {
            direction*=-1;
            this.setDirection(direction);
        }
        var scaleVector = new Vector(0.5, 0.5);
        var deltaScale = scaleVector.multiply(deltaTime);
        var scaleDirection = deltaScale.multiply(direction);
        var newScale = previousScale.add(scaleDirection);
        var newScaleMatrix = new Matrix.createScale(newScale);
        this.setScale(newScale);
        this.getScaleNode().setLTM(newScaleMatrix);
    }
}