////alert("asd");

////const p4 = document.getElementById('judul');
////p4.style.color = 'white';
////p4.style.backgroundColor = 'salmon' ;

////const p = document.getElementsByTagName('p');
////p[0].style.color = "cyan";

////const p2 = document.querySelector('.p2');
////p2.addEventListener('click', function () {
////    p2.style.backgroundColor = 'red';
////});

////p2.addEventListener('click', function () {
////    p2.color = 'cyan';
////});

////const animals = [
////    { name: "Fluffy", species: "cat", class: { name: "mamalia" } },
////    { name: "Nemo", species: "dog", class: { name: "vetebrata" } },
////    { name: "Ursa", species: "cat", class: { name: "mamalia" } },
////    { name: "Morphy", species: "bird", class: { name: "vetebrata" } },
////    { name: "Meow", species: "cat", class: { name: "mamalia" } },
////    { name: "Doggy", species: "dog", class: { name: "mamalia" } },
////    { name: "Momo", species: "dragon", class: { name: "amfibi" } },
////    { name: "Mino", species: "dog", class: { name: "vetebrata" } },
////    { name: "Carvil", species: "cat", class: { name: "mamalia" } },
////    { name: "Leon", species: "dog", class: { name: "vetebrata" } }
////]



////let cats=new Array();
////for (let i = 0; i < animals.length; i ++) {
////    if (animals[i].species == "dog") {
////        animals[i].class.name = "non-mamalia";
////    }
    
////    //if (animals[i].species == "cat") {
////    //   cats.push(animals[i]);
////    //}
////}


////cats = animals.filter(animal => animal.species == "cat");
////console.log(animals);
////console.log(cats);

$(document).ready(function () {
    $.ajax({
        url: "https://pokeapi.co/api/v2/pokemon/"
    }).done((result) => {
        console.log(result);
        var text = "";
        $.each(result.results, function (key, val) {
            /*text += `<li>${val.name}</li>`*/
            text += `<tr id="result${key + 1}">
            <td>${key + 1}</td>
            <td>${val.name}</td>
            <td><button type="button" class="btn btn-primary" data-toogle="modal" data-target="#StarWars" onclick ="detail('${val.url}')">Detail</button>
            </td>
            </tr>`;
            
            //$(document).ready(function () {

            //    $(`.click${key + 1}`).click(function (){
            //        let isDelete = confirm(`Delete ${val.name}?`);
            //        if (isDelete) {
            //            var el = document.getElementById(`result${key + 1}`);
            //            el.remove();
            //        }
            //    });
            //});


        });


        $('#swap1').html(text);

    }).fail((result) => {
        console.log(result);
    });

});

function detail(url) {
    $.ajax({
        url:url
    }).done((result) => {
        console.log(result);
        var text = "";
        var img = "";
        var type = "";
        var ability = "";
        var stat = "";
        /*img = `<img src="${result.sprites.other.official-artwork.front_default}">`;*/
        /*text += `<li>${val.name}</li>`*/
        console.log(result.sprites.other.dream_world.front_default);
        title = `<h1>${result.forms[0].name}</h1>`;
        img = `<img src="${result.sprites.other.dream_world.front_default}" class="rounded mx-auto d-block">`;
        for (let i = 0; i < result.types.length; i++) {
            if (result.types[i].type.name == 'grass') {
                type += `
                    <span class="badge badge-success">Grass</span>;`
  
            } if (result.types[i].type.name == 'poison') {
                type += `
                    <span class="badge badge-dark">Poison</span>`;
            }if (result.types[i].type.name == 'fire') {
                type += `
                    <span class="badge badge-danger">Fire</span>`;
            }if (result.types[i].type.name == 'flying') {
                type += `
                    <span class="badge badge-warning">Flying</span>`;
            }if (result.types[i].type.name == 'water') {
                type += `
                    <span class="badge badge-primary">Water</span>`;
            }if (result.types[i].type.name == 'bug') {
                type += `
                    <span class="badge badge-secondary">Bug</span>`;
            }if (result.types[i].type.name == 'normal') {
                type += `
                    <span class="badge badge-light">Normal</span>`;
            }
        }
        text = `
               <center><p>${type}</p></center>
               <ul>
                <li>ID      : ${result.id}</li>
                <li>Name    : ${result.forms[0].name}</li>
                <li>Weight  : ${result.weight}</li>
                <li>EXP     : ${result.base_experience}</li>
                `;
   
        for (let j = 0; j < result.abilities.length; j++) {
            if (result.abilities.length - 1 == 0 || j == 0) {
                if (result.abilities.length - 1 == 0) {
                    ability += `<li>Ability :
               <ol>
                <li>${result.abilities[0].ability.name} 
                </li>
               </ol >`;
                } else {
                    ability += `<li>Ability :
               <ol>
                <li>${result.abilities[0].ability.name}</li>`;
                }

            } else if (result.abilities.length-1!=0 && j == result.abilities.length - 1) {
                ability +=`
                <li>${result.abilities[j].ability.name}</li>
                </ol>`;
            } else if (result.abilities.length - 1 != 0 && j < result.abilities.length - 1){
                ability +=`<li>${result.abilities[j].ability.name}</li>`
            }
        }

        for (j = 0; j < result.stats.length; j++) {
            if (j == 0) {
                stat += `<li>Status : 
               <ul>
                <li>${result.stats[0].stat.name} : ${result.stats[0].base_stat}</li>`;
            } else if (j == result.stats.length - 1) {
                stat += `
                <li>${result.stats[j].stat.name} : ${result.stats[j].base_stat}</li>
                </ul>
                </ul>`;
            } else {
                stat += `<li>${result.stats[j].stat.name} : ${result.stats[j].base_stat}</li>`
            }
        }

        $("#StarWars").modal('show');

       
        $(".modal-body").html(title+img+text+ability+stat);

    }).fail((result) => {
        console.log(result);
    });
}

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