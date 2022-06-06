$("#search_text").on("focus keyup", function () {
    CententRemove();
    ClassRemove();
    // 获取搜索条件的值
    var search_type = $('input:radio[name="search_type"]:checked').val();
    // 获取文本框的值
    var search_text = $("#search_text").val();
    if (search_text == "") {
        return;
    }
    else {
        // 本地搜索
        if (search_type === "local") {
            $.ajax({
                url: "/Home/GetSearch",
                type: "post",
                data: { "key": search_text },
                dataType: "json",
                success: function (data) {
                    LocalSearch(data);
                }
            });
        }
        // 百度搜索
        else if (search_type === "baidu") {
            var scriptSearch = document.createElement("script");
            scriptSearch.src = "https://sp0.baidu.com/5a1Fazu8AA54nxGko9WTAnF6hhy/su?wd=" + search_text + "&cb=BaiduSearch";
            document.body.appendChild(scriptSearch);
            document.body.removeChild(scriptSearch);
        }
    }
});
// 列表点击事件
$("#search_result").on("mousedown", "li", function () {
    // 获取搜索条件的值
    var search_type = $('input:radio[name="search_type"]:checked').val();
    // id的值
    var id = $(this).find('span:eq(0)');
    var idval = id.text();
    // 本地搜索
    if (search_type === "local") {
        $.ajax({
            url: "/Home/AddSearch",
            type: "post",
            data: { "id": idval },
            dataType: "json",
            success: function (data) {
                if (data === "success") {
                    return;
                }
            }
        });
    }
});
// 本地搜索
function LocalSearch(data) {
    if (data == "404") {
        return;
    }
    else {
        ClassAdd();
        $.each(data, function (i, item) {
            $("#search_result").append("<li><span  class='id'>" + item.Id + "</span><a href='https://www.baidu.com/s?ie=UTF-8&wd=" + item.Text + "'>" + item.Text + "</a><span class='frequency'>" + item.Frequency +"</span></li>");
        });
    }
}
// 百度搜索
function BaiduSearch(myJson) {
    var data = myJson.s;
    if (data.length === 0) {
        return;
    }
    else {
        ClassAdd();
        $.each(data, function (i, item) {
            $("#search_result").append("<li><a href='https://www.baidu.com/s?ie=UTF-8&wd=" + item + "'>" + item + "</a></li>");
        });
    }
}
//失去焦点
$("#search_text").on("blur", function () {
    CententRemove();
    ClassRemove();
});
// 添加样式
function ClassAdd() {
    $("#search_result").addClass("search_result");
    $("#search_text").addClass("search_text_success");
}
// 删除样式
function ClassRemove() {
    $("#search_result").removeClass("search_result");
    $("#search_text").removeClass("search_text_success");
}
// 删除内容
function CententRemove() {
    $("#search_result").html("");
}