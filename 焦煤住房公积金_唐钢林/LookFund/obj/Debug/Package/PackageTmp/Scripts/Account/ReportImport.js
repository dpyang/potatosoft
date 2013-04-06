
function uploadFile() {
    var options = {
        success: showResponse
    };
    $("#filePost").ajaxSubmit(options);
}

function uploadHistoryFile() {
    var options = {
        success: showResponse
    };
    $("#fileHistoryPost").ajaxSubmit(options);
}
function showResponse(responseText, statusText) {
    alert(responseText);
}

function uploadSvae() {    
    if(confirm("请将每一个月的上传完了才保存，确定现在就要保存？")) {
        showBg();
        var data = "ddlAddress=" + $("#ddlAddress").val();
        $.ajax({
            type: "POST",
            url: "BaseImportSave",
            data: data,
            dataType: "text/plain",
            success: function (msg) {
                alert(msg);
                closeBg();
            }
        });
    }
}
function uploadHistorySvae() {
    if (confirm("请将每一个月的上传完了才保存，确定现在就要保存？")) {
        showBg();
        var data = "ddlAddress=" + $("#ddlAddress").val();
        $.ajax({
            type: "POST",
            url: "HistoryImportSave",
            data: data,
            dataType: "text/plain",
            success: function (msg) {
                alert(msg);
                closeBg();
            }
        });
    }
}