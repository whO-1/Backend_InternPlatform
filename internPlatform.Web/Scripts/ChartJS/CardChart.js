document.addEventListener('DOMContentLoaded', async function () {

    const state = {
        statisticCards : {
            url: '/Admin/Statistics/StatisticUsersEvents',
            data : [
                {
                    data: {
                        Total: 239,
                        Increase: 20,
                    },
                    id: "statisticViews",
                    secondary_id: "statisticViews_lastWeek",
                    node: {},
                    secondary_node: {}
                },
                {
                    data: {
                        Total: 78,
                        Increase: 10,
                    },
                    id: "statisticEvents",
                    secondary_id: "statisticEvents_lastWeek",
                    node: {

                    },
                    secondary_node: {}
                },
                {
                    data: {
                        Total: 140,
                        Increase: -5,
                    },
                    id: "statisticUsers",
                    secondary_id: "statisticUsers_lastWeek",
                    node: {

                    },
                    secondary_node: {}
                },
                {
                    data: {
                        Total: 65,
                        Increase: 0,
                    },
                    secondary_id: "statisticSubscribers_lastWeek",
                    id: "statisticSubscribers",
                    node: {

                    },
                    secondary_node: {}
                },
            ],
        },
        userInfoCards : {
            url: '/Admin/Statistics/StatisticUsersInfo',
            data :[
                {
                    data: {
                        Total: 55,
                    },
                    id: "statisticAuthUsers",
                    //secondary_id: "statisticViews_lastWeek",
                    url: "",
                    node: {},
                    secondary_node: {}

                },
                {
                    data: {
                        Total: 139,
                    },
                    id: "statisticActiveUsers",
                    //secondary_id: "statisticViews_lastWeek",
                    url: "",
                    node: {},
                    secondary_node: {}

                },
                {
                    data: {
                        Total: 533,
                    },
                    id: "statisticPassiveUsers",
                    //secondary_id: "statisticViews_lastWeek",
                    url: "",
                    node: {},
                    secondary_node: {}

                },
                {
                    data: {
                        Total: 14,
                    },
                    id: "statisticInactive",
                    //secondary_id: "statisticViews_lastWeek",
                    url: "",
                    node: {},
                    secondary_node: {}

                },
                {
                    data: {
                        Total: 0,
                    },
                    id: "statisticBlockedUsers",
                    //secondary_id: "statisticViews_lastWeek",
                    url: "",
                    node: {},
                    secondary_node: {}

                },
                {
                    data: {
                        Total: 645,
                    },
                    id: "statisticTotalUsers",
                    //secondary_id: "statisticViews_lastWeek",
                    url: "",
                    node: {},
                    secondary_node: {}

                },
            ],
        },
        visitorsCards : {
            url: '',
            data : [
                {
                    data: {
                        Total: 145,
                    },
                    id: "visitors_chrome",
                    //secondary_id: "statisticViews_lastWeek",
                    url: "",
                    node: {},
                    secondary_node: {}
                },
                {
                    data: {
                        Total: 23,
                    },
                    id: "visitors_Safari",
                    //secondary_id: "statisticViews_lastWeek",
                    url: "",
                    node: {},
                    secondary_node: {}
                },
                {
                    data: {
                        Total: 2,
                    },
                    id: "visitors_Firefox",
                    //secondary_id: "statisticViews_lastWeek",
                    url: "",
                    node: {},
                    secondary_node: {}
                },
                {
                    data: {
                        Total: 11,
                    },
                    id: "visitors_Edge",
                    //secondary_id: "statisticViews_lastWeek",
                    url: "",
                    node: {},
                    secondary_node: {}
                },

            ],
        },

    }

    



    const Select = (arr) => {
        arr.forEach(el => {
            el.node = document.getElementById(el.id);
            el.secondary_node = document.getElementById(el.secondary_id);
        });
    }





    function transformData(records) {
        return records.map(record => ({
            data: {
                Total: record.Total,
                Increase: record.Increase
            },
            id: `statistic${record.Title}`,
            secondary_id: `statistic${record.Title}_lastWeek`,
            node: {},
            secondary_node: {}
        }));
    }
    const GetData = async ( url = '' ) => {
        const response = await fetch(url, {
            method: 'GET', 
            mode: 'cors', 
            credentials: 'same-origin', 
            headers: {
                'Content-Type': 'application/json'
            },
            redirect: 'follow', 
            referrerPolicy: 'no-referrer', 
        });
        return response.json(); 
    }
    const Populate = async (obj) => {

        const fetchData = await GetData(obj.url);

        if (fetchData.Result === "OK" && Array.isArray(fetchData.Records)) {
            const transformedData = transformData(fetchData.Records);
            obj.data = transformedData;
            console.log(transformedData)
        } else {
            console.error('Unexpected JSON format:', fetchData);
        }

    }



    const displayPercentage = (value) => {
        return (value > 0) ? '+' + value + '%' :  value + '%' ;
    }
    const displaySimple = (value) => value;
    const animateValue = (element=0, start=0, end=0, duration=1000, displayFun = (e) => e) => {
        const range = end - start; 
        let current = start;
        const increment = end >= start ? 1 : -1;
        const stepTime = Math.abs(Math.floor(duration / (range + 1)));

        if (start !== end) {
            const timer = setInterval(() => {
                current += increment;
                element.innerText = displayFun(current);
                if (!(current < end - 1 || current > end + 1)) {
                    clearInterval(timer);
                    element.innerText = displayFun(end);
                }
            }, stepTime);
        }
        else {
            element.innerText = displayFun(end);
        }
    }
    const Display = (arr) => {
        arr.forEach(el => {
            if (el.node !== undefined && el.node !== null) {
                el.node.innerText = el.data.Total;
                
            }
            if (el.secondary_node !== undefined && el.secondary_node !== null) {

              
                let value = '';
                const startValue = 0; 
                const endValue = el.data.Increase; 

                if (el.data.Increase > 0) {
                    value = '+' + el.data.Increase + '%';
                    el.secondary_node.classList.add('text-success');
                    el.secondary_node.parentElement.previousElementSibling.classList.add("text-success");
                    el.secondary_node.parentElement.previousElementSibling.classList.add("bg-success-subtle");
                }
                else if (el.data.Increase < 0) {
                    value = el.data.Increase + '%';
                    el.secondary_node.classList.add('text-danger');
                    el.secondary_node.parentElement.previousElementSibling.classList.add("text-danger");
                    el.secondary_node.parentElement.previousElementSibling.classList.add("bg-danger-subtle");
                }
                else {
                    value = el.data.Increase + '%';
                    el.secondary_node.parentElement.previousElementSibling.classList.add("bg-secondary-subtle");
                }
                animateValue(el.secondary_node, startValue, el.data.Increase, 1000, displayPercentage);
                animateValue(el.node, startValue, el.data.Total, 1000, displaySimple);
            }


        });

    }



    
    await Populate(state.statisticCards);
    await Populate(state.userInfoCards);
    //Populate(state.visitorsCards);

    Select(state.statisticCards.data);
    Select(state.userInfoCards.data);
    Select(state.visitorsCards.data);
    
    Display(state.statisticCards.data);
    Display(state.userInfoCards.data);
    Display(state.visitorsCards.data);

});


