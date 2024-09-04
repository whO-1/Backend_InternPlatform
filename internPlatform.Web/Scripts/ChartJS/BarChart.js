(async function () {
    var t = {
        891: function () {
            !function () {
                const t = {
                    async fetchData() {
                        const response = await fetch('/Admin/Statistics/StatisticLastHalfYearPosts'); 
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
                                    name: "Posted",
                                    data: [76, 85, 101, 98, 87, 105]//apiData.posted 
                                },
                                {
                                    name: "Deleted",
                                    data: [76, 85, 101, 98, 87, 105]//apiData.deleted 
                                }
                            ],
                            chart: {
                                type: "bar",
                                toolbar: {
                                    show: !1
                                }
                            },
                            colors: ["var(--bs-primary)", "var(--bs-primary-bg-subtle)"],
                            states: {
                                hover: {
                                    filter: {
                                        type: "darken",
                                        value: .9
                                    }
                                }
                            },
                            plotOptions: {
                                bar: {
                                    horizontal: !1,
                                    columnWidth: "45%",
                                    endingShape: "rounded"
                                }
                            },
                            dataLabels: {
                                enabled: !1
                            },
                            legend: {
                                position: "bottom"
                            },
                            xaxis: {
                                categories: ["Avg", "Jul", "Aug", "Sep", "Oct", "Nov"]
                            },
                            yaxis: {
                                title: {
                                    text: "{ P o s t s } "
                                }
                            },
                            tooltip: {
                                theme: "dark",
                                y: {
                                    formatter(t) {
                                        return  t + " posts"
                                    }
                                }
                            }
                        };
                        if (apiData.Result === 'OK') {
                            t.series[0].data = apiData.Records.Posted;
                            t.series[1].data = apiData.Records.Deleted;
                            t.xaxis.categories = apiData.Records.Labels;
                        }
                        new ApexCharts(document.querySelector("#bsb-chart-5"), t).render();
                    }
                };
                async function e() {
                    await t.init();
                }
                "loading" === document.readyState ? document.addEventListener("DOMContentLoaded", e) : e(),
                    window.addEventListener("load", (function () {
                    }
                    ), !1)
            }()
        }
    }
        , e = {};
    function n(r) {
        var o = e[r];
        if (void 0 !== o)
            return o.exports;
        var a = e[r] = {
            exports: {}
        };
        return t[r](a, a.exports, n),
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
            for (var r in e)
                n.o(e, r) && !n.o(t, r) && Object.defineProperty(t, r, {
                    enumerable: !0,
                    get: e[r]
                })
        }
        ,
        n.o = function (t, e) {
            return Object.prototype.hasOwnProperty.call(t, e)
        }
        ,
        function () {
            "use strict";
            n(891)
        }()
})();