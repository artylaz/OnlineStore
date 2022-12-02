
let trProducts = document.getElementsByName("trProduct");
let price = 0.00;
trProducts.forEach((element) => {
    let priceStr = element.getElementsByClassName("product-price")[0].innerHTML;
    let num = priceStr.length;
    let input = element.getElementsByTagName("input")[0].value;
    element.getElementsByClassName("product_total")[0].innerHTML = (parseFloat(priceStr.slice(0, num - 2)) * parseFloat(input)).toFixed(2) + " ₽";
    price += parseFloat(priceStr.slice(0, num - 2)) * parseFloat(input)
})

document.getElementsByName("cartPrice").forEach((element) => {
    element.innerHTML = price.toFixed(2) + " ₽"
})

function Oninput() {
    let trProducts = document.getElementsByName("trProduct");
    let price = 0.00;
    trProducts.forEach((element) => {
        let priceStr = element.getElementsByClassName("product-price")[0].innerHTML;
        let num = priceStr.length;
        let input = element.getElementsByTagName("input")[0].value;
        element.getElementsByClassName("product_total")[0].innerHTML = (parseFloat(priceStr.slice(0, num - 2)) * parseFloat(input)).toFixed(2) + " ₽";
        price += parseFloat(priceStr.slice(0, num - 2)) * parseFloat(input)
    })

    document.getElementsByName("cartPrice").forEach((element) => {
        element.innerHTML = price.toFixed(2) + " ₽"
    })
};

function Edit(el) {
    

    var inputs = document.getElementById(el.parentNode.parentNode.getAttribute('id')).getElementsByTagName("input");
    
    var selects = document.getElementById(el.parentNode.parentNode.getAttribute('id')).getElementsByTagName("select");

    for (var item of inputs) {
        item.removeAttribute("readonly");
        if (item.id == "save") {
            item.style.display = "flex"
        }

        
    }

    for (var item of selects) {
        item.removeAttribute("disabled");
    }

    el.style.display = "none";

}




