const SEARCH_INTERVAL = 200;

let elem = document.querySelector('#input');
let containerRow = document.querySelector('#product-container .row');
let timeout;

elem.addEventListener('input',
    function () {
        if (timeout != null) {
            clearTimeout(timeout);
            timeout = null;
        }

        timeout = setTimeout(search, SEARCH_INTERVAL, elem.value);
    });

function search(query) {
    let url = "search?query=" + query;

    fetch(url).then(resp => {
        resp.json().then(data => {
            generateItems(data);
        });
    });
}

function generateItems(data) {
    containerRow.innerHTML = "";

    if (data == null || data.products == null || data.products.length == 0) {
        containerRow.innerHTML = '<div class="col-md-12" style="text-align: center"><h2>There\'s no products available. Try searching with another criteria</h2></div></div>';
        return;
    }

    for (let i = 0; i < data.products.length; i++) {
        let product = data.products[i];

        let div = document.createElement("div");
        let card = document.createElement("div");

        div.classList.add(["col-md-12", "col-lg-3", "mb-3", "mb-4"]);
        card.classList.add("card");

        card.innerText = product.name;

        div.appendChild(card);
        containerRow.appendChild(div);
    }
}