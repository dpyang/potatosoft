
$.ajaxSetup({
    cache: false //关闭AJAX相应的缓存
});
function CommentsReply(comUId) {
    $('#CommentsReply').load("ReplyEdit", "id=" + comUId);
}

function NextPageComments(page) {
    var data = "page=" + page + "&strSearch=" + $("#txtSearch").val();
    $.ajax({
        type: "POST",
        url: "CommentsManagePage",
        data: data,
        dataType: "text/plain",
        success: function (msg) {
            if (msg == "Saved") {
                alert("提交成功！");
            }
            else {
                $("#showComments").html(msg);
            }
        }
    });
}
function simbitReply() {
    var page = document.getElementById("currentPage").innerHTML;
    var date = $("#formCommentsReply").serialize() + "&page=" + page;
    $.ajax({
        type: "POST",
        url: $("#formCommentsReply").attr('action'),
        data: date,
        dataType: "text/plain",
        success: function (msg) {
            if (msg == "Saved") {
                alert("提交成功！");
            }
            else {
                $("#showComments").html(msg);
            }
        }
    });
}

function CommentSearch() {
    var data = "strSearch="+$("#txtSearch").val();
    $.ajax({
        type: "POST",
        url: "CommentsSearch",
        data: data,
        dataType: "text/plain",
        success: function (msg) {
            if (msg == "Saved") {
                alert("保存成功！");
            }
            else {
                $("#showComments").html(msg);
            }
        }
    });
}