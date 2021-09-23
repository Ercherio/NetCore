

$(document).ready(function () {
    $.ajax({
        url: '/Persons/GetPerson',
        /*url: '/Persons/GetPerson',*/
        type: "GET"
    }).done((result) => {
        console.log(result);
        var female = result.filter(data => data.gender === 0).length;
        var male = result.filter(data => data.gender === 1).length;
        console.log(male);
        console.log(result[0].gender);

        var itdel = result.filter(data => data.universityId === 1).length;
        var ui = result.filter(data => data.universityId === 2).length;
        var itb = result.filter(data => data.universityId === 3).length;
        var undip = result.filter(data => data.universityId === 4).length;
        console.log(itdel);



        var sal = []
        var fName = []
        for (r of result) {
            /*console.log(r.salary)*/
            sal.push(r.salary)
            fName.push(r.firstName)
        }

        console.log(sal);
        console.log(fName);
        /*ini untuk university*/

        var universities = {
            series: [{
                data: [itdel, ui, itb, undip]
            }],
            chart: {
                height: 350,
                type: 'bar',
            },
            plotOptions: {
                bar: {
                    borderRadius: 10,
                    dataLabels: {
                        position: 'center', // top, center, bottom
                    },
                }
            },
            dataLabels: {
                enabled: true,
                formatter: function (val) {
                    return val;
                },
                offsetY: -20,
                style: {
                    fontSize: '12px',
                    colors: ["#304758"]
                }
            },
            xaxis: {
                categories: ["ITDEL", "UI", "ITB", "UNDIP"],
                position: 'top',
                axisBorder: {
                    show: false
                },
                axisTicks: {
                    show: false
                },
                crosshairs: {
                    fill: {
                        type: 'gradient',
                        gradient: {
                            colorFrom: '#D8E3F0',
                            colorTo: '#BED1E6',
                            stops: [0, 100],
                            opacityFrom: 0.4,
                            opacityTo: 0.5,
                        }
                    }
                },
                tooltip: {
                    enabled: true,
                }
            },
            yaxis: {
                axisBorder: {
                    show: false
                },
                axisTicks: {
                    show: false,
                },
                labels: {
                    show: false,
                    formatter: function (val) {
                        return val;
                    }
                }
            }
        };
        var chartuniv = new ApexCharts(document.querySelector("#chartuniv"), universities);
        chartuniv.render();

        //ini untuk char salary

        var salaries = {
            series: [{
                name: 'Salary',
                data: sal
            }],
            chart: {
                height: 350,
                type: 'bar',
            },
            plotOptions: {
                bar: {
                    borderRadius: 10,
                    columnWidth: '50%',
                }
            }, dataLabels: {
                enabled: false
            },
            stroke: {
                width: 2
            },

            grid: {
                row: {
                    colors: ['#fff', '#f2f2f2']
                }
            }, xaxis: {
                labels: {
                    rotate: -45
                },
                categories: fName,
                tickPlacement: 'on'
            },
            yaxis: {
                title: {
                    text: 'Salary (Rp)',
                },
            },
            fill: {
                type: 'gradient', gradient: {
                    shade: 'light',
                    type: "horizontal",
                    shadeIntensity: 0.25,
                    gradientToColors: undefined,
                    inverseColors: true,
                    opacityFrom: 0.85,
                    opacityTo: 0.85,
                    stops: [50, 0, 100]
                },
            }
        };

        var chartsa = new ApexCharts(document.querySelector("#chartsalary"), salaries);
        chartsa.render();
        /*ini untuk char gender*/

        var options = {
            series: [male, female],
            chart: {
                width: 280,
                height: 350,
                type: 'pie',
            },
            labels: ['Male', 'Female'],
            responsive: [{
                breakpoint: 480,
                options: {
                    chart: {
                        width: 200
                    },
                    legend: {
                        show: true,
                        position: 'right',
                    }
                }
            }]
        };
        var chart = new ApexCharts(document.querySelector("#chart"), options);
        chart.render();
    }).fail((error) => {
        Swal.fire({
            title: 'Error!',
            text: 'Data Cannot Deleted',
            icon: 'Error',
            confirmButtonText: 'Next'
        })
    });
})