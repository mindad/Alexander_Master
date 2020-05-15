function checkExpiration_Brewmiester() { //check if past expiration date

    let user_name;
    try {
        user_name = localStorage.getItem('user');

    } catch (e) {
        window.open('../Login.html', '_self');
    }
    if (user_name == "Manager") {
        window.open('../Login.html', '_self');
    }

    let logged_in_time = JSON.parse(localStorage.getItem(`${user_name}_Timeout`));

    if (new Date(logged_in_time) < new Date()) {
        localStorage.removeItem(`${user_name}_Timeout`);
        localStorage.removeItem('user');
        window.open('../Login.html', '_self');
    }
}

function checkExpiration_Manager() { //check if past expiration date

    let user_name;
    try {
        user_name = localStorage.getItem('user');

    } catch (e) {
        window.open('../Login.html', '_self');
    }
    if (user_name == "Brewmiester") {
        window.open('../Login.html', '_self');
    }

    let logged_in_time = JSON.parse(localStorage.getItem(`${user_name}_Timeout`));

    if (new Date(logged_in_time) < new Date()) {
        localStorage.removeItem(`${user_name}_Timeout`);
        localStorage.removeItem('user');
        window.open('../Login.html', '_self');
    }
}

function Check_User(user_to_check) {
    try {
        user_name = localStorage.getItem('user');

    } catch (e) {
        window.open('../Login.html', '_self');
    }

    if (user_to_check != user_name) {
        window.open('../Login.html', '_self');
    }
}

function Clear_LocalStorage() {
    localStorage.clear();
}