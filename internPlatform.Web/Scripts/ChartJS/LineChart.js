﻿(async function () {
    var e = {
        562: function () {
            !function () {
                const e = {
                    async fetchData() {
                        const response = await fetch('https://api.example.com/data'); // Replace with your API URL
                        const data = await response.json();
                        return data;
                    },
                    async init() {
                        //const apiData = await this.fetchData();
                        new ApexCharts(document.querySelector("#bsb-chart-6"), {
                            series: [{                  
                                name: "Visitors",
                                data: [
                                    {        //apiData.data
                                        x: "Jul",
                                        y: 2248
                                    },
                                    {
                                        x: "Aug",
                                        y: 3168
                                    },
                                    {
                                        x: "Sep",
                                        y: 3587
                                    },
                                    {
                                        x: "Oct",
                                        y: 3154
                                    },
                                    {
                                        x: "Nov",
                                        y: 3565
                                    },
                                    {
                                        x: "Dec",
                                        y: 4221
                                    },
                                    {
                                        x: "Jan",
                                        y: 6221
                                    }
                                ]
                            }],
                            chart: {
                                type: "area",
                                sparkline: {
                                    enabled: !0
                                }
                            },
                            xaxis: {
                                type: "category"
                            },
                            stroke: {
                                curve: "smooth"
                            },
                            fill: {
                                type: "gradient",
                                gradient: {
                                    shadeIntensity: 1,
                                    inverseColors: !1,
                                    opacityFrom: .12,
                                    opacityTo: 0,
                                    stops: [0, 90, 100]
                                }
                            },
                            colors: ["var(--bs-primary)"],
                            markers: {
                                size: 0
                            },
                            tooltip: {
                                theme: "dark",
                                fixed: {
                                    enabled: !0,
                                    position: "topLeft"
                                }
                            }
                        }).render()
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
            n(562)
        }()
})();
