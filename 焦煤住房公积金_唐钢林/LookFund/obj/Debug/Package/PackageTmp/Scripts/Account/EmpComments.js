
$.ajaxSetup({
    cache: false //关闭AJAX相应的缓存
});

function simbitComments() {
    if ($('#Conent').val() == "") {
        alert("意见不能为空");
    }
    if (confirm("确定要提交意见？")) {
        $.ajax({
            type: "POST",
            async: false,
            url: $("#FormComments").attr('action'),
            data: $("#FormComments").serialize(),
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
}