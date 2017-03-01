(function ($) {
    $.fn.extend({
        cp_paginator: function (options) {
            var settings = $.extend({
                totalrecords: 0,
                recordsperpage: 0,
                length: 10,
                next: 'Next',
                prev: 'Prev',
                first: 'First',
                last: 'Last',
                go: 'Go',
                theme: '',
                display: 'double',
                initval: 1,
                datacontainer: '', //data container id
                dataelement: '', //children elements to be filtered e.g. tr or div
                onchange: null,
                controlsalways: false,
                clickevents: true,
				addHasTags: false
            }, options);
            return this.each(function () {
                var currentPage = 0;
                
                var startPage = 0;
                var totalpages = parseInt(settings.totalrecords / settings.recordsperpage);
                if (settings.totalrecords % settings.recordsperpage > 0) totalpages++;
                var initialized = false;
                var container = $(this).addClass('pager').addClass(settings.theme);
                container.find('ul').remove();
                container.find('div').remove();
                container.find('span').remove();
                var dataContainer;
                var dataElements;
                var clickevents = options.clickevents;
                var addHashTags = options.addHasTags;
                if (settings.datacontainer != '') {
                    dataContainer = $(settings.datacontainer);
                    //dataElements = $('' + settings.dataelement + '', dataContainer);
					dataElements = $(dataContainer).children(settings.dataelement);
                }
                var list = $('<ul/>');
                //var btnPrev = $('<div/>').text(settings.prev).click(function () { if ($(this).hasClass('disabled')) return false; currentPage = parseInt(list.find('li a.active').text()) - 1; navigate(--currentPage); }).addClass('btn');
                //var btnNext = $('<div/>').text(settings.next).click(function () { if ($(this).hasClass('disabled')) return false; currentPage = parseInt(list.find('li a.active').text()); navigate(currentPage); }).addClass('btn');
                
					var btnPrev = $('<div/>').text(settings.prev).addClass('cpSys_btn').addClass('cpSys_prev');;
					var btnNext = $('<div/>').text(settings.next).addClass('cpSys_btn').addClass('cpSys_next');;
					if (clickevents) {
					btnPrev.click(function () {
						if ($(this).hasClass('disabled')) return false; 
						currentPage = parseInt(list.find('li a.cpSys_active').text()) - 1; navigate(--currentPage); 
						if (addHashTags)
						{
							window.location.hash = 'Page' + (currentPage + 1);
						}
						});
						
					btnNext.click(function () { 
						if ($(this).hasClass('disabled')) return false; 
						currentPage = parseInt(list.find('li a.cpSys_active').text()); navigate(currentPage);
						if (addHashTags) {
							window.location.hash = 'Page' + (currentPage + 1);
						}
						});
						
					}
				
				container.append(btnPrev).append(list).append(btnNext).append($('<div/>').addClass('short'));
				buildNavigation(startPage);
                if (settings.initval == 0) settings.initval = 1;
                currentPage = settings.initval - 1;
                if (addHashTags) {
                	window.addEventListener("hashchange", findCurrentPageAndNavigate, false);                	
                	var currentPage = 0;
                	if (window.location.hash) {
                		if (window.location.hash.indexOf('#Page') > -1) {
                			currentPage = parseInt(window.location.hash.replace('#Page', '')) - 1;
                		}
                	} else {
                		currentPage = 0;
                	}
                }
                navigate(currentPage);
                initialized = true;
                function showLabels(pageIndex) {
                    container.find('span').remove();
                    var upper = (pageIndex + 1) * settings.recordsperpage;
                    if (upper > settings.totalrecords) upper = settings.totalrecords;
                    container.append($('<span class="cpsys_Pager_PageValue1"/>').append($('<b/>').text(pageIndex * settings.recordsperpage + 1)))
                                             .append($('<span class="cpsys_Page_Dash"/>').text('-'))
                                             .append($('<span class="cpsys_Pager_PageValue2"/>').append($('<b/>').text(upper)))
                                             .append($('<span class="cpsys_Pager_Of"/>').text('of'))
                                             .append($('<span class="cpsys_Pager_TotalRecords"/>').append($('<b/>').text(settings.totalrecords)));
                }
                function buildNavigation(startPage) {
                    list.find('li').remove();
                    if (settings.totalrecords <= settings.recordsperpage) return;
                    for (var i = startPage; i < startPage + settings.length; i++) {
                        if (i == totalpages) break;
                        if (addHashTags) {
                        	if (clickevents) {
                        		list.append($('<li/>')
											.append($('<a>').attr('data-page', (i + 1)).addClass(settings.theme).addClass('cpSys_normal')
											.attr('href', '#Page' + (i +1))
											.text(i + 1))
											.click(function () {
												currentPage = startPage + $(this).closest('li').prevAll().length;
												navigate(currentPage);
											}));
                        	} else {
                        		list.append($('<li/>')
											.append($('<a>').attr('data-page', (i + 1)).addClass(settings.theme).addClass('cpSys_normal')
											.attr('href', '#Page' + (i + 1))
											.text(i + 1))
											);
                        	}
                        } else {
                        	if (clickevents) {
                        		list.append($('<li/>')
											.append($('<a>').attr('data-page', (i + 1)).addClass(settings.theme).addClass('cpSys_normal')
											.attr('href', 'javascript:void(0)')
											.text(i + 1))
											.click(function () {
												currentPage = startPage + $(this).closest('li').prevAll().length;
												navigate(currentPage);
											}));
                        	} else {
                        		list.append($('<li/>')
											.append($('<a>').attr('data-page', (i + 1)).addClass(settings.theme).addClass('cpSys_normal')
											.attr('href', 'javascript:void(0)')
											.text(i + 1))
											);
                        	}
                        }
                        
						
						
						
						
						//if (clickevents) {
						//	list.find('a').each(function(index, value) {
						//		$(this).click(function () {
						//			currentPage = startPage + $(this).closest('li').prevAll().length;
                        //            navigate(currentPage);
                         //       });
						//	});
						//}
					}
                    showLabels(startPage);
                    list.find('li a').addClass(settings.theme).removeClass('cpSys_active');
                    list.find('li:eq(0) a').addClass(settings.theme).addClass('cpSys_active');
					
					if ($.browser.msie && $.browser.version == '7.0') {
						list.find('li').addClass(settings.theme).removeClass('cpSys_active');
						list.find('li:eq(0)').addClass(settings.theme).addClass('cpSys_active');
					}
                    //set width of paginator
					var width = list.find('li:first').width() * list.find('li').length + (parseInt(list.find('li:eq(0)').css('margin-left')) * list.find('li').length);
                    if ($.browser.version != '7.0') {
						list.css({ width: width });
					}
					showRequiredButtons(startPage);
                }
                function navigate(topage) {
                    //make sure the page in between min and max page count
                    var index = topage;
                    var mid = settings.length / 2;
                    if (settings.length % 2 > 0) mid = (settings.length + 1) / 2;
                    var startIndex = 0;
                    if (topage >= 0 && topage < totalpages) {
                        if (topage >= mid) {
                            if (totalpages - topage > mid)
                                startIndex = topage - (mid - 1);
                            else if (totalpages > settings.length)
                                startIndex = totalpages - settings.length;
                        }
                        buildNavigation(startIndex); showLabels(currentPage);
                        list.find('li a').removeClass('cpSys_active');
                        list.find('li a[data-page="' + (index + 1) + '"]').addClass('cpSys_active');
						if ($.browser.msie && $.browser.version == '7.0') {
							list.find('li').removeClass('cpSys_active');
							list.find('li a[data-page="' + (index + 1) + '"]').parent().addClass('cpSys_active');
						}
                        var recordStartIndex = currentPage * settings.recordsperpage;
                        var recordsEndIndex = recordStartIndex + settings.recordsperpage;
                        if (recordsEndIndex > settings.totalrecords)
                            recordsEndIndex = settings.totalrecords % recordsEndIndex;
                        if (initialized) {
                            if (settings.onchange != null) {
                                settings.onchange((currentPage + 1), recordStartIndex, recordsEndIndex);
                            }
                        }
                        if (dataContainer != null) {
                            if (dataContainer.length > 0) {
                                //hide all elements first
                                dataElements.css('display', 'none');
                                //display elements that need to be displayed
                                if ($(dataElements[0]).find('th').length > 0) { //if there is a header, keep it visible always
                                    $(dataElements[0]).css('display', '');
                                    recordStartIndex++;
                                    recordsEndIndex++;
                                }
                                for (var i = recordStartIndex; i < recordsEndIndex; i++)
                                    $(dataElements[i]).css('display', '');
                            }
                        }

                        showRequiredButtons();
                    }
                }
                function showRequiredButtons() {
                        if (currentPage > 0) {
                            if (!settings.controlsalways) {
                                btnPrev.css('display', '');
                            }
                            else {
                                btnPrev.css('display', '').removeClass('disabled');
                            }
                        }
                        else {
                            if (!settings.controlsalways) {
                                btnPrev.css('display', 'none');
                            }
                            else {
                                btnPrev.css('display', '').addClass('disabled');
                            }
                        }

                        if (currentPage == totalpages - 1) {
                            if (!settings.controlsalways) {
                                btnNext.css('display', 'none');
                            }
                            else {
                                btnNext.css('display', '').addClass('disabled');
                            }
                        }
                        else {
                            if (!settings.controlsalways) {
                                btnNext.css('display', '');
                            }
                            else {
                                btnNext.css('display', '').removeClass('disabled');
                            }
                        }
                }
                function isTextSelected(el) {
                    var startPos = el.get(0).selectionStart;
                    var endPos = el.get(0).selectionEnd;
                    var doc = document.selection;
                    if (doc && doc.createRange().text.length != 0) {
                        return true;
                    } else if (!doc && el.val().substring(startPos, endPos).length != 0) {
                        return true;
                    }
                    return false;
                }
                function findCurrentPageAndNavigate() {
                	currentPage = 0;
                	if (window.location.hash) {
                		if (window.location.hash.indexOf('#Page') > -1) {
                			currentPage = parseInt(window.location.hash.replace('#Page', '')) - 1;
                		}
                	} else {
                		currentPage = 0;
                	}
                	navigate(currentPage);
                }
            });
        }
    });
})(jQuery);