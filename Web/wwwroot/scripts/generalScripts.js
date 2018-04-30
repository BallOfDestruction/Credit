/* Стилизация селектов */
$(function () {
    $('select, input').styler({
        selectPlaceholder: ''
    });
});

/* Вызов модальных окон */
$.arcticmodal('setDefault', {
    beforeOpen: function () {
        setTimeout(function () {
            $('body').addClass('open_modal');
        }, 100);
    },
    beforeClose: function () {
        $('body').removeClass('open_modal');
    },
    overlay: {
        css: {
            backgroundColor: '#0a192e',
            opacity: .5
        }
    }
});

/* Кнопка наверх */
$(window).scroll(function () {
    if ($(document).scrollTop() > 100) {
        $('#on_top').fadeIn('fast');
    } else {
        $('#on_top').fadeOut('fast');
    }
});

$(document).on('click', '#on_top', function () {
    $('body,html').animate({scrollTop: 0}, 600);
});

$(document).on('click', '#on_top', function () {
    $('body,html').animate({scrollTop: 0}, 600);
});

$(function () {
    $("a[href^='http']").each(function () {
        if ($(this).attr("href").toLowerCase().indexOf(document.location.hostname.toLowerCase()) == -1) {
            $(this).attr("target", "_blank");
        }
    });
});

function callPromptModal(title, text) {
    var modal = $('#prompt-modal');
    modal.find('.modal-title').text(title);
    modal.find('.modal-content').text(text);
    modal.arcticmodal();
    return false;
};
$(document).on('click', '#prompt-modal .btn', function () {
    $('#prompt-modal').arcticmodal('close');
});

function callErrorModal(title, text) {
    var modal = $('#error-modal');
    modal.find('.modal-title').text(title);
    modal.find('.modal-content').text(text);
    modal.arcticmodal();
    return false;
};

//Модальные окна
$(document).on('click', '#error-modal .btn', function () {
    $('#error-modal').arcticmodal('close');
});

