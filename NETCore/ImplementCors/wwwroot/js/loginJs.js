

//$(document).ready(function () {
//    (function () {
//        'use strict';
//        window.addEventListener('load', function () {
//            // Fetch all the forms we want to apply custom Bootstrap validation styles to
//            var forms = document.getElementsByClassName('user');

//            // Loop over them and prevent submission
//            var validation = Array.prototype.filter.call(forms, function (form) {
//                form.addEventListener('submit', function (event) {
//                    if (form.checkValidity() === false) {
//                        event.preventDefault();
//                        Swal.fire({
//                            icon: 'error',
//                            title: 'Oops...',
//                            text: 'Something went wrong!',
//                        })
//                        event.stopPropagation();
//                    }
//                    form.classList.add('was-validated');
//                }, false);
//            });
//            var validation = Array.prototype.filter.call(forms, function (form) {
//                form.addEventListener('submit', function (event) {
//                    if (form.checkValidity() === true) {
//                        event.preventDefault();
//                        var obj = {
                            
//                            "Email": $('#InputEmail').val(),
                           
//                            "Password": $('#InputPassword').val(),
                           

//                        };

//                        console.log(JSON.stringify(obj));
//                        var data = JSON.stringify(obj);
//                        console.log(data);
//                        // post data to database
//                        //insert(data);
//                    }
//                    form.classList.add('was-validated');
//                }, false);
//            });
//        }, false);
//    })();

//    method = "post" asp - controller="Logins" asp - action="Auth"

    //$("#btnLogin").click(e => {

    //    event.preventDefault();
    //    var obj = {

    //        "Email": $('#InputEmail').val(),

    //        "Password": $('#InputPassword').val(),


    //    };

    //    console.log(JSON.stringify(obj));
    //    var data = JSON.stringify(obj);
    //    console.log(data);
    //});

/*})*/