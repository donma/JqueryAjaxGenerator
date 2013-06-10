//--------------------------------------------------------------------------------
// Generate By ASMXBridge.
// No2don is no warranty for this document works correctly.
// If you have any questions.You can  feel free mail to no2don@gmail.com
//--------------------------------------------------------------------------------


//---------------------------------------------------------------------------//
//-----------------------------Object Models--------------------------------//
//--------------------------------------------------------------------------//

// -- Users
var Users = new Array();

// -- User
function User() {
    /// <summary>User</summary>
    User.prototype.Id = '';
    User.prototype.Name = '';
    User.prototype.Age = '';
    User.prototype.Birth = '';
}



//-----------------------------Methods---------------------------------//

// -- UpdateUsers
function UpdateUsers(users, successFunc, errorFunc) {
    /// <summary></summary>
    /// <param name="users" type="ArrayOfUser">users</param>
    /// <param name="successFunc" type="function">Success Function</param>
    /// <param name="errorFunc" type="function">Error Function</param>
    /// <returns type="void">No return data.</returns>
    var $res = null;
    $.ajax({
        type: "POST",
        url: "http://localhost:19756/UserService.asmx/UpdateUsers",
        contentType: "application/json; charset=utf-8",
        async: false,
        cache: false,
        dataType: 'json',
        data: "{users:" + JSON.stringify(users) + "}",
        success: function (data) {
            if (data.hasOwnProperty("d")) {
                $res = data.d;
                if (successFunc != null)
                    successFunc(data.d);
            }
            else {
                $res = data;
                if (successFunc != null)
                    successFunc(data);
            }
        },
        error: function () {
            if (errorFunc != null)
                errorFunc();
        }


    });
    return $res;
}

// -- AddUser
function AddUser(user, successFunc, errorFunc) {
    /// <summary></summary>
    /// <param name="user" type="User">user</param>
    /// <param name="successFunc" type="function">Success Function</param>
    /// <param name="errorFunc" type="function">Error Function</param>
    /// <returns type="string">AddUserResult as string</returns>
    var $res = '';
    $.ajax({
        type: "POST",
        url: "http://localhost:19756/UserService.asmx/AddUser",
        contentType: "application/json; charset=utf-8",
        async: false,
        cache: false,
        dataType: 'json',
        data: "{user:" + JSON.stringify(user) + "}",
        success: function (data) {
            if (data.hasOwnProperty("d")) {
                $res = data.d;
                if (successFunc != null)
                    successFunc(data.d);
            }
            else {
                $res = data;
                if (successFunc != null)
                    successFunc(data);
            }
        },
        error: function () {
            if (errorFunc != null)
                errorFunc();
        }


    });
    return $res;
}

// -- GetAllUsers
function GetAllUsers(successFunc, errorFunc) {
    /// <summary></summary>
    /// <param name="successFunc" type="function">Success Function</param>
    /// <param name="errorFunc" type="function">Error Function</param>
    /// <returns type="Users">GetAllUsersResult as Users</returns>
    var $res = new Array();
    $.ajax({
        type: "POST",
        url: "http://localhost:19756/UserService.asmx/GetAllUsers",
        contentType: "application/json; charset=utf-8",
        async: false,
        cache: false,
        dataType: 'json',
        data: '',
        success: function (data) {
            if (data.hasOwnProperty("d")) {
                $res = data.d;
                if (successFunc != null)
                    successFunc(data.d);
            }
            else {
                $res = data;
                if (successFunc != null)
                    successFunc(data);
            }
        },
        error: function () {
            if (errorFunc != null)
                errorFunc();
        }


    });
    return $res;
}

// -- GetUserById
function GetUserById(id, successFunc, errorFunc) {
    /// <summary></summary>
    /// <param name="id" type="string">id</param>
    /// <param name="successFunc" type="function">Success Function</param>
    /// <param name="errorFunc" type="function">Error Function</param>
    /// <returns type="User">GetUserByIdResult as User</returns>
    var $res = new User();
    $.ajax({
        type: "POST",
        url: "http://localhost:19756/UserService.asmx/GetUserById",
        contentType: "application/json; charset=utf-8",
        async: false,
        cache: false,
        dataType: 'json',
        data: "{id:" + JSON.stringify(id) + "}",
        success: function (data) {
            if (data.hasOwnProperty("d")) {
                $res = data.d;
                if (successFunc != null)
                    successFunc(data.d);
            }
            else {
                $res = data;
                if (successFunc != null)
                    successFunc(data);
            }
        },
        error: function () {
            if (errorFunc != null)
                errorFunc();
        }


    });
    return $res;
}

//--------------------------------------------------------------------------------
// Generate By ASMXBridge.
// No2don is no warranty for this document works correctly.
// If you have any questions.You can  feel free mail to no2don@gmail.com
//--------------------------------------------------------------------------------


//---------------------------------------------------------------------------//
//-----------------------------Object Models--------------------------------//
//--------------------------------------------------------------------------//

// -- Users
var Users = new Array();

// -- User
function User() {
    /// <summary>User</summary>
    User.prototype.Id = '';
    User.prototype.Name = '';
    User.prototype.Age = '';
    User.prototype.Birth = '';
}



//-----------------------------Methods---------------------------------//

// -- UpdateUsers
function UpdateUsers(users, successFunc, errorFunc) {
    /// <summary></summary>
    /// <param name="users" type="ArrayOfUser">users</param>
    /// <param name="successFunc" type="function">Success Function</param>
    /// <param name="errorFunc" type="function">Error Function</param>
    /// <returns type="void">No return data.</returns>
    var $res = null;
    $.ajax({
        type: "POST",
        url: "UserService.asmx/UpdateUsers",
        contentType: "application/json; charset=utf-8",
        async: false,
        cache: false,
        dataType: 'json',
        data: "{users:" + JSON.stringify(users) + "}",
        success: function (data) {
            if (data.hasOwnProperty("d")) {
                $res = data.d;
                if (successFunc != null)
                    successFunc(data.d);
            }
            else {
                $res = data;
                if (successFunc != null)
                    successFunc(data);
            }
        },
        error: function () {
            if (errorFunc != null)
                errorFunc();
        }


    });
    return $res;
}

// -- AddUser
function AddUser(user, successFunc, errorFunc) {
    /// <summary></summary>
    /// <param name="user" type="User">user</param>
    /// <param name="successFunc" type="function">Success Function</param>
    /// <param name="errorFunc" type="function">Error Function</param>
    /// <returns type="string">AddUserResult as string</returns>
    var $res = '';
    $.ajax({
        type: "POST",
        url: "UserService.asmx/AddUser",
        contentType: "application/json; charset=utf-8",
        async: false,
        cache: false,
        dataType: 'json',
        data: "{user:" + JSON.stringify(user) + "}",
        success: function (data) {
            if (data.hasOwnProperty("d")) {
                $res = data.d;
                if (successFunc != null)
                    successFunc(data.d);
            }
            else {
                $res = data;
                if (successFunc != null)
                    successFunc(data);
            }
        },
        error: function () {
            if (errorFunc != null)
                errorFunc();
        }


    });
    return $res;
}

// -- GetAllUsers
function GetAllUsers(successFunc, errorFunc) {
    /// <summary></summary>
    /// <param name="successFunc" type="function">Success Function</param>
    /// <param name="errorFunc" type="function">Error Function</param>
    /// <returns type="Users">GetAllUsersResult as Users</returns>
    var $res = new Array();
    $.ajax({
        type: "POST",
        url: "UserService.asmx/GetAllUsers",
        contentType: "application/json; charset=utf-8",
        async: false,
        cache: false,
        dataType: 'json',
        data: '',
        success: function (data) {
            if (data.hasOwnProperty("d")) {
                $res = data.d;
                if (successFunc != null)
                    successFunc(data.d);
            }
            else {
                $res = data;
                if (successFunc != null)
                    successFunc(data);
            }
        },
        error: function () {
            if (errorFunc != null)
                errorFunc();
        }


    });
    return $res;
}

// -- GetUserById
function GetUserById(id, successFunc, errorFunc) {
    /// <summary></summary>
    /// <param name="id" type="string">id</param>
    /// <param name="successFunc" type="function">Success Function</param>
    /// <param name="errorFunc" type="function">Error Function</param>
    /// <returns type="User">GetUserByIdResult as User</returns>
    var $res = new User();
    $.ajax({
        type: "POST",
        url: "UserService.asmx/GetUserById",
        contentType: "application/json; charset=utf-8",
        async: false,
        cache: false,
        dataType: 'json',
        data: "{id:" + JSON.stringify(id) + "}",
        success: function (data) {
            if (data.hasOwnProperty("d")) {
                $res = data.d;
                if (successFunc != null)
                    successFunc(data.d);
            }
            else {
                $res = data;
                if (successFunc != null)
                    successFunc(data);
            }
        },
        error: function () {
            if (errorFunc != null)
                errorFunc();
        }


    });
    return $res;
}

