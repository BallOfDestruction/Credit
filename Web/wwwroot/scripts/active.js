//Мобильная навигация
// $(document).on('click', 'header .header_switcher', function () {
// 	var viewport = $(window).width();
// 	if(viewport <= 680){
// 		$(this).parent().toggleClass('head_opened');
// 	}else{
// 		$(this).parent().removeClass('head_opened');
// 	}
// });
// $(window).resize(function(){
// 	var viewport = $(window).width();
// 	if(viewport > 680){
// 		$('header').removeClass('head_opened');
// 	}
// });
//Модальные окна
// $(document).on('click', '#call_back_trig', function () {
// 	yaCounter26745762.reachGoal('call');
// 	$('#call_back').arcticmodal().find('.clicked').removeClass('clicked').find('.hide_block').css('display','block').end().find('.after_submit').css('display','none');
// 	$('#call_back form')[0].reset();
// });
// $(document).on('click', '#back_connect_trig', function () {
// 	yaCounter26745762.reachGoal('consult');
// 	$('#back_connect').arcticmodal().find('.clicked').removeClass('clicked').find('.hide_block').css('display','block').end().find('.after_submit').css('display','none');
// 	$('#back_connect form')[0].reset();
// });
// $(document).on('click', '#sh_oz_trig', function () {
// 	yaCounter26745762.reachGoal('paint');
// 	$('#sh_oz').arcticmodal().find('.clicked').removeClass('clicked').find('.hide_block').css('display','block').end().find('.after_submit').css('display','none');
// 	$('#sh_oz form')[0].reset();
// });
// $(document).on('click', '#constrakt_zash_trig', function () {
// 	yaCounter26745762.reachGoal('plate');
// 	$('#constrakt_zash').arcticmodal().find('.clicked').removeClass('clicked').find('.hide_block').css('display','block').end().find('.after_submit').css('display','none');
// 	$('#constrakt_zash form')[0].reset();
// });
// $(document).on('click', '#free_table_link', function () {
// 	$('#free_table_link_modal').arcticmodal();
// 	yaCounter26745762.reachGoal('paint-list');
// });
// $(document).on('click', '.spes_layers', function () {
// 	$('#many_comp_layers').arcticmodal().find('.clicked').removeClass('clicked').find('.hide_block').css('display','block').end().find('.after_submit').css('display','none');
// 	$('#many_comp_layers form')[0].reset();
// });
// $(document).on('click', '.spes_layers_spesial_1', function () {
// 	$('#many_comp_layers_1').arcticmodal().find('.clicked').removeClass('clicked').find('.hide_block').css('display','block').end().find('.after_submit').css('display','none');
// 	$('#many_comp_layers_1 form')[0].reset();
// });
// $(document).on('click', '.spes_layers_spesial_2', function () {
// 	$('#many_comp_layers_2').arcticmodal().find('.clicked').removeClass('clicked').find('.hide_block').css('display','block').end().find('.after_submit').css('display','none');
// 	$('#many_comp_layers_2 form')[0].reset();
// });
// $(document).on('click', '.free_consultation_trig', function () {
// 	$('#free_consultation').arcticmodal().find('.clicked').removeClass('clicked').find('.hide_block').css('display','block').end().find('.after_submit').css('display','none');
// 	$('#free_consultation form')[0].reset();
// });
// $(document).on('click', '.youtubefoto', function (){
// 	var you_number = $(this).attr("youtube");
// 	$.arcticmodal({
// 		type: 'ajax',
// 		url: 'ajax/youtube'+ you_number +'.html'
// 	});
// });
// $(document).on('click', '.quest_and_text button', function () {
// 	$('#quest_and_text').arcticmodal().find('.clicked').removeClass('clicked').find('.hide_block').css('display','block').end().find('.after_submit').css('display','none');
// 	$('#quest_and_text form')[0].reset();
// });


//Цели
// $(document).on('click', '.price_mtag', function () {
// 	yaCounter26745762.reachGoal('menu-price');
// });
// $(document).on('click', '#url_first .prod_same_header', function () {
// 	yaCounter26745762.reachGoal('paint-open');
// });
// $(document).on('click', '#url_first .prod_same_header_open', function () {
// 	yaCounter26745762.reachGoal('paint-close');
// });
// $(document).on('click', '#foot_send', function () {
// 	var cover = $(this).closest('.hide_block');
// 	var mail = cover.find('.email').val();
// 	var name = cover.find('.name').val();
// 	var message = cover.find('.message').val();
// 	var yaClientField = {
// 		Почта: mail,
// 		Имя: name,
// 		Сообщение: message
// 	};
// 	yaCounter26745762.reachGoal('contact-send', yaClientField);
// });
// $(document).on('click', '#call_back .subButton', function () {
// 	var cover = $(this).closest('.hide_block');
// 	var phone = cover.find('.phone').val();
// 	var name = cover.find('.name').val();
// 	var message = cover.find('.message').val();
// 	var yaClientField = {
// 		Телефон: phone,
// 		Имя: name,
// 		Сообщение: message
// 	};
// 	yaCounter26745762.reachGoal('call-requested', yaClientField);
// });
// $(document).on('click', '#back_connect .subButton', function () {
// 	var cover = $(this).closest('.hide_block');
// 	var mail = cover.find('.email').val();
// 	var name = cover.find('.name').val();
// 	var message = cover.find('.message').val();
// 	var yaClientField = {
// 		Почта: mail,
// 		Имя: name,
// 		Сообщение: message
// 	};
// 	yaCounter26745762.reachGoal('consult-send', yaClientField);
// });
// $(document).on('click', '#sh_oz .subButton', function () {
// 	var cover = $(this).closest('.hide_block');
// 	var mail = cover.find('.email').val();
// 	var name = cover.find('.name').val();
// 	var message = cover.find('.message').val();
// 	var yaClientField = {
// 		Почта: mail,
// 		Имя: name,
// 		Сообщение: message
// 	};
// 	yaCounter26745762.reachGoal('paint-contact-send', yaClientField);
// });
// $(document).on('click', '#constrakt_zash .subButton', function () {
// 	var cover = $(this).closest('.hide_block');
// 	var mail = cover.find('.email').val();
// 	var name = cover.find('.name').val();
// 	var message = cover.find('.message').val();
// 	var yaClientField = {
// 		Почта: mail,
// 		Имя: name,
// 		Сообщение: message
// 	};
// 	yaCounter26745762.reachGoal('plate-contact-send', yaClientField);
// });

/*$(document).on('click', '.subButton', function () {
 _gaq.push(['_trackEvent', 'lead', 'click']);
 });*/
// $(document).on('click', '#many_comp_layers_1 .subButton', function () {
// 	yaCounter26745762.reachGoal('price-lead', yaClientField);
// });
// $(document).on('click', '#many_comp_layers_2 .subButton', function () {
// 	yaCounter26745762.reachGoal('price-lead', yaClientField);
// });
//
// $(document).ready(function() {
// 		$("header li a").click(function() {
// 				$("html, body").animate({
// 						scrollTop: $($(this).attr("href")).offset().top - 40 + "px"
// 				}, {
// 						duration: 500
// 				});
// 				return false;
// 		});
// });
/*$(document).on('click', '.subButton', function () {
 item = $(this).parent().parent();
 if(item.hasClass('clicked')){
 return false
 }
 else{
 item.submit(function(e){
 item.find('.hide_block').css('display','none');
 item.find('.after_submit').css('display','block');
 item.addClass('clicked');
 e.preventDefault();
 $.ajax({
 url: "php/contact.php",
 type: "POST",
 data: $(item).serialize(),
 });
 });
 }
 });*/
// $('form').submit(function(e){
// 	$(this).find('.hide_block').css('display','none');
// 	$(this).find('.after_submit').css('display','block');
// 	$(this).addClass('clicked');
// 	e.preventDefault();
// 	$.ajax({
// 		url: "php/contact.php",
// 		type: "POST",
// 		data: $(this).serialize()
// 	}).done(function() {
// 		yaCounter26745762.reachGoal('lead');
// 	});
// });
// $(document).on('click', '.prod_same_header', function () {
// 	/*stop();*/
// 	var item = $(this);
// 	item.addClass('prod_same_header_open').removeClass('prod_same_header');
// 	item.closest('.products--lim').find('.slideBlockClosed').slideDown();
// });
// $(document).on('click', '.prod_same_header_open', function () {
// 	/*stop();*/
// 	var item = $(this);
// 	item.addClass('prod_same_header').removeClass('prod_same_header_open');
// 	item.closest('.products--lim').find('.slideBlockClosed').slideUp();
// });
// $(document).ready(function(){
// 	$(".owl-carousel").owlCarousel({
// 		items:1,
// 		dots: true,
// 		autoplay: true,
// 		loop:true
// 	});
// });
// var url_parse = $.url('?');
// if(url_parse == 1){
// 	$("#url_first").find('.slideBlockClosed').slideUp().end().find('.prod_same_header_open').addClass('prod_same_header').removeClass('prod_same_header_open');
// } else if (url_parse == 2){
// 	$("#url_second").find('.slideBlockClosed').slideUp().end().find('.prod_same_header_open').addClass('prod_same_header').removeClass('prod_same_header_open');
// } else if (url_parse == 3){
// 	$("#url_first").find('.slideBlockClosed').slideUp().end().find('.prod_same_header_open').addClass('prod_same_header').removeClass('prod_same_header_open');
// 	$("#url_second").find('.slideBlockClosed').slideUp().end().find('.prod_same_header_open').addClass('prod_same_header').removeClass('prod_same_header_open');
// }
//

// $("input.phone").mask("+7(999) 999-9999");
//
// $(document).on('click', '.burger', function () {
//     $(this).closest("nav").toggleClass("active");
//     $(".back_ground").toggleClass("active");
// });
//
// $(document).on('click', '.show_hidden_content', function () {
//     $(this).hide().closest(".row").find(".hidden_content").slideDown();
// });
//
// $(document).on('click', '.back_ground.active', function () {
//     $("nav .burger").click();
// });
//
// /*Modal*/
// $(document).on('click', '.btn_callback_MA', function () {
//     $('#callBack').arcticmodal();
// });
//
// $(document).on('click', '.btn_consultModal_MA', function () {
//     $('#consultModal').arcticmodal();
// });
//
// $(document).on('click', '.price_grid .item', function () {
//     var $this = $(this).find('.btn_pricegridModal_MA');
//     var dop_info_block = $(".pricegridModal_double");
//     if (dop_info_block.length) {
//         var this_error_id = $this.data("errorid");
//         var this_section_name = $this.closest(".row").data("section_name");
//         var $content = $this.closest(".item").find(".content");
//         var title = $content.find(".item_title").text();
//         var error_list = $('#pricegridModal').find(".double_info ul");
//         error_list.html("");
//         $('#pricegridModal').find(".double_info .title").text(title);
//         $.getJSON("/" + this_section_name + "/data.json", function (data) {
//             var items = [];
//             $.each(data[this_error_id], function (key, val) {
//                 items.push("<li>" + val.Error + "<span>от "+ val.Price +" ₽</span>" + "</li>");
//             });
//             $.each(items, function (key, val) {
//                 error_list.append(val);
//             });
//         });
//     }
//     $('#pricegridModal').arcticmodal();
// });
//
// $(document).on('click', '.btn_pricegridModal_MA', function () {
//     var dop_info_block = $(".pricegridModal_double");
//     if (dop_info_block.length) {
//         var this_error_id = $(this).data("errorid");
//         var this_section_name = $(this).closest(".row").data("section_name");
//         var $content = $(this).closest(".item").find(".content");
//         var title = $content.find(".item_title").text();
//         var error_list = $('#pricegridModal').find(".double_info ul");
//         error_list.html("");
//         $('#pricegridModal').find(".double_info .title").text(title);
//         $.getJSON("/" + this_section_name + "/data.json", function (data) {
//             var items = [];
//             $.each(data[this_error_id], function (key, val) {
//                 items.push("<li>" + val.Error + "<span>от "+ val.Price +" ₽</span>" + "</li>");
//             });
//             $.each(items, function (key, val) {
//                 error_list.append(val);
//             });
//         });
//     }
//     $('#pricegridModal').arcticmodal();
// });
//
// $(document).on('click', '#garanty12_btn', function () {
//     $('#garanty12').arcticmodal();
// });
//
// $(document).on('click', '#detail_btn', function () {
//     $('#detail').arcticmodal();
// });
//
// $(document).on('click', '#brand_btn', function () {
//     $('#brandModal').arcticmodal();
// });
//
// $(document).on('click', '#faster_btn', function () {
//     $('#fasterModal').arcticmodal();
// });
//
// $(document).on('click', '.btn_masterModal_MA', function () {
//     $('#masterModal').arcticmodal();
// });
//
// $(document).on('click', '.btn_brandModal_MA', function () {
//     $('#brandModal').arcticmodal();
// });
//
// $(document).on('click', '.btn_docsModal_MA', function () {
//     $('#docsModal').arcticmodal();
// });
//
// $(document).on('click', '.page_nav .link', function () {
//     var page_scroll = $(window).scrollTop();
//     var href = $(this).data("href");
//     var obj_top = $(href + "").offset().top;
//     if (!$(".page_nav").hasClass("scrolling") && page_scroll != obj_top) {
//         $(".page_nav").addClass("scrolling");
//         setTimeout(function () {
//             $(".page_nav").removeClass("scrolling");
//         }, 599);
//         $('html, body').animate({
//             scrollTop: obj_top
//         }, 600);
//     }
// });
//
// $(document).on('click', '.comment .read_all', function () {
//     var content = $(this).closest(".item").find(".item");
//     $('#review .modal_body').html(content);
//     $("#review").arcticmodal();
// });
//
// // $(function(){
// //     $(document).click(function(event) {
// //         if ($(event.target).closest(".comments_cover .item").length) return;
// //         $('.comments_cover>.item').find('.item').css('display','none');
// //         event.stopPropagation();
// //     });
// // });
//
// // function MounthChange() {
// //     var d = new Date();
// //     var day = d.getUTCDate();
// //     var month = d.getMonth()+1;
// //     var months = ["новогодних каникул","января","февраля","марта","апреля","мая","июня","июля","августа","сентября","октября","ноября","декабря"];
// //     if(month == 1 && day > 1 && day < 8){
// //         $("section.request .title .date").text(months[0]);
// //     }else{
// //         $("section.request .title .date").text(months[month]);
// //     }
// // }
// // MounthChange();
//

/*
 $('form').submit(function (e) {
 e.preventDefault();
 $.ajax({
 url: "/php/contact.php",
 type: "POST",
 data: $(this).serialize()
 });
 var inModal = $(this).closest(".box-modal").length;
 if (inModal) {
 var modalID = $(this).closest(".box-modal").attr('id');
 $('#' + modalID).arcticmodal('close');
 }
 $('#thanks').arcticmodal();
 yaCounter45531846.reachGoal('order');
 setTimeout(function () {
 $('#thanks').arcticmodal('close');
 }, 5000);
 });*/

//anim

$(document).keydown(function (e) {
    // console.log(e.keyCode);
    var slide = $('body').data('slide');
    console.log(slide);
    var slides = $('body').find('section').length;
    var sections = $('body').find('section');
    var scrollTop = window.pageYOffset;
    var wid_height = window.screen.height;
    if(e.keyCode == 38 || e.keyCode == 33){
        if (!$('body').hasClass('double')) {
            if (slide > 0) {
                slide_up_single(e ,slide);
            }
        } else{
            if (slide > 0) {
                slide_up_double(e ,slide)
            }
            $.each(sections, function () {
                if (scrollTop + wid_height > $(this)["0"].offsetTop && scrollTop < $(this)["0"].offsetTop) {
                    $('body').data('slide', $(this).index() - 1);
                }
            });
        }
    }
    if(e.keyCode == 40 || e.keyCode == 34){
        if (!$('body').hasClass('double')) {
            if (slide < slides - 1) {
                slide_down_single(e ,slide);
            }
        }else{
            if (slide > 0) {
                slide_down_double(e ,slide)
            }
            $.each(sections, function () {
                if (scrollTop + wid_height > $(this)["0"].offsetTop && scrollTop < $(this)["0"].offsetTop) {
                    $('body').data('slide', $(this).index() - 1);
                }
            });
        }
    }
});

if (!$(body).hasClass('mobile') && $('.main_page').length > 0) {
    $(window).on('scroll touchmove mousewheel', function (e) {
        var slide = $('body').data('slide');
        var slides = $('body').find('section').length;
        var wid_height = window.screen.height;
        if (!$('body').hasClass('double')) {
            if (e.originalEvent.deltaY < 0 && slide > 0){
                slide_up_single(e, slide);
            }
            if (e.originalEvent.deltaY > 0 && slide < slides - 1) {
                slide_down_single(e ,slide);
            }
        } else {
            var sections = $('body').find('section');
            var scrollTop = window.pageYOffset;
            var wid_height = window.screen.height;
            if (e.originalEvent.deltaY < 0 && slide > 0) {
                slide_up_double(e, slide)
            }
            if (e.originalEvent.deltaY > 0 && slide < slides - 1) {
                slide_down_double(e, slide)
            }
            $.each(sections, function () {
                if (scrollTop + wid_height > $(this)["0"].offsetTop && scrollTop < $(this)["0"].offsetTop) {
                    $('body').data('slide', $(this).index() - 1);
                }
            });
        }
    });

    $(window).scroll(function (e) {
        detect_between();
    });
}

function slide_up_single(e, slide) {
    if (!$('body').hasClass('animating')) {
        var body = $("html, body");
        var to_position = $('body').find('section').eq(slide - 1)["0"].offsetTop;
        body.stop().animate({scrollTop: to_position}, 800, 'swing', function () {
            $('body').removeClass('animating');
        });
        $('body').data('slide', slide - 1);
    }
    $('body').addClass('animating');
    e.preventDefault();
    e.stopPropagation();
    return false;
}

function slide_down_single(e, slide) {
    if (!$('body').hasClass('animating')) {
        var body = $("html, body");
        var to_position = $('body').find('section').eq(slide + 1)["0"].offsetTop;
        body.stop().animate({scrollTop: to_position}, 800, 'swing', function () {
            $('body').removeClass('animating');
        });
        $('body').data('slide', slide + 1);
    }
    $('body').addClass('animating');
    e.preventDefault();
    e.stopPropagation();
    return false;
}

function slide_up_double(e, slide) {
    if (!$('body').hasClass('animating')) {
        var body = $("html, body");
        var to_position = $('body').find('section').eq(slide - 1)["0"].offsetTop;
        body.stop().animate({scrollTop: to_position}, 800, 'swing', function () {
            $('body').removeClass('animating');
        });
        $('body').data('slide', slide - 1);
    }
    $('body').addClass('animating').removeClass('double');
    e.preventDefault();
    e.stopPropagation();
    return false;
}

function slide_down_double(e, slide) {
    if (!$('body').hasClass('animating')) {
        var body = $("html, body");
        var to_position = $('body').find('section').eq(slide)["0"].offsetTop;
        body.stop().animate({scrollTop: to_position}, 800, 'swing', function () {
            $('body').removeClass('animating');
        });
        $('body').data('slide', slide + 1);
    }
    $('body').addClass('animating').removeClass('double');
    e.preventDefault();
    e.stopPropagation();
    return false;
}

$('document').ready(function () {
    var dark_color = $('section[data-clr="dark"]');
    var header_height = $('header')["0"].clientHeight;
    $.each(dark_color, function (key) {
        if($('header').data('pray') == key+1 || $('header').data('pray') == 0){
            console.log(window.pageYOffset, $(this)["0"].offsetTop, $(this)["0"].offsetHeight, header_height / 2);
            if (window.pageYOffset > $(this)["0"].offsetTop - header_height / 2 && window.pageYOffset < $(this)["0"].offsetTop + $(this)["0"].offsetHeight - header_height / 2) {
                $('header').addClass('dark').data('pray', key+1);
            } else {
                $('header').removeClass('dark').data('pray', 0);
            }
        }
    });
});

$(window).scroll(function (e) {
    var dark_color = $('section[data-clr="dark"]');
    var header_height = $('header')["0"].clientHeight;
    $.each(dark_color, function (key) {
        if($('header').data('pray') == key+1 || $('header').data('pray') == 0){
            /*console.log(window.pageYOffset, $(this)["0"].offsetTop, $(this)["0"].offsetHeight, header_height / 2);*/
            if (window.pageYOffset > $(this)["0"].offsetTop - header_height / 2 && window.pageYOffset < $(this)["0"].offsetTop + $(this)["0"].offsetHeight - header_height / 2) {
                $('header').addClass('dark').data('pray', key+1);
            } else {
                $('header').removeClass('dark').data('pray', 0);
            }
        }
    });
});

$('document').ready(function () {
    animateMenu();
    animatePresentation();
    $('.products_grid').owlCarousel({
        responsive: {
            0: {
                items: 1
            },
            1100: {
                items: 2
            }
        },
        nav: true,
        navText: ["", ""]
    });
    $('.projects_slider').owlCarousel({
        items: 1,
        nav: true,
        navText: ["", ""],
        navSpeed: 600,
        dragEndSpeed: 600
    });
    $('.review_cover_slider').owlCarousel({
        responsive: {
            0: {
                stagePadding: 0,
                items: 1
            },
            950: {
                stagePadding: 100,
                items: 2
            },
            1300: {
                stagePadding: 200,
                items: 3
            }
        },
        nav: true,
        navText: ["", ""],
        navSpeed: 600,
        dragEndSpeed: 600,
        onChanged: function (event) {
            if(event.item.index > 0){
                $('.review_cover_slider').find('.item.text').addClass('hide');
            }else{
                $('.review_cover_slider').find('.item.text').removeClass('hide');
            }
        }
    });
    if (!$(body).hasClass('mobile')) {
        detect_between();
        loading_section();
    }
    gallery_test();
});

$(document).on('click', 'header .menu', function () {
    if ($(this).hasClass('active')) {
        $(this).removeClass('active');
        $('.navigation').removeClass('active');
        setTimeout(function () {
            $('.navigation').removeClass('visible');
        }, 800);
        $(this).find('span').text('меню');
        $('header').removeClass('open_menu');
    } else {
        if (!$('.navigation').hasClass('visible')) {
            $(this).addClass('active');
            $('.navigation').addClass('visible');
            setTimeout(function () {
                $('.navigation').addClass('active');
            }, 0);
            $(this).find('span').text('закрыть');
            $('header').addClass('open_menu');
        }
    }
});

$(document).on('click', '.btn_requestModal', function () {
    $('#requestModal').arcticmodal();
});

$(document).on('click', '.btn_videoFrameModal', function () {
    $('#videoFrame').arcticmodal();
});

$(document).ready(function () {
    window.sr = ScrollReveal();
    sr.reveal('.amin', { 
        duration: 800,
        distance: '100px',
        scale: 1
    });
});


var gallery_counter = 0;

if($('.gallery_cover_owl').length){
    $('.gallery_cover_owl').owlCarousel({
        responsive : {
            0 : {
                mouseDrag: true
            },
            1000 : {
                mouseDrag: false
            }
        },
        items: 1,
        smartSpeed:700,
        onChanged: function (event) {
            gallery_counter = event.item.index;
        }
    });
}

function gallery_test(){
    var parent = $('.small_img_cont');
    var small_img = $('.small_img');
    var left_anim = 'left_anim';
    var top_anim = 'top_anim';
    var right_anim = 'right_anim';

    var width_img = parent.width();
    var height_img = parent.height();

    small_img.find('.img').width(width_img);
    small_img.find('.img').height(height_img);

    $('.small_img_cont:first-of-type').find('.small_img').css('left', -width_img);
    $('.small_img_cont:nth-child(2)').find('.small_img').css('top', -height_img*2);
    $('.small_img_cont:nth-child(3)').find('.small_img').css('top', -height_img*4);
    $('.small_img_cont:last-of-type').find('.small_img').css('right', -width_img*3);

    var quantity_item = $('.gallery_cover_owl ').find('.img').length;
    var quantity_width = quantity_item * width_img ;
    var pars_quantity_width =  parseInt(quantity_width, 10) + 'px';
    var quantity_height = quantity_item * height_img - height_img;

    $(document).on('click', '.nav_prev', function () {
        if(!$('.gallery_cover').hasClass('animating') && gallery_counter!=0){
            parent.each(function() {
                if ($(this).hasClass(left_anim)) {
                    var current_left = $('.left_anim').find('.small_img').css('left');
                    var new_left = parseInt(current_left, 10) + parseInt(width_img, 10) + 'px';

                    if(!(new_left >= 0)){
                        $('.left_anim').find('.small_img').css('left', new_left);
                    }
                }else if ($(this).hasClass(top_anim)){
                    $('.top_anim').each(function() {
                        var current_top = $(this).find('.small_img').css('top');
                        var new_top = parseInt(current_top, 10) + parseInt(height_img, 10) + 'px';

                        if(!(new_top >= 0)){
                            $(this).find('.small_img').css('top', new_top);
                        }
                    });
                }else if ($(this).hasClass(right_anim)){
                    var current_right = $('.right_anim').find('.small_img').css('right');
                    var new_right= parseInt(current_right, 10) + parseInt(width_img, 10) + 'px';

                    if(!(new_right >= 0)){
                        $('.right_anim').find('.small_img').css('right', new_right);
                    }
                }
            });
            $('.gallery_cover_owl').find('.owl-prev').click();
            $('.gallery_cover').addClass('animating');
            setTimeout(function () {
                $('.gallery_cover').removeClass('animating');
            }, 700);
        }
    });
    $(document).on('click', '.nav_next', function () {
        console.log('123');
        if(!$('.gallery_cover').hasClass('animating')){
            parent.each(function() {
                if ($(this).hasClass(left_anim)) {
                    var current_left = $('.left_anim').find('.small_img').css('left');

                    var new_left = parseInt(current_left, 10) - parseInt(width_img, 10) + 'px';

                    $('.left_anim').find('.small_img').css('left', new_left);
                }else if ($(this).hasClass(top_anim)){
                    $('.top_anim').each(function() {
                        var current_top = $(this).find('.small_img').css('top');

                        var new_top = parseInt(current_top, 10) - parseInt(height_img, 10) + 'px';
                        $(this).find('.small_img').css('top', new_top);
                    });
                }else if ($(this).hasClass(right_anim)){
                    var current_right = $('.right_anim').find('.small_img').css('right');

                    var new_right = parseInt(current_right, 10) - parseInt(width_img, 10) + 'px';
                    $('.right_anim').find('.small_img').css('right', new_right);
                }
            });
            $('.gallery_cover_owl').find('.owl-next').click();
            $('.gallery_cover').addClass('animating');
            setTimeout(function () {
                $('.gallery_cover').removeClass('animating');
            }, 700);
        }
    });
}

function loading_section() {
    var sections = $('body').find('section');
    var scrollTop = window.pageYOffset;
    $.each(sections, function () {
        console.log(scrollTop, $(this)["0"].offsetTop);
        if (scrollTop == $(this)["0"].offsetTop) {
            $('body').removeClass('double').data('slide', $(this).index() - 1);
        }
    });
}

function detect_between() {
    var scrollTop = window.pageYOffset;
    var wid_height = window.screen.height;
    var slide = $('body').data('slide');
    var cur_slide_scroll = $('body').find('section').eq(slide)["0"].offsetTop;

    if (!$('body').hasClass('animating')) {
        if (scrollTop != cur_slide_scroll) {
            $('body').addClass('double');
        } else {
            $('body').removeClass('double');
        }
    }

    if (scrollTop > 100) {
        $('header').addClass('sec_pos');
        $('.navigation').addClass('sec_pos');
    } else {
        $('header').removeClass('sec_pos');
        $('.navigation').removeClass('sec_pos');
    }
}

function animateGrid() {
    if (!$(body).hasClass('mobile')) {
        var sham_blocks = $('.products_grid').find('[data-sham]');
        $.each(sham_blocks, function () {
            var this_data = $(this).data('sham');
            var $this = $(this);
            setTimeout(function () {
                $this.addClass('animated fadeIn');
            }, 150 * this_data)
        });
    }
}

function animateSimple(this_block) {
    if (!$(body).hasClass('mobile')) {
        this_block.find('.animateSimpleFunc').addClass('animated fadeInUp');
    }
}

function animateSeoText() {
    if (!$(body).hasClass('mobile')) {
        setTimeout(function () {
            $('.seo_text_cover').find('.text_cover').addClass('animated fadeInUp');
        }, 200)
    }
}

function animatePresentation() {
    if (!$(body).hasClass('mobile')) {
        setTimeout(function () {
            $('.presentation_materials_cover').addClass('animated fadeInUp');
        }, 800);
    }
}

function animateMenu() {
    if (!$(body).hasClass('mobile')) {
        $('header').addClass('animated fadeInDown');
    }
}

function animateSlider() {

    var sliderItems = $('.stages_slider .item'),
        activeSliderItem = sliderItems.filter('.active'),
        nextSliderItem = activeSliderItem.next(),
        prevSliderItem = activeSliderItem.prev();
    var topItems = $('.stages_list .item'),
        activeTopItem = topItems.filter('.active'),
        nextTopItem = activeTopItem.next(),
        prevTopItem = activeTopItem.prev();
    var indexItem;

    function changeVar(index) {
        activeSliderItem = sliderItems.eq(index);
        nextSliderItem = activeSliderItem.next();
        prevSliderItem = activeSliderItem.prev();
        activeTopItem = topItems.eq(index);
        nextTopItem = activeTopItem.next();
        prevTopItem = activeTopItem.prev();
    }
    function doSlide() {
        $(sliderItems).removeClass('active');
        setTimeout(function () {
            $(sliderItems).css('display', 'none');
            $(activeSliderItem).css('display', 'block');
            var url = $(activeSliderItem).data('url');
            $('.stages_construction').css('background-image', 'url(' + url + ')');
            setTimeout(function () {
                $(activeSliderItem).addClass('active');
            }, 10);
            $(topItems).removeClass('active');
            $(activeTopItem).addClass('active');
        }, 500);
    }

    $(document).on('click', '.stages_list .item', function () {
        indexItem = $(this).index();
        changeVar(indexItem);
        doSlide();
    });

    $(document).on('click', '.stages_slider_nav .right', function () {
        if (nextSliderItem.length) {
            indexItem = nextSliderItem.index();
            changeVar(indexItem);
            doSlide();
        } else {
            return;
        }
    });
    $(document).on('click', '.stages_slider_nav .left', function () {
        if (prevSliderItem.length) {
            indexItem = prevSliderItem.index();
            changeVar(indexItem);
            doSlide();
        } else {
            return;
        }
    });

}
animateSlider();