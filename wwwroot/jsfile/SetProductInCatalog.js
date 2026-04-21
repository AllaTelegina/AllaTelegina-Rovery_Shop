// Метод который создаст либо возвращает localStorage для хранения продуктов в каталоге;
function getCatalogProducts() {
    let catalogProducts = localStorage.getItem('catalogProducts');
    // TEMP: Выводим в консоль текущие продукты в каталоге для проверки;
    console.log("Создан LocalStorage", catalogProducts);
    return JSON.parse(catalogProducts)|| [];
}
// Метод который сохраняет корзину в localStorage;
function saveCatalogProducts(catalogProducts) {
    localStorage.setItem('catalogProducts', JSON.stringify(catalogProducts));
}
// Метод который добавляет продукт в каталог;
function addProductToCatalog(productId) {
    let catalogProducts = getCatalogProducts();
    console.log("Добовляем продукт в каталог", productId);
    if(catalogProducts.includes(productId)){
        catalogProducts = catalogProducts.filter(id => id !== productId);
        console.log("Продукт удален из каталога", productId);
    }
    else{
        catalogProducts.push(productId);
        console.log("Продукт добавлен в каталог", productId);
    }
    saveCatalogProducts(catalogProducts);
    console.log("Текущий каталог продуктов", catalogProducts);
}
