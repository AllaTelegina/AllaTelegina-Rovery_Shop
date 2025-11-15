// Скрипт для валидации полей формы в реальном времени;
// Инструкция: добавить класс "validate-email" к полю email в форме, которую нужно валидировать;
//  Добавить класс "submit-buttonValidate" к кнопке submit формы, чтобы управлять её состоянием;
//  Добавить класс "validate-password" к полю password в форме, которую нужно валидировать;
//  Добавить класс "validate-form" к форме, которую нужно валидировать и нужно удалить атрибут type="submit" у кнопки submit формы, чтобы предотвратить её автоматическую отправку;




document.addEventListener("DOMContentLoaded", handlerforms);

var flag_email=false;
var flag_password=false;

function handlerforms() {
    const email=document.getElementsByClassName("validate-email")[0];
    const password=document.getElementsByClassName("validate-password")[0];
    const buttons_submit=document.getElementsByClassName("submit-buttonValidate")[0];
    const validate_form=document.getElementsByClassName("validate-form")[0];

    // Для нажатия на кнопку Google Sing in ;   
    const buttonGoogle=document.getElementsByClassName("google-button")[0];
    
    validateEmail(email); // вызываем метод проверки email;
    validatePassword(password); // вызываем метод проверки password;

    /*// Метод и событие для кнопки Google Sing in ;
    buttonGoogle.addEventListener("submit", function(){
        console.log("нажали на кнопку Google Sing in");
    });*/
    
    // Метод для проверки, если событие нажать на кнопку "submit" формы;
    validate_form.addEventListener("submit", function(event) {
        console.log("вошли в проверку кнопки submit, сработало событие submit");
        if(flag_email && flag_password){
            validate_form.submit();
        }
        else{
            event.preventDefault(); // предотвращаем стандартное поведение формы при отправке;
            if(!flag_email){
            email.classList.add("border-red-500", "border-[5px]"); // добавляем класс с красной рамкой при ошибке;
            }
            if(!flag_password){
            password.classList.add("border-red-500", "border-[5px]"); // добавляем класс с красной рамкой при ошибке;
            }
        }
    });

    // Метод для проверки email;
    function validateEmail(email) {
    if(email){ 
            email.addEventListener("blur", function(event) {
                
                //<--control-->
                console.log("вошли в проверку email, сработало событие blur");
                const imailControl_Regex = /^[a-zA-Z0-9._+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/; // регулярное выражение для проверки формата email;
                const value_from_field= event.target.value.trim(); // получаем текущее значение поля email;
                const isvalid=imailControl_Regex.test(value_from_field); // проверяем значение на соответствие формату email;
                if(value_from_field===null || !isvalid){
                    event.target.classList.add("border-red-500", "border-[5px]"); // добавляем класс с красной рамкой при ошибке;
                    flag_email=false;

                    //<--control-->
                    console.log("проверка email не пройдена", flag_email);
                } 
                else {
                    event.target.classList.remove("border-red-500", "border-[5px]"); // удаляем класс с красной рамкой если ошибка исправлена;
                    flag_email=true;
                }
            });
        }
    }

    // Метод для проверки password;
    function validatePassword(password) {
        if(password){
            password.addEventListener("blur", function(event) {

                //<--control-->
                console.log("вошли в проверку password, сработало событие blur");
                const hashpassword=/^[A-Za-z\d@$!%*?&]{8,}$/; // регулярное выражение для проверки формата password;
                /* Которое допускает ввод только :
                1. Латинские буквы
                2. Допустимы цыфры
                3. Допустимы специальные символы @$!%*?& 
                4. Не менее 8 символов ;*/

                const value_password= event.target.value.trim(); // получаем текущее значение поля password;
                const isvalid_password=hashpassword.test(value_password); // проверяем значение на соответствие формату password;
                if(value_password.length<8){
                    event.target.classList.add("border-red-500", "border-[5px]"); // добавляем класс с красной рамкой при ошибке;
                    flag_password=false;
                } 
                if(value_password===null ||!isvalid_password){
                    event.target.classList.add("border-red-500", "border-[5px]"); // добавляем класс с красной рамкой при ошибке;
                    flag_password=false;
                    
                    //<--control-->
                    console.log("проверка password не пройдена", flag_password);
                }
                else {
                    event.target.classList.remove("border-red-500", "border-[5px]"); // удаляем класс с красной рамкой если ошибка исправлена;
                    flag_password=true;
                }
            });
        }
    }
}
