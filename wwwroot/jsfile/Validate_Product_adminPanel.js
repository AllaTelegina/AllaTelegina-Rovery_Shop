// js файл для проверки поля заполнения, а именно для проверки и для автоматического заполнения SKU;

document.addEventListener("DOMContentLoaded", Validate_AND_Create_SKU)

function Validate_AND_Create_SKU(){
    // нужно получить значение, которое будет выбрано в поле категория;
    // примеры заполнения полей и получения SKU.
    // если это будет:
    // велосипеды -             "1000000"
    // электровелосипеды -      "2000000"
    // самокаты -               "3000000"
    // электросамокаты -        "4000000"
    // аксессуары -             "5000000"

    //получил элемент, который делает выбор;
    const category= document.getElementById("categoryId");
    if (category){
        // отслеживаю за событием, которое будет происходить при выборе значения и сразу создаю функцию;
        category.addEventListener("change", function() {
            const category_element=this.value;
            console.log("Result");
            console.log(category_element);
            GetIdCategory(category_element);
            // Метод, который будет отправлять 
            
            async function GetIdCategory(category_element) {
                let respons= await fetch ("/SendSKU/" + category_element);
                console.log("запрос в BD");
                let responsDB= await respons.json();
                console.log(responsDB);
            }
        })
    }
    else
        console.log('Элемент не найден');
}
