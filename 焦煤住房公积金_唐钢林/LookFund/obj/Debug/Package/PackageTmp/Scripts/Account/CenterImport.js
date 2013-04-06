function CanteruploadFile() {
    var options = {
        success: showResponse
    };
    $("#CanterfilePost").ajaxSubmit(options);
}

function SumbitCenterLook() {
    $.ajax({
        type: "POST",
        url: $("#FormCenterLook").attr('action'),
        data: $("#FormCenterLook").serialize(),
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
function NextPageCenter(page) {
    var sdate = $("#FormCenterLook").serialize() + "&page=" + page;
    $.ajax({
        type: "POST",
        url: "EmpCenterLookPage",
        data: sdate,
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