class SceneGraphTransform extends SceneGraphGroup {

    constructor(pLTM) {
        super();
        this.setLTM(pLTM);
    }

    getType() {
        return "transform";
    }

    getLTM() {
        return this.mLTM;
    }

    setLTM(pLTM) {
        this.mLTM = pLTM;
    }
}