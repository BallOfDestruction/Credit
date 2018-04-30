/**
 * Created by Damir on 17.03.2018.
 */
if (!$(body).hasClass('mobile')) {
    $(window).on('scroll touchmove mousewheel', function (e) {
        var slide = $('body').data('slide');
        // console.log(slide);
        if (slide >= 1) {
            setTimeout(function () {
                animateGrid();
            }, 300)
        }
        if (slide >= 2) {
            animateSimple($('.work_map'));
        }
        if (slide >= 3) {
            animateSimple($('.projects_slider'));
        }
        if (slide >= 4) {
            animateSeoText();
        }
    });
}