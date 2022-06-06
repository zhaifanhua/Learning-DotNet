﻿var UploadPath = "";
//开始上传
function UploadStart() {
    var file = $("#path")[0].files[0];
    AjaxFile(file, 0);
}
function AjaxFile(file, i) {
    var name = file.name, //文件名
        size = file.size, //总大小shardSize = 2 * 1024 * 1024,
        shardSize = 2 * 1024 * 1024,//以2MB为一个分片
        shardCount = Math.ceil(size / shardSize); //总片数
    if (i >= shardCount) {
        return;
    }
    //计算每一片的起始与结束位置
    var start = i * shardSize,
        end = Math.min(size, start + shardSize);
    //构造一个表单，FormData是HTML5新增的
    var form = new FormData();
    form.append("data", file.slice(start, end)); //slice方法用于切出文件的一部分
    form.append("lastModified", file.lastModified);
    form.append("fileName", name);
    form.append("total", shardCount); //总片数
    form.append("index", i + 1); //当前是第几片
    UploadPath = file.lastModified
    //Ajax提交文件
    $.ajax({
        url: "/Upload/UploadFile",
        type: "POST",
        data: form,
        async: true, //异步
        processData: false, //很重要，告诉jquery不要对form进行处理
        contentType: false, //很重要，指定为false才能形成正确的Content-Type
        success: function (result) {
            if (result != null) {
                i = result.number++;
                var num = Math.ceil(i * 100 / shardCount);
                $("#output").text(num + '%');
                AjaxFile(file, i);
                if (result.mergeOk) {
                    var filepath = $("#path");
                    filepath.after(filepath.clone().val(""));
                    filepath.remove();//清空input file
                    $('#upfile').val('请选择文件');
                    alert("success!!!");
                }
            }
        }
    });
}