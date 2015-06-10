function tabselected(tab) {//设置当前选中的tag项
    $("a[name='a_tab']").removeClass("selected");
    $("#a" + tab).addClass("selected");
    //.removeClass("selected");
    $("div.module").slideUp();
    $("#tab" + tab).slideDown();
}