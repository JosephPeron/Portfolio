class Physics {
    constructor(pPosition, pVelocity, pMass) {
        this.setMass(pMass);
        this.setVelocity(pVelocity);
        this.setAcceleration(new Vector(0, 0));
        this.setGravity(new Vector(0, 20));
        this.setPosition(pPosition);

        this.mForces = [];
    }

    getMass() {
        return this.mMass;
    }
    setMass(pMass) {
        this.mMass = pMass;
    }

    getGravity() {
        return this.mGravity;
    }
    setGravity(pGravity) {
        if (this.getGravity() === undefined) {
            this.mGravity = pGravity;
        }
    }

    getVelocity() {
        return this.mVelocity;
    }
    setVelocity(pVelocity) {
        this.mVelocity = pVelocity;
    }

    getAcceleration() {
        return this.mAcceleration;
    }
    setAcceleration(pAcceleration) {
        this.mAcceleration = pAcceleration;
    }

    getPosition() {
        return this.mPosition;
    }
    setPosition(pPosition) {
        this.mPosition = pPosition;
    }

    getForces() {
        return this.mForces;
    }

    clearForces() {
        this.mForces = [];
    }

    addForce(pForce) {
        this.mForces.push(pForce);
    }

    update(pDeltaTime) {
        //work out net force
        var netForce = this.calculateResultForce();

        // work out accel
        var accel = this.updateAcceleration(netForce);
        this.setAcceleration(accel);

        //add to accel to velocity and multiply by deltatime
        var velocity = this.updateVelocity(pDeltaTime);
        this.setVelocity(velocity);

        //add velo to position
        var position = this.updatePosition(pDeltaTime);
        this.setPosition(position);


    }

    calculateResultForce() {
        var netForce = new Vector(0, 0);
        var finalNetForce = new Vector(0, 0);
        for (var i = 0; i < this.mForces.length; i += 1) {
            finalNetForce = netForce.add(this.mForces[i]);
        }
        this.clearForces();

        return finalNetForce;
    }

    updateAcceleration(pNetForce) {
        //var accel = this.getForce() / this.getMass();
        var accel = pNetForce;
        accel = accel.add(this.getGravity());

        return accel;
    }

    updateVelocity(deltaTime) {
        var velocity = this.getVelocity();
        velocity = velocity.add(this.getAcceleration().multiply(deltaTime));

        return velocity;
    }

    updatePosition(deltaTime) {
        var initialPosition = this.getPosition();
        var velocity = this.getVelocity();
        var newPosition = initialPosition.add(velocity.multiply(deltaTime));

        return newPosition;
    }
}