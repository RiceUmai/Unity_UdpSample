module.exports = class jsonsample{
    constructor () {
        this.jsonObj = {
            "type" : null,

            client : {
            "own" : {"address" : null, "port" : null},
            "ribal" : {"address" : null, "port" : null}
            },

            pos : {
                "own" : {"x" : null, "y" : null, "z" : null},
                "ribal" : {"x" : null, "y" : null, "z" : null}
            },

            rot : {
                "own" : {"x" : null, "y" : null, "z" : null},
                "ribal" : {"x" : null, "y" : null, "z" : null}
            },

            scale : {
                "own" : {"x" : null, "y" : null, "z" : null},
                "ribal" : {"x" : null, "y" : null, "z" : null}
            },
        }
    }
    get() {
        return this.jsonObj;  z
    } 
}