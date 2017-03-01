(function( $ ) {
	$.fn.cp_Accordion = function(options){
		// Create some defaults, extending them with any options that were provided
		var settings = $.extend( {
		collapsable : true,
		active : 0,
		event: "click",
		expandedSpanText : '',
		collapsedSpanText : '',
		expandCollapsePosition : 'left',
		expandedHeaderText : '',
		collapsedHeaderText : ''
		}, options);
		
		var collapsable = settings.collapsable;
		var active = settings.active;
		var headerClass = settings.headerClass;
		var contentClass = settings.contentClass;
		var event = settings.event;
		var expandedSpanText = settings.expandedSpanText;
		var collapsedSpanText = settings.collapsedSpanText;
		var expandCollapsePosition = settings.expandCollapsePosition;
		var expandedHeaderText = settings.expandedHeaderText;
		var collapsedHeaderText = settings.collapsedHeaderText;
		
		headerClass = "." + headerClass;
		contentClass = "." + contentClass;
		
		

		if (collapsable) {
		$(headerClass).bind(event, function() {
		$(headerClass).removeClass('on');
	 	$(contentClass).slideUp('normal');
		$(headerClass).children('.expanded').removeClass('expanded').addClass('collapsed');
		if($(this).next().is(':hidden') == true) {
			$(this).addClass('on');
			$(this).next().slideDown('normal');
			$(this).children('.collapsed').removeClass('collapsed').addClass('expanded');
		 } 
		if (expandedSpanText != '') {
			$(headerClass).children('.expanded').html(expandedSpanText);
		}
		if (collapsedSpanText != '') {
			$(headerClass).children('.collapsed').html(collapsedSpanText);
		}
		if (expandedHeaderText != '') {
			$(headerClass).children('.expanded').siblings('.cpsty_header').html(expandedHeaderText);
		}
		if (collapsedHeaderText != '') {
			$(headerClass).children('.collapsed').siblings('.cpsty_header').html(collapsedHeaderText);
		}
		});
		} else {
			$(headerClass).each(function(index, value) {
				$(headerClass).bind(event, function() {
					if ($(this).siblings(contentClass).is(':hidden') == true && $(this).next(contentClass).is(':hidden') == true) {
					$(headerClass).removeClass('on');
					$(contentClass).slideUp('normal');
					$(headerClass).children('.expanded').removeClass('expanded').addClass('collapsed');
					if($(this).next().is(':hidden') == true) {
					$(this).addClass('on');
					$(this).next().slideDown('normal');
					$(this).children('.collapsed').removeClass('collapsed').addClass('expanded');
					} 
					if (expandedSpanText != '') {
						$(headerClass).children('.expanded').html(expandedSpanText);
					}
					if (collapsedSpanText != '') {
						$(headerClass).children('.collapsed').html(collapsedSpanText);
					}
					if (expandedHeaderText != '') {
						$(headerClass).children('.expanded').siblings('.cpsty_header').html(expandedHeaderText);
					}
					if (collapsedHeaderText != '') {
						$(headerClass).children('.collapsed').siblings('.cpsty_header').html(collapsedHeaderText);
					}
				}
				});
			});
		}
		
		
		
	$(headerClass).mouseover(function() {
		$(this).addClass('over');
		
	}).mouseout(function() {
		$(this).removeClass('over');										
	});
	
	$(contentClass).each(function(index, value) {
		if (active != index) {
		$(value).hide();
		CreateCollapsedSpan($(value).prev(headerClass));
		} else {
		CreateExpandedSpan($(value).prev(headerClass));
		}
		
	});
	
	return this;
	
		function CreateCollapsedSpan(selectorName) {
		$(selectorName).html( function(index, oldhtml) { 
				var strOldHtml = oldhtml;
				if (collapsedHeaderText != '') {
					strOldHtml = collapsedHeaderText;
				}
				
				if (expandCollapsePosition == 'right') {
					return '<span class="cpsty_header">' + strOldHtml + '</span>' + '<span class="collapsed">' + collapsedSpanText + '&nbsp;</span>' //added space to fix IE 7 bug
				} else {
					return '<span class="collapsed">' + collapsedSpanText + '&nbsp;</span>' + '<span class="cpsty_header">' + strOldHtml + '</span>'; //added space to fix IE 7 bug
				}
			});
		};
	
		function CreateExpandedSpan(selectorName) {
		$(selectorName).html( function(index, oldhtml) { 
				var strOldHtml = oldhtml;
				if (expandedHeaderText != '') {
					strOldHtml = expandedHeaderText;
				}

			if (expandCollapsePosition == 'right') {	
				return '<span class="cpsty_header">' + strOldHtml + '</span>' + '<span class="expanded">' + expandedSpanText + '&nbsp;</span>'; //added space to fix IE 7 bug
			} else {
				return '<span class="expanded">' + expandedSpanText + '&nbsp;</span>' + '<span class="cpsty_header">' + strOldHtml + '</span>'; //added space to fix IE 7 bug
			}
			});
		};
	
	};
})( jQuery );