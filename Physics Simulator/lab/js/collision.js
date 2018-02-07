class Collision {
    constructor() { 
    }

    static circleCircleCheck(circle1, circle2) {

        var position1 = circle1.getPosition();
        var position2 = circle2.getPosition();

        var vectorDistanceBetween = position1.subtract(position2);
        var magnitudeBetween = vectorDistanceBetween.magnitude();

        var radius1 = circle1.getRadius();
        var radius2 = circle2.getRadius();

        var radiiSum = radius1 + radius2;

        if (magnitudeBetween <= radiiSum) {
            return true;
        }
        else {
            return false;
        }
    }

    static circleBoxCheck(circle1, box1) {
        var circle = circle1.getPosition();
        var box = box1.getPosition();
        var circlePositionX = circle.getX();
        var circlePositionY = circle.getY();
        var boxPositionX = box.getX();
        var boxPositionY = box.getY();

        var circleDistanceX = abs(circlePositionX - boxPositionX);
        var circleDistanceY = abs(circlePositionY - boxPositionY);
    }

    static lineLineCheck(box1, box2) {

    }

    static collidedResponse(object1, object2) {
        var position1 = object1.getPosition();
        var position2 = object2.getPosition();

        var vectorDistanceBetween = position1.subtract(position2);
        var normalisedVector = vectorDistanceBetween.normalise();
        if (normalisedVector.getX() == 0 && normalisedVector.getY() == 0) {
            normalisedVector = new Vector(0, 1);
        }
        var propulsion = normalisedVector.multiply(2000);

        object1.addForce(propulsion);
        object2.addForce(propulsion.multiply(-1));

    }


}
