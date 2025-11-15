console.log("файл подключен");
document.addEventListener("DOMContentLoaded", handlerforms);

function handlerforms() {
    const forms = document.getElementsByClassName("validate-form");
    if(!forms) return;

    // Нужно найти событие submit на каждом элементе формы, чтобы проверить поля перед отправкой
    document.addEventListener("submit", serch_element_forms);

    function serch_element_forms(event) {
        const email=document.getElementsByClassName("validate-email");
        const password=document.getElementsByClassName("validate-password");
        const fields_others=document.getElementsByClassName("validate-fields_others");

        // установка флага для проверки каждого поля
        let valid = true;
        let messages=[];
        
        // проверка заполнения поля email;
        if(!email.value.trim()){
            valid = false;
            messages.push("Email is required.");
        }
        
        // ошибка если поле ввело неправильный символ;
        const imailControl = /^[a-zA-Z0-9._+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/;
        if(!imailControl.test(email.value)){
            valid = false;
            messages.push("Email format is invalid. Example: user@example.com or user.name@example.com");
        }

        // проверка заполнения поля password;
        if(!password.value.trim()){
            valid = false;
            messages.push("Password is required.");
        }
        
        if(password.value.length < 8){
            valid = false;
            messages.push("Password must be at least 8 characters long.");
        }

        const passwordControl = /^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d@$!%*?&#^()[\]{}<>+=_.,:;|/~]{8,}$/;
        if(!passwordControl.test(password.value)){
            valid = false;
            messages.push("Password must contain at least one letter and one number.");
        }   

        // проверка заполнения других полей;
        if (!fields_others.value.trim()){
            valid = false;
            messages.push("This field is required.");
        }
        if(!valid){
            event.preventDefault(); // предотвращаем отправку формы
            alert(messages.join("\n")); // выводим сообщения об ошибках
        }
    }
};
