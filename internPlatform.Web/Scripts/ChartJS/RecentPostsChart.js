document.addEventListener('DOMContentLoaded', async function () {

    const state = {
        statisticPostsInfo: {
            url: '/Admin/Statistics/StatisticPostsInfo',
            arrData: [],
            headChartId: '',
        },
        statisticRecentPosts: {
            url: '/Admin/Statistics/StatisticRecentPosts',
            arrData: [],
            headChartId: 'headRecentPosts',
        },
        statisticTopPosts: {
            url: '/Admin/Statistics/StatisticTopPosts',
            arrData: [],
            headChartId: 'headTopPosts',
        },
    };

    const Select = (obj) => {
        obj.arrData.forEach(el => {
            el.ids.forEach(id => {
                el.arrNodes.push(document.getElementById(id));
            });
        });
    };


    function transformData(obj, index) {
        const newData = {
            data: {
                ...obj
            },
            ids: [],
            arrNodes:[]
        }
        for (const key in obj) {
            if (obj.hasOwnProperty(key)) {
                newData.ids.push(`statistic${obj.Title}_${key}_${index}`);
                //newData.ids.push(`statistic${obj.Title}_${key}`);
            }
        }

        return newData;
    }

    const GetData = async (url = '') => {
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
            let index = 0;
            fetchData.Records.forEach(el => {
                const transformedData = transformData(el, index);
                obj.arrData.push(transformedData);
                index++;
            });
            
        } else {
            console.error('Unexpected JSON format:', fetchData);
        }
    }

    const statusDisplay = (node, value) => {
        switch(value){
            case "Active":
                node.classList.add("bg-success");
                break; 
            case "In Process...":
                node.classList.add("bg-warning");
                break;   
            case "Blocked":
                node.classList.add("bg-danger");
                break;  
        }
        node.innerText = value; 
    }

    const defaultDisplay = (node, value) => node.innerText = value; 
    const displayPercentage = (value) => {
        return (value > 0) ? '+' + value + '%' : value + '%';
    }
    const displaySimple = (value) => value;

    const animateValue = (element = 0, start = 0, end = 0, duration = 1000, displayFun = (e) => e) => {
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
    const Display = (obj , funcFormat  ) => {
        if (obj.headChartId !== null && obj.headChartId !== undefined && obj.headChartId !== "" && obj.arrData.length === 0) {
            const head = document.getElementById(obj.headChartId);
            if (head !== null) {
                head.style.display = 'none';
                head.parentElement.innerHTML += '<p class="text-secondary"> No data yet ... </p>';
            }
        }
        else {
            obj.arrData.forEach(el => {
                const keys = Object.keys(el.data);
                el.arrNodes.forEach((node, index) => {
                    if (node !== undefined && node !== null) {
                        const indexKey = keys[index];
                        const indexValue = el.data[indexKey];
                        funcFormat(node, indexValue);
                    }
                });
                //formatFunction();
                //animateValue(el.secondary_node, startValue, el.data.Increase, 1000, displayPercentage);
                //animateValue(el.node, startValue, el.data.Total, 1000, displaySimple);
            });
        }
        
    }



    await Populate(state.statisticPostsInfo);
    await Populate(state.statisticRecentPosts);
    await Populate(state.statisticTopPosts);


    //Populate(state.visitorsCards);
    
    Select(state.statisticPostsInfo);
    Select(state.statisticRecentPosts);
    Select(state.statisticTopPosts);
    //console.log("After select",state.statisticPosts);

    Display(state.statisticPostsInfo, defaultDisplay);
    Display(state.statisticRecentPosts, defaultDisplay);
    Display(state.statisticTopPosts, statusDisplay);
});






