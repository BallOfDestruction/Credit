function ChangeWorkStep(id, url, elem) {
    var html = $("#workStepSectionInnerHtmlDiv" + id).html();
    $("#workStepSectionInnerHtmlDiv").html(html);
    $("#workStepSectionImageSrc").attr("src", url);

    $('.WorkStepSectionWithAction').each(function (i, elem) {
        elem.classList.remove('active');
    });
    elem.classList.add('active');
}

function openUrl(url) {
    document.location = url;
}