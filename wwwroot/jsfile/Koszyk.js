// Js файл для страницы koszyk для модели Alpine.js;
function cart() {
    itemsBackets = JSON.parse(localStorage.getItem('catalogProducts')) || [];
    var cartItems = itemsBackets.map(item => ({
        id: item.id,            // Уникальный идентификатор товара;
        name: item.category,    // Категория товара;
        model: item.name,       // Имя товара;
        hsCode: item.sku,       // Код товара;
        quantity: item.quantity,            // Количество товара, которое покупает пользователь в корзине;
        weight: item.weight || 0,
        perPieceRate: item.price || 0,      // Цена за единицу товара;
        totalPrice: item.price || 0,
        color: "Black",
        deliveryMethod: "standard",
        description: "No description available.",
        isEditingDescription: false,
        originalDescription: "",
        showDescription: false,
        image: item.images[0] || []         // Отражается главное либо первое фото товара;
    }));
    return {
        cartItems,
        shippingMethod: "standard",
        promoCode: "",
        promoMessage: "",
        promoValid: false,
        discount: 0,

        removeItem(index, item) {
            if (confirm('Are you sure you want to remove this item?')) {
                this.cartItems.splice(index, 1);
                addProductToCatalog(item); // Возвращаем товар в каталог
            }
        },

        clearCart() {
            if (confirm('Are you sure you want to clear your cart?')) {
                this.cartItems = [];
            }
        },

        incrementQuantity(index) {
            this.cartItems[index].quantity++;
            this.updateTotalPrice(index);
            // TEMP: проверка работы метода ChengeQuentityElementsLocalstorage
            ChengeQuentityElementsLocalstorage(index, this.cartItems[index].quantity);
            console.log("Строка LocalStorage после добовления количества товара:", localStorage.getItem('catalogProducts'));
        },

        decrementQuantity(index) {
            if (this.cartItems[index].quantity > 1) {
                this.cartItems[index].quantity--;
                this.updateTotalPrice(index);
                ChengeQuentityElementsLocalstorage(index, this.cartItems[index].quantity);
                console.log("Строка LocalStorage после уменьшения количества товара:", localStorage.getItem('catalogProducts'));
            }
        },

        updateTotalPrice(index) {
            const item = this.cartItems[index];
            item.totalPrice = item.perPieceRate * item.quantity;
        },

        toggleDescription(index) {
            this.cartItems[index].showDescription = !this.cartItems[index].showDescription;
        },

        startEditingDescription(index) {
            this.cartItems[index].originalDescription = this.cartItems[index].description;
            this.cartItems[index].isEditingDescription = true;
        },

        saveDescription(index) {
            this.cartItems[index].isEditingDescription = false;
            // Here you could add code to save to backend if needed
            console.log(`Description updated for ${this.cartItems[index].name}`);
        },

        cancelEditingDescription(index) {
            this.cartItems[index].description = this.cartItems[index].originalDescription;
            this.cartItems[index].isEditingDescription = false;
        },

        getColorHex(color) {
            const colorMap = {
                'Black': '#000000',
                'Silver': '#C0C0C0',
                'Blue': '#0047AB',
                'Red': '#FF0000',
                'White': '#FFFFFF'
            };
            return colorMap[color] || '#000000';
        },  

        applyPromoCode() {
            // Example promo codes
            const promoCodes = {
                'SAVE10': {discount: 0.1, message: '10% discount applied!'},
                'FREESHIP': {discount: 0, message: 'Free shipping applied!', freeShipping: true},
                'WELCOME20': {discount: 0.2, message: '20% discount applied!'}
            };
            if (this.promoCode.trim() === '') {
                this.promoMessage = 'Please enter a promo code';
                this.promoValid = false;
                return;
            }

            const promo = promoCodes[this.promoCode.toUpperCase()];
            if (promo) {
                this.promoValid = true;
                this.promoMessage = promo.message;

                if (promo.discount) {
                    this.discount = this.subtotal * promo.discount;
                }

                if (promo.freeShipping) {
                    this.shippingMethod = 'standard';
                    this.shipping = 0;
                }
            }
            else {
                this.promoValid = false;
                this.promoMessage = 'Invalid promo code';
                this.discount = 0;
            }
        },

        calculateTax() {
            // Example tax calculation (7.5%)
            return (this.subtotal - this.discount) * 0.075;
        },

        get subtotal() {
            return this.cartItems.reduce((sum, item) => sum + item.totalPrice, 0);
        },

        get shippingCost() {
            const shippingRates = {
                'standard': 5,
                'express': 15,
                'overnight': 25
            };
            return shippingRates[this.shippingMethod] || 5;
        },

        get total() {
            return this.subtotal + this.shippingCost + this.calculateTax() - this.discount;
        }
    };
}

// TODO: метод для изминения количества товаров в строке LocalStorage;
// Замена будет производится по индексу элемента, в этот метод нужно передовать индекс; 
function ChengeQuentityElementsLocalstorage(indexElement, newQuantity) {
    // TEMP: проверка параметров входящих в метод элементов;
    console.log(`Индекс элемента для изменения количества: ${indexElement}, новое количество: ${newQuantity}`);
    let modeljs = JSON.parse(localStorage.getItem('catalogProducts')) || [];

    for (let i = 0; i < modeljs.length; i++) {
        if(i===indexElement){
            modeljs[i].quantity = newQuantity;
        }
        console.log("Массив modeljs после изменения количества элемента:", modeljs);
    };
    localStorage.setItem('catalogProducts', JSON.stringify(modeljs));
}
