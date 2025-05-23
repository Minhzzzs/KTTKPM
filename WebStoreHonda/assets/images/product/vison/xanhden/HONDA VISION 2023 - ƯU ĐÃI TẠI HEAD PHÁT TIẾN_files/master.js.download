jQuery(document).ready(function($){
    var iOS = /iPad|iPhone|iPod/.test(navigator.userAgent);
    if(iOS){
        $(document.body).addClass('ios');
    };
    //input date
    if($('.input-date').length){
        var pkcont = 'body';
        if($('.picker-container').length){
            pkcont = '.picker-container';
        }
        $('.input-date').datepicker({
            todayHighlight: true,
            startDate: "0d",
            container: pkcont
        });
    }
    
    //Show/Hide scroll-top on Scroll
    // hide #back-top first
	$("#scroll-top").hide();
    // fade in #back-top
    $(function () {
        $(window).scroll(function () {
            if ($(this).scrollTop() > 100) {
                $('#scroll-top').fadeIn();
            } else {
                $('#scroll-top').fadeOut();
            }
        });
        // scroll body to 0px on click
        $('#scroll-top').click(function () {
            $('body,html').animate({
                scrollTop: 0
            }, 1000);
        });
    });
    $('.navbar-toggle').on('click',function(e){
        $(this).toggleClass('open');
        $('body').toggleClass('menuin');
    });
    $('.nav-overlay').on('click',this,function(e){
        $('.navbar-toggle').trigger('click');
    });
     $('.dropdown').on('click', '.dropdown-toggle',function(e){
       
        var $this = $(this);
        var parent = $this.parent('.dropdown');
        var submenu = parent.find('.sub-menu-wrap');
        parent.toggleClass('open').siblings().removeClass('open');
        e.stopPropagation();
        
        submenu.click(function(e){
           e.stopPropagation();
        });
        
       
    });
    $('body,html').on('click', function(){
        
        if($('.dropdown').hasClass('open')){

            $('.dropdown').removeClass('open');
        }
    });
    $('.mega-bike-right').each(function(){
            var $this = $(this);
            var $img = $this.children('.img').children('img');
            $this.find('[data-pd-img]').on('click', function(evt){
                evt.preventDefault();
                var urlString = $(this).attr('data-pd-img');
                $img.attr('src', urlString);
                console.log($img);
                console.log(urlString);
                $(this).addClass('active').siblings().removeClass('active');
            });
        });
    // menu responsive js
    if($(window).width() < 991){
        $('#main-nav .dropdown').each(function(){
            var $submn = $(this).children('.sub-menu-wrap');
            var $toggleIcon = $('<i class="fa fa-angle-down bamvomosub"></i>')
            $(this).prepend($toggleIcon);
//            $submn.hide();
               console.log($submn);
//            $toggleIcon.on('click', function(e){
//                 e.preventDefault();
//                 $submn.toggle();
//            });
        });
        $(document).on('click','.dropdown .bamvomosub',function(e){
             e.preventDefault();
             console.log($(this).closest('.dropdown').find('.sub-menu-wrap'));
             $(this).closest('.dropdown').find('.sub-menu-wrap').stop().toggleClass('mobile-active');
        });
    } 
    // END menu JS
    
    $('.callpromote-popup ').on('click', function(e){
            e.preventDefault();
            $.magnificPopup.open({
                type: 'inline',
                items: {
                    src: $(this).attr('href'),  
                  },
                callbacks: {
                    open: function() {
                        var $popC = this.content;

                    },
                    close: function() {
                        var $popC = this.content;
                    }
                }
            });
        });
    $('[data-yt-id]').on('click', function(e){
             e.preventDefault();
            var ytId = $(this).attr('data-yt-id');
            $.magnificPopup.open({
                type: 'iframe',
                items: {
                    src: 'https://www.youtube.com/watch?v='+ytId,
                    
                  },
                callbacks: {
                    open: function() {
                        var $popC = this.content;

                    },
                    close: function() {
                        var $popC = this.content;
                    }
                }
            });
        });
        
        if ($('#slider-bikes').length){
            var rowInt = 2;
            if($(window).innerWidth() < 1025) rowInt = 1 ;
            $('#slider-bikes').slick({
                dots: false,
                arrows: true,
                infinite: true,
                speed: 500,
                rows: rowInt,
                slidesPerRow: 4,
                autoplay: false,
                autoplaySpeed: 2000,
                prevArrow: '<span class="slick-prev slick-arrow"><i class="fa fa-angle-left"></i></span>',
                nextArrow: '<span class="slick-next slick-arrow"><i class="fa fa-angle-right"></i></span>',
                responsive: [
                        {
                          breakpoint: 1025,
                          settings: {     
                             slidesToShow: 3,
                          }
                        },
                        {
                          breakpoint: 769,
                          settings: {
                                slidesToShow: 2,
                          }
                        },
                        {
                          breakpoint: 350,
                          settings: {
                                slidesToShow: 1,
                          }
                        }
                        // You can unslick at a given breakpoint now by adding:
                        // settings: "unslick"
                        // instead of a settings object
                      ]
            });
        }
        if ($('#slider-tinkhuyenmai').length){
            $('#slider-tinkhuyenmai').slick({
                dots: false,
                arrows: true,
                infinite: true,
                speed: 500,
                slidesToShow: 3,
                autoplay: false,
                autoplaySpeed: 2000,
                prevArrow: '<span class="slick-prev slick-arrow"><i class="fa fa-angle-left"></i></span>',
                nextArrow: '<span class="slick-next slick-arrow"><i class="fa fa-angle-right"></i></span>',
                responsive: [
                        {
                          breakpoint: 991,
                          settings: {
                                slidesToShow: 3,
                          }
                        },
                        {
                          breakpoint: 769,
                          settings: {
                                slidesToShow: 1,
                          }
                        }
                        // You can unslick at a given breakpoint now by adding:
                        // settings: "unslick"
                        // instead of a settings object
                      ]
            });
        }
        //END home js
        $('.sec-bike-list').on('click', '.btn-change-view',function(e){
            e.preventDefault();
            var $this = $(this);
            var $bikeList = $this.closest('.sec-bike-list').find('.bike-list-ul');
            
            
            $this.addClass('active').siblings('.btn-change-view').removeClass('active');
            
            if($this.hasClass('grid-view')){
                $bikeList.removeClass('listview');
            } else if($this.hasClass('list-view')){
                $bikeList.addClass('listview');
            } else {
                alert('undefine view type')    
            }
        });
    
//    $.magnificPopup.open({
//                 items: {
//                    src: '#addfilePop', // can be a HTML string, jQuery object, or CSS selector
//                  },
//                mainClass: 'file-popup',
//                prependTo: '.files-wrap',
//                callbacks: {
//                    open: function() {
//                        var $popC = this.content;
//
//                    },
//                    close: function() {
//                        var $popC = this.content;
//                    }
//                }
//            });
    
    $('.tab-nav-ul').on('click', 'a', function(e){
        e.preventDefault();
        var $this = $(this);
        var $li = $this.parent();
        var megaWrap = $li.closest('.tab-cont-nav').parent();
        megaWrap.find('.tab-cont'+$this.attr('href')).addClass('active').siblings('.tab-cont').removeClass('active');
        $li.addClass('active').siblings().removeClass('active');
    });
    
    $('.collapse').on('click','.collapse-heading',function(){
        var container = $(this).parent('.collapse');
        $(container).siblings().removeClass('on').find('.collapse-body').slideUp();
        $(container).find('.collapse-body').is(':visible')  ?  
        $(container).removeClass('on').find('.collapse-body').slideUp() :
        $(container).addClass('on').find(':hidden').slideDown() ;
        
    });
    stickyHeader();
//    $(window).scrollTop() > $("#header").height() ? $("#header").addClass("sticky") : $("#header").removeClass("sticky");
    $(window).scroll(function () {
//        $(window).scrollTop() > $("#header").height() ? $("#header").addClass("sticky") : $("#header").removeClass("sticky");
        stickyHeader();
    });
    function stickyHeader(){
        var hdOffsetTop =  $("#header").offset().top;
        if($(window).scrollTop() > $("#header").height()){
            $("#header").addClass("sticky");
        } else {
            $("#header").removeClass("sticky");
        }
    }
    
    if ($('#slider-top').length){
        $('#slider-top').slick({
            dots: true,
            arrows:true,
            infinite: true,
            speed: 500,
            slidesToShow: 1,
            slidesToScroll: 1,
            autoplay: false,
            autoplaySpeed: 5000,
            prevArrow: '<span class="slick-prev slick-arrow"><i class="fa fa-angle-left"></i></span>',
            nextArrow: '<span class="slick-next slick-arrow"><i class="fa fa-angle-right"></i></span>',
            responsive: [
                    {
                      breakpoint: 480,
                      settings: {
                        arrows:false,
                      }
                    }
                    // You can unslick at a given breakpoint now by adding:
                    // settings: "unslick"
                    // instead of a settings object
                  ]
        });
    }

    var megaDropdown = document.querySelectorAll('.mega-bike-dropdown .sub-menu');
    megaDropdown.forEach(function(e){
        e.children[0].classList.add('hover');
        [...e.children].forEach(function(el){
            el.addEventListener('mouseenter', () => {
                el.classList.add('hover');
            });
            el.addEventListener('mouseleave', () => {
                el.classList.remove('hover');
            });
        });
        e.addEventListener('mouseleave', function(){
            e.children[0].classList.add('hover');
        });
    });

    var categoryNavItem = document.querySelectorAll('.category-nav-ul li');
    var categoryContainer = document.querySelectorAll('.slider-bikes');
    categoryNavItem.forEach(function(e){
        if(e.classList.contains('hl-item')){
            let href = e.children[0].getAttribute('href').replace('#', '');
            categoryContainer.forEach(function(ell){
               
                if(ell.getAttribute('id') == href){
                    ell.classList.add('show');
                }
            });
        }
        e.addEventListener('click', function(el){
            let href = e.children[0].getAttribute('href');
            if(href.startsWith('#')){
                href = href.replace('#', '');
                el.preventDefault();
                categoryNavItem.forEach(function(ell){
                    ell.classList.remove('hl-item');
                })
                e.classList.add('hl-item');
                categoryContainer.forEach(function(ell){
                    ell.classList.remove('show');
                    if(ell.getAttribute('id') == href){
                         
                        ell.classList.add('show');
                    }
                });
            }
        })
    });

    
    // Copyright Manh Nguyen
    
    $('.mega-bike-right').on('click', '.dl-bike-info a', function(e){
        e.preventDefault();
        
        let $colorActive, $linkBtn, $link;
        $colorActive = $(this).attr('data-cl');
        $linkBtn = $(this).parents('.mega-bike-right').find('.this-link');
        $link = $(this).parents('.mega-bike-right').find('.img').attr('data-href');
        console.log($(this).parents('.mega-bike-right').find('.img'));
        $linkBtn.attr('href', $link.concat(`?color=${$colorActive}`));
    });

    if($('.product-info-wrap').length > 0){
        let $pdName, $currentColor, $currentColorName, $colorName, $img, $reelImages, $reelImageString;
        $pdName = $('.pd-name').text();
        $currentColor = window.location.href.split('?')[1];
        if($currentColor){
            $(`.pd-color-picker [data-cl='${$currentColor}']`).addClass('active').siblings().removeClass('active');
            $currentColorName = $(`.pd-color-picker [data-cl='${$currentColor}']`).next('.cl-name').text();
        }
        else{
            $currentColorName = $(`.pd-color-picker .cl-picker.active`).next('.cl-name').text();
        }
       // $('.pd-name').text($pdName.concat(` - ${$currentColorName}`));
        $reelImages = $(`.pd-color-picker .cl-picker.active`).attr('data-imgs');
        if($reelImages.split('|')[1] != ''){
            $reelImageString = $reelImages.split('|')[1].split('..')[0];
            $reelImage = $reelImages.split('|')[0].replace(/####/g, $reelImageString);
            $('#image360').reel({images: $reelImages});
        } else {
            $('#image360').attr('src', $reelImages.split('|')[0]);
        }

        localStorage.setItem("bikeColor", $pdName.concat(` - ${$currentColorName}`));
        $('.pd-color-picker').on('click', '.cl-picker', function(){
            $color = $(this).attr('data-cl');
            $colorName = $(this).next('.cl-name').text();
           // $('.pd-name').text($pdName.concat(` - ${$colorName}`));
            localStorage.setItem("bikeColor", $pdName.concat(` - ${$colorName}`));
        });
    }
//    if($('.mona-order-form').find('.cl-bike').length > 0 && localStorage.bikeColor){
//        $('.mona-order-form').find('.cl-bike').text(localStorage.bikeColor)
//    }
});