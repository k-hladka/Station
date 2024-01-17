let arrow = document.getElementsByClassName("arrow-parrent")[0];
arrow.addEventListener("click", function () {
    let from = document.getElementById("from");
    let to = document.getElementById("to");
    let tmp;
    tmp = from.value;
    from.value = to.value;
    to.value = tmp;
}); 
/* <div>
        <form>
            <label for="count_of_seats">Оберіть кількість місць</label>
            <select name="count_of_seats">
                <option>1</option>
                <option>2</option>
                <option>3</option>
            </select>
            <button type="submit">Далі</button>
        </form>
    </div>*/
/*document.body.addEventListener("click", function (e) {
    if (e.target.classList.contains("buy_bus") && !e.target.classList.contains("has")) {
        let countTickets = document.getElementsByClassName("countTickets");
        for (let i in countTickets)
            console.log(i)
        let elem = e.target;
        while (!elem.classList.contains("parent_info")) 
            elem = elem.parentElement;
    
        e.target.classList.add("has");

        elem.append(getCountTickets());
    }
});*/
/*function getCountTickets() {
    let div = document.createElement('div');
    div.classList.add("countTickets");
    let form = document.createElement('form');
    let label = document.createElement('label');
    label.innerHTML = "Оберіть кількість місць";
    label.setAttribute("for", "count_of_seats");

    let select = document.createElement('select');
    select.setAttribute("name", "count_of_seats");

    for (let i = 0; i < 3; i++) {
        let option = document.createElement('option');
        option.setAttribute("name", i + 1);
        option.innerHTML = i + 1;
        select.append(option);
    }
    let button = document.createElement("button");
    button.setAttribute("type", "submit");
    button.innerHTML = "Далі";

    form.append(label);
    form.append(select);
    form.append(button);

    div.append(form);

    return div;
}*/
