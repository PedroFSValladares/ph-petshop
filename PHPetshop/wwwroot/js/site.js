var bannerTop = $(".sobre")
var min = 300
var max = 800
var cards = Array.from(document.getElementsByClassName("card"))

function normalize(value) { return 1 - (value - min) / (max - min)} 

$(document).scroll((e) => { //330 a 1200
    bannerTop.css({ "opacity": normalize($(this).scrollTop()) })
    if ($(this).scrollTop() >= 800) {
        bannerTop.css({ "visibility": "hidden" })
    } else if ($(this).scrollTop() <= 1060) {
        bannerTop.css({ "visibility": "visible" })
    }
})

cards.forEach((element) => {
    $(element).on({
        "mouseenter": function (e) {
            /*
            $(element).removeClass("card-transition-reverse")
            $(element).addClass("card-transition")
            $(element.getElementsByTagName("h3")).removeClass("change-title-color-reverse")
            $(element.getElementsByTagName("h3")).addClass("change-title-color")
            */
            $(element.getElementsByTagName("h3")).addClass("card-title-animation")
            $(element).addClass("card-animation")
            setTimeout(() => {
                $(element).css("animation-play-state", "paused")
                $(element.getElementsByTagName("h3")).css("animation-play-state", "paused")
            }, parseFloat($(element).css("animation-duration")) / 2 * 1000)
        },
        "mouseleave": function (e) {
            /*
            $(element).removeClass("card-transition")
            $(element).addClass("card-transition-reverse")
            $(element.getElementsByTagName("h3")).removeClass("change-title-color")
            $(element.getElementsByTagName("h3")).addClass("change-title-color-reverse")
            */
            $(element).css("animation-play-state", "running")
            $(element.getElementsByTagName("h3")).css("animation-play-state", "running")
        }
    })
    element.addEventListener("animationend", (e) => {
        $(element).removeClass("card-animation")
        $(element.getElementsByTagName("h3")).removeClass("card-title-animation")
    })
})