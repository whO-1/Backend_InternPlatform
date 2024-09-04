!function () {
    var t = {
        859: function () {
            !function () {
                const t = {
                    async fetchData() {
                        const response = await fetch('/Admin/Statistics/StatisticLastHalfYearPostsUser');
                        const data = await response.json();
                        return data;
                    },
                    async init() {
                        let apiData;
                        try {
                            apiData = await this.fetchData(); // Fetch data from API
                        }
                        catch (err) {
                            console.error("Error:", err.message);
                        }
                        const t = {
                            series: [
                                {
                                    name: "Likes",
                                    data: [44, 55, 57, 56, 61, 58]
                                },
                                {
                                    name: "Views",
                                    data: [76, 85, 101, 98, 87, 105]
                                }
                            ],
                            legend: {
                                position: "bottom"
                            },
                            theme: {
                                palette: "palette1"
                            },
                            chart: {
                                type: "bar"
                            },
                            plotOptions: {
                                bar: {
                                    horizontal: !1,
                                    columnWidth: "55%",
                                    endingShape: "rounded"
                                }
                            },
                            dataLabels: {
                                enabled: !1
                            },
                            stroke: {
                                show: !0,
                                width: 2,
                                colors: ["transparent"]
                            },
                            xaxis: {
                                categories: ["Jun", "Jul", "Aug", "Sep", "Oct", "Nov"]
                            },
                            yaxis: {
                                title: {
                                    text: "total"
                                }
                            },
                            fill: {
                                opacity: 1
                            },
                            tooltip: {
                                y: {
                                    formatter(t) {
                                        return  t
                                    }
                                }
                            }
                        };
                        if (apiData.Result === 'OK') {
                            t.series[0].data = apiData.Records.Likes;
                            t.series[1].data = apiData.Records.Views;
                            t.xaxis.categories = apiData.Records.Labels;
                        }
                        new ApexCharts(document.querySelector("#bsb-chart-3"), t).render()
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
        var a = e[o] = {
            exports: {}
        };
        return t[o](a, a.exports, n),
            a.exports
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
            n(859)
        }()
}();
