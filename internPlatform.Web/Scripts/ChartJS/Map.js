﻿!function () {
    var e = {
        620: function () {
            !function () {
                const e = {
                    init() {
                        new jsVectorMap({
                            selector: "#bsb-map-2",
                            map: "world_merc",
                            selectedRegions: ["AU", "BR", "RU", "US"],
                            markers: [{
                                name: "Australia",
                                coords: [-25.274399, 133.775131],
                                style: {
                                    fill: "rgb(25, 135, 84)"
                                }
                            }, {
                                name: "Brazil",
                                coords: [-14.235004, -51.925282],
                                style: {
                                    fill: "rgb(255, 193, 7)"
                                }
                            }, {
                                name: "Russia",
                                coords: [61.52401, 105.318756],
                                style: {
                                    fill: "rgb(220, 53, 69)"
                                }
                            }, {
                                name: "United States",
                                coords: [37.09024, -95.712891],
                                style: {
                                    fill: "rgb(13, 110, 253)"
                                }
                            }]
                        })
                    }
                };
                function t() {
                    e.init()
                }
                "loading" === document.readyState ? document.addEventListener("DOMContentLoaded", t) : t(),
                    window.addEventListener("load", (function () { }
                    ), !1)
            }()
        }
    }
        , t = {};
    function n(r) {
        var o = t[r];
        if (void 0 !== o)
            return o.exports;
        var i = t[r] = {
            exports: {}
        };
        return e[r](i, i.exports, n),
            i.exports
    }
    n.n = function (e) {
        var t = e && e.__esModule ? function () {
            return e.default
        }
            : function () {
                return e
            }
            ;
        return n.d(t, {
            a: t
        }),
            t
    }
        ,
        n.d = function (e, t) {
            for (var r in t)
                n.o(t, r) && !n.o(e, r) && Object.defineProperty(e, r, {
                    enumerable: !0,
                    get: t[r]
                })
        }
        ,
        n.o = function (e, t) {
            return Object.prototype.hasOwnProperty.call(e, t)
        }
        ,
        function () {
            "use strict";
            n(620)
        }()
}();
