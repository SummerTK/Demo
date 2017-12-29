//发布
function Release(Obj) {
    var DivFocusZone;
    var Text = $("#Te__" + Obj).html();
    if (Text.split("<div>").join("").split("<br>").join("").split("</div>").join("").trim() == "") {
        layer.msg("内容不可以为空！", { tiem: 500, icon: 0 });
        return false;
    }
    var Uname = "测试"
    var Date = "时间"
    var Vid = 1
    var DivText = '';
    if (Obj == 0) {
        DivText += '<div class="spComment_8d833d90" data-automation-id="sp-comment-block-root" id="Posted__' + Vid + '">'
        DivText += '<div class="spContentRegion_8d833d90 ms-scaleUpIn100">'
        DivText += '<div class="spAvatar_8d833d90" aria-hidden="true" role="presentation">'
        DivText += '<canvas class="spAvatarImage_8d833d90" data- src="/_layouts/15/userphoto.aspx?size=S&amp;accountname=test2017@mobike.com" data- status="success" data- fingerprint="8a2bceaca7e84b8f4da535d772625c6f" width="32" height="32" > test2017 的照片</canvas >'
        DivText += '</div >'
        DivText += '<div class="spContent_8d833d90">'
        DivText += '<span class="spAuthor_8d833d90" >' + Uname + '</span >'
        DivText += '<div class="spMetadata_8d833d90">'
        DivText += '<a href="#comment=17" class="ms-Link root_c02e569e isEnabled_c02e569e" > ' + Date + '</a >'
        DivText += '</div > '
        DivText += '<div class="spText_8d833d90">'
        DivText += '<span><span>' + Text + '</span></span>'
        DivText += '</div>'
        DivText += '<div class="spActions_8d833d90 ms-fadeIn500">'
        DivText += '<div onclick="Publish(' + Vid + ')">'
        DivText += '<button type="button" class="ms-Button ms-Button--action ms-Button--command spAction_8d833d90 root-79  button button-primary button-rounded button-small" data-automation-id="comment-reply-button" aria-label="答复' + Uname + '" data-is-focusable="true">'
        DivText += ' <img src= "Images/back.png" />'
        DivText += '<div class="ms-Button-flexContainer flexContainer-80">'
        DivText += '<div class="ms-Button-textContainer textContainer-81">'
        DivText += '<div class="ms-Button-label label-83">回复</div>'
        DivText += '</div>'
        DivText += '</div>'
        DivText += '</button>'
        DivText += '</div>'
        DivText += '</div>'
        DivText += '</div>'
        DivText += '</div>'
        DivText += '<div class="spComments_8d833d90" id="Se__' + Vid + '"></div>'
        DivText += '</div>'
        DivFocusZone = $("#Posted__" + Obj);
    }
    else {
        DivText += '<div class="spComment_8d833d90" data-automation-id="sp-comment-block-root" id="Posted__' + Vid + '">'
        DivText += '<div class="spContentRegion_8d833d90 ms-scaleUpIn100">'
        DivText += '<div class="spAvatar_8d833d90" aria-hidden="true" role="presentation">'
        DivText += '<canvas class="spAvatarImage_8d833d90" data- src="/_layouts/15/userphoto.aspx?size=S&amp;accountname=test2017@mobike.com" data- status="success" data- fingerprint="8a2bceaca7e84b8f4da535d772625c6f" width="32" height="32" > test2017 的照片</canvas >'
        DivText += '</div >'
        DivText += '<div class="spContent_8d833d90">'
        DivText += '<span class="spAuthor_8d833d90" >' + Uname + '</span >'
        DivText += '<div class="spMetadata_8d833d90">'
        DivText += '<a href="#comment=17" class="ms-Link root_c02e569e isEnabled_c02e569e" > ' + Date + '</a >'
        DivText += '</div > '
        DivText += '<div class="spText_8d833d90">'
        DivText += '<span><span>' + Text + '</span></span>'
        DivText += '</div>'
        DivText += '</div>'
        DivText += '</div>'
        DivText += '<div class="spComments_8d833d90 Se_Id" id="Se__' + Vid + '"></div>'
        DivText += '</div>'
        DivFocusZone = $("#Se__" + Obj);
    }
    DivFocusZone.prepend(DivText)
    var Text = $("#Te__" + Obj).html("");
    $("#sp-comments span").html()
}

//回复
function Publish(Obj) {
    var Test = "#Se__" + Obj;
    var DivFocusZone = $(Test);
    var DivText = '';
    $(".Se_Id:form").remove();
    DivFocusZone.html(DivText)
    DivText += '<div class="spReply_8d833d90">'
    DivText += '<div class="spAvatar_8d833d90" aria-hidden="true" role="presentation">'
    DivText += '<canvas class="spAvatarImage_8d833d90" data-src="/_layouts/15/userphoto.aspx?size=S&amp;accountname=test2017@mobike.com" data-status="success" width= "32" height= "32" data- fingerprint="8a2bceaca7e84b8f4da535d772625c6f" > test2017 的照片</canvas >'
    DivText += '</div>'
    DivText += '<div class="spField_8d833d90">'
    DivText += '<div aria-label="添加注释" class="spInput_8d833d90" contenteditable="true" placeholder="添加注释" id="Te__' + Obj + '" role="textbox"></div>'
    DivText += '</div>'
    DivText += '<div class="spReplyButtonBlock_8d833d90">'
    DivText += '<button id= "Btn__' + Obj + '"  onclick= "Release(' + Obj + ')" type= "button" class="ms-Button ms-Button--primary spButton_8d833d90 is-disabled root-85 button button-primary button-rounded button-small" data- automation - id="sp-comment-post" aria- label="发布" data- is - focusable="false" >'
    DivText += '<div class="ms-Button-label label-75">发布</div>'
    DivText += '</button >'
    DivText += '</div>'
    DivText += '</div>'
    DivFocusZone.append(DivText)
}
