function SumbitFundLook() {
    var smonth = $("#MothDate").val();
    var patrn = /^[1-2]\d{3}-(0?[1-9]|1[0-2])$/;
    if ((!patrn.test(smonth)) && smonth != "") {
        alert("月份格式不正确！");
        return false;
    }
    $.ajax({
        type: "POST",
        url: $("#FormFundLook").attr('action'),
        data: $("#FormFundLook").serialize(),
        dataType: "text/plain",
        success: function (msg) {
            if (msg == "Saved") {
                alert("提交成功！");
            }
            else {
                $("#LookResult").html(msg);
            }
        }
    });
}

function getUnitList() {
    var strcom = $('#strCompany').val();
    $.ajax({
        type: "POST",
        url: "SelectListUnit",
        data: "company=" + strcom,
        dataType: "text/plain",
        success: function (msg) {
            if (msg == "Saved") {
                alert("提交成功！");
            }
            else {
                $("#changeUnit").html(msg);
            }
        }
    });
}
function NextPageBase(page) {
    var sdate = $("#FormFundLook").serialize() + "&page=" + page;
    $.ajax({
        type: "POST",
        url: "EmpFundLookPage",
        data:sdate,
        dataType: "text/plain",
        success: function (msg) {
            if (msg == "Saved") {
                alert("提交成功！");
            }
            else {
                $("#LookResult").html(msg);
            }
        }
    });
}

function EmpFundLookInfo(strGuid) {
    var data = "strGuid=" + strGuid + "&strCompany=" + $('#strCompany').val();
    $.ajax({
        type:"POST",
        url: "EmpFundLookInfo",
        data:data,
        dataType: "text/plain",
        success: function (msg) {
            if (msg == "Saved") {
                alert("提交成功！");
            }
            else {
                $("#LookResult").html(msg);
            }
        }
    });
}

function EmpHistoryFundLook() {
    var strbegion = $("#BegionTime").val();
    var strend = $("#EndTime").val();
    var patrn = /^[1-2]\d{3}(0?[1-9]|1[0-2])$/;
    if ((!patrn.test(strbegion)) && strbegion != "") {
        alert("开始日期格式不正确！");
        return false;
    }
    if ((!patrn.test(strend)) && strend != "") {
        alert("结束日期格式不正确！");
        return false;
    }
    $.ajax({
        type: "POST",
        url: $("#FormHistFundLook").attr('action'),
        data: $("#FormHistFundLook").serialize(),
        dataType: "text/plain",
        success: function (msg) {
            if (msg == "Saved") {
                alert("提交成功！");
            }
            else {
                $("#LookResult").html(msg);
            }
        }
    });
}

//----上报情况统计－－－
function NextPageStat(page) {
    var data = $("#FormReportStatLook").serialize() + "&page=" + page;
    $.ajax({
        type: "POST",
        url: "ReportStatepage",
        data: data,
        dataType: "text/plain",
        success: function (msg) {
            if (msg == "Saved") {
                alert("提交成功！");
            }
            else {
                $("#LookResult").html(msg);
            }
        }
    });
}
function ReportStatLook() {
    var smonth = $("#StatDate").val();
    var patrn = /^[1-2]\d{3}-(0?[1-9]|1[0-2])$/;
    if ((!patrn.test(smonth)) && smonth != "") {
        alert("开始时间格式错误！");
        return false;
    }
    var emonth = $("#EndDate").val();
    if ((!patrn.test(emonth)) && emonth != "") {
        alert("结束时间格式错误！");
        return false;
    }
    $.ajax({
        type: "POST",
        url: $("#FormReportStatLook").attr('action'),
        data: $("#FormReportStatLook").serialize(),
        dataType: "text/plain",
        success: function (msg) {
            if (msg == "Saved") {
                alert("提交成功！");
            }
            else {
                $("#LookResult").html(msg);
            }
        }
    }); 

}