function submitLogin() {
    var adress = $("#strAddress").val();
    if (adress == "0") {
        alert("请选择所属单位");  
        return ;
    }
    var subform = document.forms[0];
    if (subform.fireEvent) {
        subform.fireEvent('onsubmit');
    }
    else if (document.createEvent) {
        var ev = document.createEvent('HTMLEvents');
        ev.initEvent('submit', false, true);
        subform.dispatchEvent(ev)
    }
    else {
        alert("浏览器不支持ajax。");
    }
}
function submitIndexLogin() {
    var subform = document.forms[0];
    if (subform.fireEvent) {
        subform.fireEvent('onsubmit');
    }
    else if (document.createEvent) {
        var ev = document.createEvent('HTMLEvents');
        ev.initEvent('submit', false, true);
        subform.dispatchEvent(ev)
    }
    else {
        alert("浏览器不支持ajax。");
    }
}
$(document).keypress(function (e) {
    if (e.which == 13) {
        var uname = $("#LoginName").val();
        var npwd = $("#LoginPwk").val();
        if (uname != "" && npwd != "") {
            submitLogin();
        }
        else {
            alert("身份证号或密码不能为空");
        }
    }
});