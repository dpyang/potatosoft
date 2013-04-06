function NextPage(i) {
    $.ajax({
        type: "POST",
        url: "VerifyFund",
        data: "page="+i,
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
function VerifySucced(cid) {
    if (!confirm("确定要提交验证通过"))
        return false;
    var page = document.getElementById("currentPage").innerHTML;
    $.ajax({
        type: "POST",
        url: "VerifySucced",
        data: "strGuid=" + cid + "&page=" + page,
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

function PageSucced() {
  
    var Table = document.getElementById("showFund");
    var obj = Table.getElementsByTagName("Input");
    var arrGuid = "";
    for (var i = 0; i < obj.length; i++) {
        if (obj[i].type == "hidden") {
            if (arrGuid == "") {
                arrGuid = obj[i].value;
            }
            else {
                arrGuid += "," + obj[i].value;
            }
        }
    }
    if (arrGuid == "") {
        alert("无数据");
        return false;
    }
    if (!confirm("确定要提交当页验证通过"))
        return false;
    var page = document.getElementById("currentPage").innerHTML;
    $.ajax({
        type: "POST",
        url: "PageSucced",
        data: "arrGuid=" + arrGuid + "&page=" + page,
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

function SuccedAll() {
    if (!confirm("确定要提交全部验证通过"))
        return false;
    $.ajax({
        type: "POST",
        url: "SuccedAll",
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