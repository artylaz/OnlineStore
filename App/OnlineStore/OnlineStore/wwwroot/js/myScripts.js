
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
    
    var id = el.parentNode.parentNode.getAttribute('id');
    var inputs = document.getElementById(id).getElementsByTagName("input");
    
    var selects = document.getElementById(id).getElementsByTagName("select");

    var divs = document.getElementById(id).getElementsByTagName("div");

    for (var item of inputs) {
        
        if (item.type == "text" || item.type == "number" || item.type == "date") {
            item.removeAttribute("readonly");
        }
        else if (item.type == "submit"){
            item.style.display = "block"
        }
        else if (item.type == "checkbox") {
            item.removeAttribute("disabled");
        }

    }

    for (var item of selects) {
        item.removeAttribute("disabled");
    }
    for (var item of divs) {
        item.classList.remove("disabled");
    }


    el.style.display = "none";

}

if (document.getElementById("inputliShow").value == "Product") {
    document.getElementById("productsId").click();
}
else if (document.getElementById("inputliShow").value == "Category") {
    document.getElementById("сategoriesId").click();
}
else if (document.getElementById("inputliShow").value == "Characteristic") {
    document.getElementById("characteristicId").click();
}
else if (document.getElementById("inputliShow").value == "MonitorDatabase") {
    document.getElementById("monitorDatabaseId").click();
}
else if (document.getElementById("inputliShow").value == "Picture") {
    document.getElementById("pictureId").click();
}
else if (document.getElementById("inputliShow").value == "PurchaseHistory") {
    document.getElementById("purchaseHistoryId").click();
}
else if (document.getElementById("inputliShow").value == "User") {
    document.getElementById("userId").click();
}
else if (document.getElementById("inputliShow").value == "Role") {
    document.getElementById("roleId").click();
}



function checkboxOnch(el) {

    let checkboxes = el.parentNode.querySelectorAll('input:checked');
    for (i = 0; i < checkboxes.length; i++) {
        checkboxes[i].name = "ProductCharacteristics[" + i + "]"
    }
}




