// the window load event handler

function onLoad() {

    var mainCanvas, mainContext, houses, circles, sqaures, items, collided;
    var centreNode, angleNode, scaleNode;
    var previousTime;

    // this function will initialise our variables
    function initialiseCanvasContent() {
        // Find the canvas element using it's ID attribute.
        mainCanvas = document.getElementById('mainCanvas');
        // if it couldn't be found 
        if (!mainCanvas) {
            // make a message box pop up with error
            alert('Error: I cannot find the canvas element!');
            return;
        }
        // Get the 2D canvas content.
        mainContext = mainCanvas.getContext('2d');
        if (!mainContext) {
            alert('Error: failed to get Context!');
            return;
        }

        //translate rotate scale

        scaleNode = new SceneGraphTransform(new Matrix.createScale(new Vector(1, 1, 1)));
        angleNode = new SceneGraphTransform(new Matrix.createRotation(0));
        centreNode = new SceneGraphTransform(new Matrix.createTranslation(new Vector(mainCanvas.width / 2, mainCanvas.height / 2, 1)));

        //create houses

        houses = [];
        items = [];

        var housePosition = new Vector(0, 0);
        var house = new House(housePosition, new Vector(1, 1, 1), 0);
        var houseNode = house.getTopNode();
        houses.push(house);
        scaleNode.addChild(houseNode);

        
        for (var i = 0; i < 100; i += 1) {
            var randomXPos = Math.floor(Math.random() * (399 - -399 + 1)) + -399;
            var randomYPos = Math.floor(Math.random() * (299 - -299 + 1)) + -299;
            var ballPosition = new Vector(randomXPos, randomYPos);
            var ballObject = new Circle(ballPosition, 10, new Vector(1, 1, 1));
            items.push(ballObject);
            var ballNode = ballObject.getTopNode();
            scaleNode.addChild(ballNode);
        }
                
        for (var i = 0; i < 100; i += 1) {
            var randomXPos = Math.floor(Math.random() * (399 - -399 + 1)) + -399;
            var randomYPos = Math.floor(Math.random() * (299 - -299 + 1)) + -299;
            var squarePosition = new Vector(randomXPos, randomYPos);
            var squareObject = new Square(squarePosition, new Vector(1, 1, 1), 20, 20);
            items.push(squareObject);
            var squareNode = squareObject.getTopNode();
            scaleNode.addChild(squareNode);
        }

        angleNode.addChild(scaleNode);
        centreNode.addChild(angleNode);

        scene = centreNode;

        renderVisitor = new RenderVisitor(mainContext);
    }

    function animationLoop() {

        var currentTime = Date.now();
        var deltaTime = (currentTime - previousTime) / 1000;

        update(deltaTime);

        draw();

        previousTime = currentTime;

        requestAnimationFrame(animationLoop);

    }

    function update(deltaTime) {
        for (var i = 0; i < houses.length; i += 1) {
            houses[i].update(deltaTime);
        }
        
        for (var i = 0; i < items.length; i += 1) {
            items[i].update(deltaTime);
        }

        for (var i = 0; i < items.length - 1; i += 1) {
            for (var j = i + 1; j < items.length; j += 1) {
                object1 = items[i];
                object2 = items[j];
                var type1 = object1.getType();
                var type2 = object2.getType();

                if (type1 == "circle" && type2 == "circle") {
                    collided = Collision.circleCircleCheck(object1, object2);
                }
                /*
                else if (type1 == "circle" && type2 == "box") {
                    collided = Collision.circleLineCheck(object1, object2);
                }
                else if (type1 == "box" && type2 == "circle") {
                    collided = Collision.getEdges(object2, object1);
                }
                else if (type1 == "box" && type2 == "box") {
                    collided = Collision.getEdges(object1, object2);
                }
                */
                if (collided) {
                    Collision.collidedResponse(object1, object2)
                }
                collided = false;
            }
        }
    }

    // this function will actually draw on the canvas
    function draw() {
        //changes the width of the lines from default to 5
        mainContext.lineWidth = 5;
        // set the draw fill style colour to grey
        mainContext.fillStyle = "#BBBBBB";
        centreNode.getLTM().setTransform(mainContext);
        // fill the canvas with black
        mainContext.fillRect(-400, -300, mainCanvas.width, mainCanvas.height);
        
        renderVisitor.visit(scene);
    }

    initialiseCanvasContent();
    previousTime = Date.now();
    animationLoop();

}
window.addEventListener('load', onLoad, false);