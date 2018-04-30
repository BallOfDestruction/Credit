$(document).ready(function () {
    var block = document.getElementById("chatContent");
    console.log(block);
    if (block !== null && block !== undefined) {
        block.scrollTop = block.scrollHeight;
    }

    $('.main_owl').owlCarousel({
        items: 1,
        nav: true,
        navText: ['','']
    });
    setTimeout(function() {
        $('input').styler();
    }, 100);

    $('.item_image_line_gallery').owlCarousel({
        items: 4,
        nav: true,
        navText: ['',''],
        onInitialized: function () {
            $('.item_image_line_gallery .owl-item.active:first-of-type').addClass('showed');
        }
    });

    $(document).on('click','.item_image_line_gallery .owl-item',function () {
        $('.item_image_line_gallery .owl-item').removeClass('showed');
        $(this).addClass('showed');
        var src = $(this).find('img').attr('src');
        $(this).closest('.item_image_gallery_cover').find('.item_image_picture img').attr('src', src);
    });


    $('.related_products_owl .catalog').owlCarousel({
        responsive : {
            0: {
                items: 1,
                autoHeight: true
            },
            700 : {
                items: 2
            },
            900 : {
                items: 4
            }
        },
        nav: true,
        navText: ['','']
    });

    $('.gallery_cover .gallery_line').owlCarousel({
        responsive : {
            0: {
                items: 2
            },
            900 : {
                items: 4
            }
        },
        nav: true,
        navText: ['',''],
        onInitialized: function () {
            $('.gallery_cover .gallery_line .owl-item.active:first-of-type').addClass('showed');
        }
    });

    $(document).on('click','.gallery_cover .gallery_line .owl-item',function () {
        $('.gallery_cover .gallery_line .owl-item').removeClass('showed');
        $(this).addClass('showed');
        var src = $(this).find('img').attr('src');
        $(this).closest('.gallery_cover').find('.gallery_image img').attr('src', src);
    });

    var slider = document.getElementById('slider_filter');
    if(slider != null){
        var inputStart = document.getElementById('slider_filter_start');
        var inputEnd = document.getElementById('slider_filter_end');

        noUiSlider.create(slider, {
            start: [20, 80], //начальное и конечное значение
            connect: true,
            range: {
                'min': 0, //минимальное
                'max': 100 //максимальное
            },
            format: {
                to: function ( value ) {
                    return Math.floor(value);
                },
                from: function ( value ) {
                    return Math.floor(value);
                }
            }
        });

        slider.noUiSlider.on('update', function( values, handle ) {
            //обновление input при изменении слайдера
            inputStart.value = values[0];
            inputEnd.value = values[1];
        });

        inputStart.addEventListener('change', function(){
            slider.noUiSlider.set([this.value]);
        });

        inputEnd.addEventListener('change', function(){
            slider.noUiSlider.set([null,this.value]);
        });
    }
});

$(document).on('click','.person_cover .burger',function () {
    $('.navigation_cover').css('left','0px');
});
$(document).on('click','.navigation_cover .close_modal',function () {
    $('.navigation_cover').css('left','100%');
});

$(document).on('click','.navigation_cover .arrow_down',function () {
    $(this).parent().find('ul').first().slideToggle();
});

$(document).on('click','.change_pass',function () {
    $('#changePassModal').arcticmodal();
});