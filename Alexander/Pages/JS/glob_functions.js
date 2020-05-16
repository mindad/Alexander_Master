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

function checkExpiration_Global() { //check if past expiration date

    let user_name;
    try {
        user_name = localStorage.getItem('user');

    } catch (e) {
        window.open('../Login.html', '_self');
    }
    if (user_name != "Brewmiester" || user_name != "Manager") {
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

    $(window).on('beforeunload', function () { // Clear localStorage
        Clear_LocalStorage();
    });
    $("a").on('mousedown', function () { // Unclear localStorage in case of moving between pages
        $(window).unbind();
    });
    $(window).on('submit', function () { $(window).unbind();})

    try {
        user_name = localStorage.getItem('user');

    } catch (e) {
        window.open('../Login.html', '_self');
    }

    if (user_to_check != user_name) {
        window.open('../Login.html', '_self');
    }
}

function Check_User_Global(user_to_check) {

    $(window).on('beforeunload', function () { // Clear localStorage
        Clear_LocalStorage();
    });
    $("a").on('mousedown', function () { // Unclear localStorage in case of moving between pages
        $(window).unbind();
    });
    $(window).on('submit', function () { $(window).unbind(); })

    try {
        user_name = localStorage.getItem('user');

    } catch (e) {
        window.open('../Login.html', '_self');
    }

    if (user_to_check != "Brewmiester" || user_name != "Manager") {
        window.open('../Login.html', '_self');
    }
}

function refresh_table() {
    $(window).unbind();
    location.reload();
}

function Clear_LocalStorage() {
    localStorage.clear();
}


function f1() {
    return false;
}

function unbind_a_tag() {
    $("a").on('mousedown', function () { // Unclear localStorage in case of moving between pages
        $(window).unbind();
    });
}