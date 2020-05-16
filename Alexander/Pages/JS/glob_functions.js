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

function Check_User_Global() {

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
        window.open('Login.html', '_self');
    }

    if (user_name != "Brewmiester" && user_name != "Manager") {
        window.open('Login.html', '_self');
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

function Create_Navbar() {

    try {
        user_name = localStorage.getItem('user');
    } catch (e) {
        //window.open('Login.html', '_self');
    }
    
    if (user_name == 'Manager') {
        $('#which_navbar').html(MANAGER_NAVBAR);
    }
    else {
        $('#BREWMIESTER_NAVBAR').html(MANAGER_NAVBAR);
    }
    unbind_a_tag();
}

MANAGER_NAVBAR = `<nav class="navbar navbar-expand-sm navbar-light border-bottom" style="background-color: #f7f6f6;">
            <a class="navbar-brand" href="Manager\Manager-dashboard.html">Home</a>
            <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarNav"
                    aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
                <span class="navbar-toggler-icon"></span>
            </button>
            <div class="collapse navbar-collapse" id="navbarNav">
                <ul class="navbar-nav">
                    <li class="nav-item active">
                        <a class="nav-link" href="Manager/Manager-prod.html">Production</a>
                    </li>
                    <li class="nav-item active px-2">
                        <a class="nav-link" href="Manager/Manager-ProductInventory.html">Inventory</a>
                    </li>
                    <li class="nav-item active px-2">
                        <a class="nav-link" href="Manager/Orders.html">Orders</a>
                    </li>
                    <li class="nav-item active px-2">
                        <a class="nav-link" href="Manager/Alerts_Manager.html">Alerts</a>
                    </li>
                    <li class="nav-item active px-2">
                        <a class="nav-link" href="Manager/Annual_Reports.html">Annual Reports</a>
                    </li>
                    <li class="nav-item active px-2">
                        <a class="nav-link" href="Waste_transition.html">Waste</a>
                    </li>
                    <li class="nav-item active px-2">
                        <a class="nav-link" href="Login.html" onclick="Clear_LocalStorage()">Exit</a>
                    </li>
                </ul>
            </div>
            <a class="navbar-brand" href="#">
                <img src="Images/Logo_alexander.jpeg" height="68" alt="alexander logo">
            </a>
        </nav>`;

BREWMIESTER_NAVBAR = `<nav class="navbar navbar-expand-sm navbar-light border-bottom" style="background-color: #f7f6f6;">
            <a class="navbar-brand" href="Brewmiester-dashboard.html">Home</a>
            <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarNav"
                    aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
                <span class="navbar-toggler-icon"></span>
            </button>
            <div class="collapse navbar-collapse" id="navbarNav">
                <ul class="navbar-nav">
                    <li class="nav-item active">
                        <a class="nav-link" href="Brewmiester/Brewmiester-prod.html">Production</a>
                    </li>
                    <li class="nav-item active px-2">
                        <a class="nav-link" href="Brewmiester/Inventory.html">Inventory</a>
                    </li>
                    <li class="nav-item active px-2">
                        <a class="nav-link" href="Brewmiester/Recipes.html">Recipes</a>
                    </li>
                    <li class="nav-item active px-2">
                        <a class="nav-link" href="Waste_transition.html">Waste</a>
                    </li>
                    <li class="nav-item active px-2">
                        <a class="nav-link" href="Brewmiester/Alerts.html">Alerts</a>
                    </li>
                    <li class="nav-item active px-2">
                        <a class="nav-link" href="Login.html" onclick="Clear_LocalStorage()">Exit</a>
                    </li>
                </ul>
            </div>
            <a class="navbar-brand" href="#">
                <img src="Images/Logo_alexander.jpeg" height="68" alt="alexander logo">
            </a>
        </nav>`;