class Line {
    constructor(pXPos, pYPos) {
        this.setXPos(pXPos);
        this.setYPos(pYPos);
    }

    getXPos() {
        return this.mXPos;
    }
    setXPos(pXPos) {
        this.mXPos = pXPos
    }

    getYPos() {
        return this.mYPos;
    }
    setYPos(pYPos) {
        this.mYPos = pYPos
    }

}