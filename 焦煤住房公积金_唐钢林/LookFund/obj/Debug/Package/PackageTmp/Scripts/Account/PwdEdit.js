function UpeatePwd() {
    var strOld = $('#OldPwd').val();
    var strNew = $('#NewPwd').val();
    var strConfig = $('#ConfirmPwd').val();
    if (strOld == "") {
        alert("原密码不能为空");
        return false;
    }
    if (strNew == "") {
        alert("新密码不能为空");
        return false;
    }
    if (strConfig != strNew) {
        alert("确定密码和新密码不一至");
        return false;
    }
    if (confirm("确定要提交修改密码？")) {
        $.ajax({
            type: "POST",
            async: false,
            url: $("#formEditRwd").attr('action'),
            data: $("#formEditRwd").serialize(),
            dataType: "text/plain",
            success: function (msg) {
                if (msg == "Saved") {
                    alert("修改成功！");
                    window.location.href = "../Home.html/Login";
                }
                else {
                    $("#right").html(msg);
                }
            }
        }); 
    }
}

function UserPwdInit() {
    var loginName = $("#txtIdCard").val();
    if (loginName == "") {
        alert("要初始化的身份证号不能为空。");
        return false;
    }
    $.ajax({
        type: "POST",
        url: "PwdInit",
        data: "LoginName=" + loginName,
        success: function (msg) {
            if (msg == "Saved") {
                alert("初始化成功！");
            }
            else {
                alert("无该身份证号，请重新输入。");
            }
        }
    });
}

function CancelLogin() {
    $.ajax({
        type: "POST",
        url: "CancelLogin",
        success: function (msg) {
            if (msg == "Saved") {
                window.location.href = "../Home.html/Login";
            }
            else {
                alert("退出失败！");
            }
        }
    });
}

function PrintPage() {
    if (document.getElementById("htmlPrint") != null) {
        var newWin = window.open('about:blank', '', '');
        var titleHTML = document.getElementById("htmlPrint").innerHTML;
        var str = "<html><head><title>打印</title> <style type='text/css'>";
        str += "table { border-collapse: collapse; border-spacing: 0px;} td{ border:solid 1px black;} </style>";
        str += "</head><body>" + titleHTML + "</body></html>";
        newWin.document.write(str);
        newWin.document.location.reload();
        newWin.print();
        newWin.close();
    }
    else {
        alert("此页面没有打印的必要！");
    }
}