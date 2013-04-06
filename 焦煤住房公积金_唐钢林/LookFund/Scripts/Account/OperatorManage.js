
function EmployeAdd() {
    $("#right").load("OperatorAdd");
}

function EmployeSaveAdd() {
    $.ajax({
        type: "POST",
        url: $("#OperatorAddForm").attr('action'),
        data: $("#OperatorAddForm").serialize(),
        dataType: "text/plain",
        success: function (msg) {
            if (msg == "Saved") {
                alert("保存成功！");
                $("#right").load("OperatorManage");
            }
            else {
                $("#right").html(msg);
            }
        }
    });
}

function EmployeUpdate(empUid) {
    $.ajax({
        type: "POST",
        url: "OperatorUpdateInit",
        data: "uid=" + empUid,
        dataType: "text/plain",
        success: function (msg) {
            if (msg == "Saved") {
                alert("提交成功！");
            }
            else {
                $("#right").html(msg);
            }
        }
    });
}

function EmployeUpdateSave() {
    
    var strlogin = $("#LoginName").val();
    var strName = $("#ShowName").val();
    var strkind = $("#EmpKind").val();
    var strphone = $("#EmpPhone").val();
    var strEmail = $("#EmpEmail").val();
    var struid = $("#Uid").val();
    var date = "LoginName=" + strlogin + "&ShowName=" + strName + "&EmpKind=" + strkind + "&EmpPhone=" + strphone + "&EmpEmail=" + strEmail + "&Uid=" + struid;
    $.ajax({
        type: "POST",
        url: $("#OperatorUpdateForm").attr('action'),
        data: date,
        dataType: "text/plain",
        success: function (msg) {
            if (msg == "Saved") {
                alert("保存成功！");
                $("#right").load("OperatorManage");
            }
            else {
                $("#right").html(msg);
            }
        }
    });
}

function EmployeDel(empUid) {
    if (confirm("确定要删除此管理员？")) {
        var page = document.getElementById("currentPage").innerHTML;
        $.ajax({
            type: "POST",
            url: "OperatorDel",
            data: "uid=" + empUid + "&page=" + page,
            dataType: "text/plain",
            success: function (msg) {
                alert("删除成功")
                $("#right").html(msg);
            }
        });
    }
}

function EmployeFunction(empUid) {
    $("#right").load("OperatorFun", "uid=" + empUid);
}

function EmployeFunSave() {
    var objli = document.getElementById("rootNode");
    var arr = objli.getElementsByTagName("INPUT");
    var leveCode = "";
    for (var i = 1; i < arr.length; i++) {
        if (arr[i].checked) {
            if (leveCode == "")
                leveCode = arr[i].value;
            else
                leveCode += "," + arr[i].value;
        }
    }
    var empUid = objli.firstChild.value;
    $.ajax({
        type: "POST",
        url: "OperatorFunSave",
        data: "arrCode=" + leveCode + "&empUId=" + empUid,
        dataType: "text/plain",
        success: function (msg) {
            alert("保存成功");
            $("#right").html(msg);
        }
    });
}

function EmployeReturn() {
    $("#right").load("OperatorManage");
}

function OperatorNextPage(page) {
    $.ajax({
        type: "POST",
        url: "OperatorPage",
        data: "page=" + page,
        dataType: "text/plain",
        success: function (msg) {
            if (msg == "Saved") {
                alert("提交成功！");
            }
            else {
                $("#right").html(msg);
            }
        }
    });
}

function SelectTreeNode(obj) {
    var objli = obj.parentNode;
    var arr = objli.getElementsByTagName("INPUT");
    for (var i = 0; i < arr.length; i++) {
        arr[i].checked = obj.checked;
    }
    if (objli.id != "rootNode") {
        topNode(objli);
    }
}

function topNode(objli) {
    if (objli.id != "rootNode") {
        var upli = objli.parentNode.parentNode;
        var arr = upli.getElementsByTagName("INPUT");
        var b = false;
        for (var i = 1; i < arr.length; i++) {
            if (arr[i].checked) {
                b = true;
                break;
            }
        }
        upli.firstChild.checked = b;
        topNode(upli);
    }
}