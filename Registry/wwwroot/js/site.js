﻿let arrow = document.getElementsByClassName("arrow-parrent")[0];
arrow?.addEventListener("click", function () {
    let from = document.getElementById("from");
    let to = document.getElementById("to");
    let tmp;
    tmp = from.value;
    from.value = to.value;
    to.value = tmp;
}); 

let often_questions = document.getElementsByClassName("often_questions")[0];
often_questions?.addEventListener("click", function (event) {
    let elem = event.target;
    if (elem.classList.contains('click')) {
        if (elem.querySelector('output').classList.contains('visible')) {
            deleteVisibleClass(often_questions);
        }
        else {
            deleteVisibleClass(often_questions);
            elem.querySelector('span').innerHTML = '-';
            elem.querySelector('output').classList.toggle('visible');
            elem.style.height = "auto";
        }
        
    }
});
function deleteVisibleClass(parentTag) {
    let outputs = parentTag.querySelectorAll("output");
    for (let tag of outputs)
        if (tag.classList.contains('visible')) {
            tag.classList.remove('visible');
            tag.parentElement.style.height = "4rem";
            tag.parentElement.querySelector('span').innerHTML = '+';
        }
}

document.documentElement.addEventListener('click', function (event) {
    if (event.target.parentElement.classList.contains('details')) {
        let parentElem = event.target.parentElement;
        let table = parentElem.querySelector('table');
        let mainDiv = parentElem.parentElement;
        let divPrice = mainDiv.getElementsByClassName('price')[0];

        if (table.classList.contains('tableVisible')) {
            table.classList.remove('tableVisible');
            divPrice.style.top = "30%";
        }
        else {
            table.classList.add('tableVisible');
            divPrice.style.top = "15.35%";
        }
        
    }
});
