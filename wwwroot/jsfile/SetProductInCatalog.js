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

// Метод который добавляет либо удаляет продукт из каталога;
function addProductToCatalog(productBacket) {
    // TEMP: Выводим в консоль продукт который хотим добавить в каталог для проверки;
    console.log("Получаем продукт для добавления в каталог", productBacket);
    let catalogProducts = getCatalogProducts();
    // TEMP: Выводим в консоль текущие продукты в каталоге для проверки;
    console.log("Добовляем продукт в каталог", productBacket);
    const exist=catalogProducts.some(x => x.id === productBacket.id);
    if(exist){
        catalogProducts = catalogProducts.filter(x => x.id !== productBacket.id);
        // TEMP: Выводим в консоль продукт который удалили из каталога для проверки;
        console.log("Продукт удален из каталога", productBacket);
    }
    else{
        catalogProducts.push(productBacket);
        console.log("Продукт добавлен в каталог", productBacket);
    }
    saveCatalogProducts(catalogProducts);
    console.log("Текущий каталог продуктов", catalogProducts);
}