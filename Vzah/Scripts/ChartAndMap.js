
    function Hi(name) {
        debugger
        alert(name);
    }



//function LoadContent(ID, ChartType, TITLE, SUBTITLE, Class, Style, NAME, LABEL, VALUE, LABELVALUE, DRILLLABELVALUE, CATEGORIES, SUBVALALIGNMENT, YAXISLABEL, ISSUBVAL, PID, ICON, COLORARRAY, tooltip, plotOptions) {
//    console.log(CATEGORIES, LABELVALUE);

//    if (ChartType == 1) {
//        DC_BarChart(ID, ChartType, TITLE, SUBTITLE, Class, Style, NAME, LABEL, VALUE, LABELVALUE, DRILLLABELVALUE, CATEGORIES, SUBVALALIGNMENT, YAXISLABEL, ISSUBVAL, PID, ICON, COLORARRAY, tooltip, plotOptions);
//    }
//    else if (ChartType == 6) {
//        DC_ColumnChart(ID, ChartType, TITLE, SUBTITLE, Class, Style, NAME, LABEL, VALUE, LABELVALUE, DRILLLABELVALUE, CATEGORIES, SUBVALALIGNMENT, YAXISLABEL, ISSUBVAL, PID, ICON, COLORARRAY, tooltip, plotOptions);
//    }
//    else if (ChartType == 7) {
//        DC_LineChart(ID, ChartType, TITLE, SUBTITLE, Class, Style, NAME, LABEL, VALUE, LABELVALUE, DRILLLABELVALUE, CATEGORIES, SUBVALALIGNMENT, YAXISLABEL, ISSUBVAL, PID, ICON, COLORARRAY, tooltip, plotOptions);
//    }
//}

//function DC_BarChart(ID, ChartType, TITLE, SUBTITLE, Class, Style, NAME, LABEL, VALUE, LABELVALUE, DRILLLABELVALUE, CATEGORIES, SUBVALALIGNMENT, YAXISLABEL, ISSUBVAL, PID, ICON, COLORARRAY, tooltip, plotOptions) {

//    Highcharts.chart(NAME, {
//        chart: {
//            type: 'bar'
//        },
//        title: {
//            text: TITLE
//        },
//        exporting: { enabled: false },
//        subtitle: {
//            text: null
//        },
//        xAxis: {
//            categories: [LABEL],//['Africa', 'America', 'Asia', 'Europe', 'Oceania'], //[@Html.Raw(Model.LST[0].LABEL)],
//    title: {
//        text: null
//    }
//},


//yAxis: {
//    min: 0,
//        title: {
//        text: null,
//            align: 'high'
//    },
//    labels: {
//        enabled: false,//default is true
//            overflow: 'justify'
//    }
//},
////tooltip: {
////    valueSuffix: ' millions'
////},
//plotOptions: {
//    bar: {
//        dataLabels: {
//            enabled: true
//        },
//        colors: [COLORARRAY],
//    },
//    series: {
//        pointWidth: 20,
//            }
//},

//legend: {
//    enabled: false,
//        layout: 'vertical',
//            align: 'right',
//                verticalAlign: 'top',
//                    x: -40,
//                        y: 80,
//                            floating: true,
//                                borderWidth: 1,
//                                    //backgroundColor:'#fd2f34',
//                                    // Highcharts.defaultOptions.legend.backgroundColor || '#FFFFFF',
//                                    shadow: true
//},
//credits: {
//    enabled: false
//},
//series: [{
//    data: [VALUE],
//point: {
//    events: {
//        click: function (event) {

//            //alert(this.name);
//            //ViewAlertDetails(0, this.category);
//        }

//    }
//}
//         }],
//responsive: {
//    rules: [{
//        condition: {
//            maxWidth: 500
//        },
//        chartOptions: {
//            legend: {
//                align: 'center',
//                verticalAlign: 'bottom',
//                layout: 'horizontal'
//            },
//            yAxis: {
//                labels: {
//                    align: 'left',
//                    x: 0,
//                    y: -5
//                },
//                title: {
//                    text: null
//                }
//            },
//            subtitle: {
//                text: null
//            },
//            credits: {
//                enabled: false
//            }
//        }
//    }]
//}
//    });

//$("#"+ NAME).highcharts().setSize($("#"+ ID).parent().width() - 50, $("#" + ID).parent().height() - 100, false);
//}


//function DC_ColumnChart(ID, ChartType, TITLE, SUBTITLE, Class, Style, NAME, LABEL, VALUE, LABELVALUE, DRILLLABELVALUE, CATEGORIES, SUBVALALIGNMENT, YAXISLABEL, ISSUBVAL, PID, ICON, COLORARRAY, tooltip, plotOptions) {
//    alert(CATEGORIES, LABELVALUE);
//    Highcharts.chart(NAME, {
//        chart: {
//            type: 'column'
//        },
//        title: {
//            text: TITLE
//        },
//        subtitle: {
//            text: SUBTITLE
//        },
//        xAxis: {
//            //type: 'category'
//            categories: [JSON.parse(CATEGORIES)],
//    crosshair: true
//},
//yAxis: {
//    min: 0,
//        title: {
//        text: YAXISLABEL
//    }
//},
//tooltip: tooltip,
//plotOptions: plotOptions,
////series:[{name: "Sites", colorByPoint: true,data: [{name: "1388",y: 14690.00,drilldown: "1388"}, {name: "1389",y: 10212.00,drilldown: "1389"}]}, drilldown: { series: [{ {name: "1388", id: "1388",data: [["JMC1",5352.00],["JMC2",14690.00]]}, {name: "1389", id: "1389",data: [["JMC3",10212.00],["JMC4",1946.00]]}}]}]  ,
//        series: [JSON.parse(LABELVALUE)],
////drilldown: { DRILLLABELVALUE },
//                  });

//    $("#" + NAME).highcharts().setSize($("#" + ID).parent().width() - 90, $("#" + ID).parent().height() - 180, false);
//}


//function DC_LineChart(ID, ChartType, TITLE, SUBTITLE, Class, Style, NAME, LABEL, VALUE, LABELVALUE, DRILLLABELVALUE, CATEGORIES, SUBVALALIGNMENT, YAXISLABEL, ISSUBVAL, PID, ICON, COLORARRAY, tooltip, plotOptions) {


//    Highcharts.chart(NAME, {
//        chart: {
//            type: 'line'
//        },
//        title: {
//            text: TITLE
//        },

//        subtitle: {
//            text: SUBTITLE
//        },


//        xAxis: {
//            categories: [CATEGORIES],
//},
//yAxis: {
//    title: {
//        text: YAXISLABEL
//    }
//},
//plotOptions: plotOptions,
//series: [LABELVALUE],
//});


//    $("#" + NAME).highcharts().setSize($("#" + ID).parent().width() - 90, $("#" + ID).parent().height() - 180, false);
//}
