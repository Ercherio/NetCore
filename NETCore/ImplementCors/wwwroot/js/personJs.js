////$(document).ready(function () {
////    $.ajax({
////        url: ""
////    }).done((result) => {
////        console.log(result);
////        var text = "";
////        $.each(result.result, function (key, val) {
////            /*text += `<li>${val.name}</li>`*/
////            text += `<tr id="result${key + 1}">
////            <td>${key+1}</td>
////            <td>${val.firstName} ${val.lastName}</td>
////            <td><button type="button" class="btn btn-primary" data-toogle="modal" data-target="#GetPerson" onclick ="detail('https://localhost:5001/api/Persons/GetPerson/${val.nik}')">Detail</button>
////            </td>
////            </tr>`;
////        });


////        $('#person').html(text);

////    }).fail((result) => {
////        console.log(result);
////    });

////});


$(document).ready(function () {

    $.ajax({
        url: 'https://localhost:5001/api/Persons/GetPerson',
        type: "GET"
    }).done((result) => {
        console.log(result);
        var female = result.result.filter(data => data.gender === "Female").length;
        var male = result.result.filter(data => data.gender === "Male").length;
        console.log(male);
        var options = {
            series: [{
                data: [male, female]
            }],
            chart: {
                height: 350,
                type: 'bar',
            },
            plotOptions: {
                bar: {
                    borderRadius: 10,
                    dataLabels: {
                        position: 'top', // top, center, bottom
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
                categories: ["Male", "Female"],
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





    (function () {
        'use strict';
        window.addEventListener('load', function () {
            // Fetch all the forms we want to apply custom Bootstrap validation styles to
            var forms = document.getElementsByClassName('needs-validation');
            
            // Loop over them and prevent submission
            var validation = Array.prototype.filter.call(forms, function (form) {
                form.addEventListener('submit', function (event) {
                    if (form.checkValidity() === false) {
                        event.preventDefault();
                        Swal.fire({
                            icon: 'error',
                            title: 'Oops...',
                            text: 'Something went wrong!',
                        })
                        event.stopPropagation();
                    }
                    form.classList.add('was-validated');
                }, false);
            });
            var validation = Array.prototype.filter.call(forms, function (form) {
                form.addEventListener('submit', function (event) {
                    if (form.checkValidity() === true) {
                        event.preventDefault();
                        var obj = {
                            "NIK": $('#nik').val(),
                            "Firstname": $('#firstName').val(),
                            "Lastname": $('#lastName').val(),
                            "Phone": $('#phone').val(),
                            "BirthDate": $('#birthDate').val(),
                            "Salary": $('#salary').val(),
                            "Email": $('#email').val(),
                            "Gender": parseInt($('#gender').val()),
                            "Password": $('#password').val(),
                            "UniversityId": parseInt($('#university').val()),
                            "Degree": $('#degree').val(),
                            "GPA": $('#gPA').val(),
                            "RoleId": parseInt($('#roleID').val())

                        };

                        console.log(JSON.stringify(obj));
                        var data = JSON.stringify(obj);
                        // post data to database
                        insert(data);
                    }
                    form.classList.add('was-validated');
                }, false);
            });
        }, false);
    })();

    var table=$('#dataperson').DataTable({
       "filter": true,
        "dom": 'Bfrtip',
        "ajax": {
            "url": "https://localhost:5001/api/Persons/GetPerson",
            "datatype": "json",
            "dataSrc": "result"
        },
        
        "columns": [
            {
                "data": null,
                "orderable": false,
                "render": function (data, type, full, meta) {
                    return meta.row+1;
                }
            },
            {
                "data": "nik"
            },
            {
                "data": null,
                "render": function (data, type, row) {

                    return row["firstName"] + " "+ row["lastName"];
                },
                "autoWidth": true
            },
            
            {
                "data": "email"
            },
            {
                data: "phone", render: function (toFormat) {
                    var tPhone;
                    tPhone = toFormat.toString();
                    subsTphone = tPhone.substring(0, 2);
                    /*console.log(data.nik);*/
                    if (subsTphone == "08") {
                        tPhone = '(' + '+62' + ')' + tPhone.substring(1, 4) + '-' + tPhone.substring(5, 9) + '-' + tPhone.substring(10, 14);
                        return tPhone
                    } else {
                        tPhone = '(' + '+62' + ')' + tPhone.substring(0, 3) + '-' + tPhone.substring(4, 8) + '-' + tPhone.substring(9, 13);
                        return tPhone
                    }
                }
            },

           
            {
                "data": null,
                "render": function (data, type, row) {

                    return "Rp " + row["salary"];
                },
                "autoWidth": true
            },
            {
                "data": null,
                "orderable": false,
                "render": function (data, type, row) {
                    var button = `<button id= "btn-detail" class="btn btn-primary" data-toogle="modal" data-target="#GetPerson" onclick="detail('${row["nik"]}')">Details</button>`;
                    
                    button +='          '+`<button class="btn btn-danger" onclick="del('${row["nik"]}')">Delete</button>`;
                    return button
                }

                
            }
       ],

       "select": true,
       "colReorder": true,
       "buttons": [
           {
               extend: 'collection',
               text: 'Export',
               buttons: [
                   'copy',
                   {
                       extend: 'excelHtml5',
                       exportOptions: {
                           columns: [ 1, 2, 3,4, 5 ]
                       }
                   },
                   'csv',
                   {
                       extend: 'pdfHtml5',
                       exportOptions: {
                           columns: [ 1, 2, 3,4, 5 ]
                       }
                   },
                   'print'
               ]
           }
       ]
   });

    


    function insert(data) {
        console.log(data);
        $.ajax({
            url: 'https://localhost:5001/api/Persons/Register',
            method: 'POST',
            dataType: 'json',
            contentType: 'application/json',
            data: data
        }).done((result) => {
            //buat alert pemberitahuan jika 
            /*alert('SUCCESS')*/
            Swal.fire(
                'Registration Success!',
                'You clicked the button!',
                'success'
            )

              //idmodal di hide
            $('#Register').modal('hide');

            //reload only datatable
            setInterval(function () {
                table.ajax.reload(null, false); // user paging is not reset on reload
            }, 0);
        }).fail((error) => {
            //alert pemberitahuan jika gagal
           /* alert('ERROR')*/
            Swal.fire({
                icon: 'error',
                title: 'Oops...',
                text: 'Something went wrong!',
            })
          
        });
    }
    //$('#person').on('click', 'button', function () {
    //    var data = table.row($(this).parents('tr')).data();
    //    //alert(data.nik + "'s salary is: " + data.salary);

    //    console.log(data.nik);
        
      
    //    detail(`https://localhost:5001/api/Persons/GetPerson/${data.nik}`);
        
        
    //});


    //$("#GetPerson").on('show.bs.modal', function (data) {
    //    let triggerLink = $(data.relatedTarget);
    //    console.log(data);
    //    let id = triggerLink.data("nik")
        

    //    $("#modalTitle").text(id);
    //    $(this).find(".modal-body").html("<h5>id: " + id + "</h5> + <p>+exitMessage</p>");

    //});
   
});


function detail(nik) {
    $.ajax({
        url: "https://localhost:5001/api/Persons/GetPerson/"+nik
    }).done((result) => {
        console.log(result);
        var text = "";
        var sPhone;
        var salary = "Rp " + result.result.salary.toString();
        var birthDate = result.result.birthDate.toString().substring(0, 10);

        sPhone = result.result.phone.toString();
        var subsTphone = sPhone.substring(0, 2);
        /*console.log(data.nik);*/
        if (subsTphone == "08") {
            sPhone = '(' + '+62' + ')' + sPhone.substring(1, 4) + '-' + sPhone.substring(5, 9) + '-' + sPhone.substring(10, 14);
            
        } else {
            sPhone = '(' + '+62' + ')' + sPhone.substring(0, 3) + '-' + sPhone.substring(4, 8) + '-' + sPhone.substring(9, 13);
            
        }
        console.log(birthDate);
        title = `<h5>Detail of ${result.result.fullName}</h5>`;

        text = `                  
                <ul>
                            <li class="list-group">: ${result.result.nik}</li>
                            <li class="list-group">: ${result.result.fullName}, ${result.result.degree}</li>
                            <li class="list-group">: ${result.result.gender}</li>
                            <li class="list-group">: ${birthDate}</li>
                            <li class="list-group">: ${result.result.email}</li>
                            <li class="list-group">: ${sPhone}</li>
                            <li class="list-group">: ${result.result.gpa}</li>
                            <li class="list-group">: ${salary}</li>
                </ul>
             `;
        /*console.log(text);*/
        $("#GetPerson").modal('show');
        $("#exampleModalCenterTitle").html(title);
        $("#detailperson").html(text);
        

    }).fail((result) => {
        console.log(result);
    });
}

function del(nik) {
    console.log(nik)
    Swal.fire({
        title: `Are you sure to delete data nik = ${nik}?`,
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: "https://localhost:5001/api/Persons/" + nik,
                method: 'DELETE'
            }).done((result) => {
                console.log(result)

            Swal.fire(
                'Deleted!',
                'Your file has been deleted.',
                'success'
                )
                setInterval(function () {
                    table.ajax.reload(null, false); // user paging is not reset on reload
                }, 0);
            }).fail((result) => {
                console.log(result);
            });
        }
    })


   
}

//function detail(url) {
//    $.ajax({
//        url:url
//    }).done((result) => {
//        console.log(result);
//        var text = "";
//        var img = "";
//        var type = "";
//        var ability = "";
//        var stat = "";
//        /*img = `<img src="${result.sprites.other.official-artwork.front_default}">`;*/
//        /*text += `<li>${val.name}</li>`*/
//        console.log(result.sprites.other.dream_world.front_default);
//        title = `<h1>${result.forms[0].name}</h1>`;
//        img = `<img src="${result.sprites.other.dream_world.front_default}" class="rounded mx-auto d-block">`;
//        for (let i = 0; i < result.types.length; i++) {
//            if (result.types[i].type.name == 'grass') {
//                type += `
//                    <span class="badge badge-success">Grass</span>;`
  
//            } if (result.types[i].type.name == 'poison') {
//                type += `
//                    <span class="badge badge-dark">Poison</span>`;
//            }if (result.types[i].type.name == 'fire') {
//                type += `
//                    <span class="badge badge-danger">Fire</span>`;
//            }if (result.types[i].type.name == 'flying') {
//                type += `
//                    <span class="badge badge-warning">Flying</span>`;
//            }if (result.types[i].type.name == 'water') {
//                type += `
//                    <span class="badge badge-primary">Water</span>`;
//            }if (result.types[i].type.name == 'bug') {
//                type += `
//                    <span class="badge badge-secondary">Bug</span>`;
//            }if (result.types[i].type.name == 'normal') {
//                type += `
//                    <span class="badge badge-light">Normal</span>`;
//            }
//        }
//        text = `
//               <center><p>${type}</p></center>
//               <ul>
//                <li>ID      : ${result.id}</li>
//                <li>Name    : ${result.forms[0].name}</li>
//                <li>Weight  : ${result.weight}</li>
//                <li>EXP     : ${result.base_experience}</li>
//                `;
   
//        for (let j = 0; j < result.abilities.length; j++) {
//            if (result.abilities.length - 1 == 0 || j == 0) {
//                if (result.abilities.length - 1 == 0) {
//                    ability += `<li>Ability :
//               <ol>
//                <li>${result.abilities[0].ability.name} 
//                </li>
//               </ol >`;
//                } else {
//                    ability += `<li>Ability :
//               <ol>
//                <li>${result.abilities[0].ability.name}</li>`;
//                }

//            } else if (result.abilities.length-1!=0 && j == result.abilities.length - 1) {
//                ability +=`
//                <li>${result.abilities[j].ability.name}</li>
//                </ol>`;
//            } else if (result.abilities.length - 1 != 0 && j < result.abilities.length - 1){
//                ability +=`<li>${result.abilities[j].ability.name}</li>`
//            }
//        }

//        for (j = 0; j < result.stats.length; j++) {
//            if (j == 0) {
//                stat += `<li>Status : 
//               <ul>
//                <li>${result.stats[0].stat.name} : ${result.stats[0].base_stat}</li>`;
//            } else if (j == result.stats.length - 1) {
//                stat += `
//                <li>${result.stats[j].stat.name} : ${result.stats[j].base_stat}</li>
//                </ul>
//                </ul>`;
//            } else {
//                stat += `<li>${result.stats[j].stat.name} : ${result.stats[j].base_stat}</li>`
//            }
//        }

//        $("#StarWars").modal('show');

       
//        $(".modal-body").html(title+img+text+ability+stat);

//    }).fail((result) => {
//        console.log(result);
//    });
//}

//function ability(url) {
//    $.ajax({
//        url: url
//    }).done((result) => {
//        console.log(result);
//        var text = "";
//        for (let i = 0; i < result.effect_entries.length; i++) {
//            text += `<p>${result.effect_entries[i].effect}`;
//            $(`".modal-body#skill${i}"`).html(title + img + type + text + ability + stat);
//        }

//    }).fail((result) => {
//        console.log(result);
//    });
//}