(async function () {
    var t = {
        551: function () {
            !function () {
                const t = {
                    async fetchData() {
                        const response = await fetch('/Admin/Statistics/StatisticPostsPerCategory'); 
                        const data = await response.json();
                        return data;
                    },
                    async init() {
                        let apiData;
                        try {
                            apiData = await this.fetchData();
                        }
                        catch (err) {
                            console.error("Error:", err.message);
                        }

                        const t = {
                            series: [21, 23, 19, 14, 8], //apiData.asda
                            labels: ["Sport", "Fashion", "Music", "Art", "Cultural"], ////apiData.asda
                            legend: {
                                position: "bottom"
                            },
                            theme: {
                                palette: "palette1"
                            },
                            chart: {
                                type: "donut"
                            },
                            dataLabels: {
                                enabled: !1
                            },
                            plotOptions: {
                                pie: {
                                    donut: {
                                        labels: {
                                            show: !0,
                                            name: {
                                                fontSize: "22px",
                                                fontWeight: 600
                                            },
                                            value: {
                                                fontSize: "16px",
                                                fontWeight: 400,
                                                formatter(t) {
                                                    return t + ' Posts'
                                                }
                                            }
                                        }
                                    }
                                }
                            },
                            tooltip: {
                                y: {
                                    formatter(t) {
                                        return  t + ' Posts'
                                    }
                                }
                            }
                        };
                        if (apiData.Result === 'OK') {
                            t.series = apiData.Records.Posted;
                            t.labels = apiData.Records.Labels;
                        }
                        new ApexCharts(document.querySelector("#bsb-chart-4"), t).render()
                    }
                };
                function e() {
                    t.init()
                }
                "loading" === document.readyState ? document.addEventListener("DOMContentLoaded", e) : e(),
                    window.addEventListener("load", (function () { }
                    ), !1)
            }()
        }
    }
        , e = {};
    function n(o) {
        var r = e[o];
        if (void 0 !== r)
            return r.exports;
        var i = e[o] = {
            exports: {}
        };
        return t[o](i, i.exports, n),
            i.exports
    }
    n.n = function (t) {
        var e = t && t.__esModule ? function () {
            return t.default
        }
            : function () {
                return t
            }
            ;
        return n.d(e, {
            a: e
        }),
            e
    }
        ,
        n.d = function (t, e) {
            for (var o in e)
                n.o(e, o) && !n.o(t, o) && Object.defineProperty(t, o, {
                    enumerable: !0,
                    get: e[o]
                })
        }
        ,
        n.o = function (t, e) {
            return Object.prototype.hasOwnProperty.call(t, e)
        }
        ,
        function () {
            "use strict";
            n(551)
        }()
})();
