let arrow = document.getElementsByClassName("arrow-parrent")[0];
arrow?.addEventListener("click", function () {
    let from = document.getElementById("from");
    let to = document.getElementById("to");
    let tmp;
    tmp = from.value;
    from.value = to.value;
    to.value = tmp;
}); 

/*let selectList = document.querySelector("[name=count_of_seats]");
selectList.addEventListener("click", function () {
    for (let i = 0; i < 3; i++) {
        console.log(selectList.c)
    }
});*/
