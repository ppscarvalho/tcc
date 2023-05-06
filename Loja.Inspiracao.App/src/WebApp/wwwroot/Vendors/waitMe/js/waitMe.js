/*
waitMe - 1.12 [12.05.15]
Author: vadimsva
Github: https://github.com/vadimsva/waitMe
*/
(function(b){b.fn.waitMe=function(n){return this.each(function(){var g=b(this),y,u,d,v=!1,r="background-color",p="",q="",w,e,c,f,x,l={init:function(){function z(a){t.css({top:"auto",transform:"translateY("+a+"px) translateZ(0)"})}f=b.extend({effect:"bounce",text:"",bg:"rgba(255,255,255,0.7)",color:"#000",sizeW:"",sizeH:"",source:""},n);x=(new Date).getMilliseconds();w=b('<div class="waitMe" data-waitme_id="'+x+'"></div>');var a="width:"+f.sizeW+";height:"+f.sizeH;switch(f.effect){case "none":d=0;
break;case "bounce":d=3;e="";c=a;break;case "rotateplane":d=1;e="";c=a;break;case "stretch":d=5;e="";c=a;break;case "orbit":d=2;e=a;c="";break;case "roundBounce":d=12;e=a;c="";break;case "win8":d=5;v=!0;c=e=a;break;case "win8_linear":d=5;v=!0;e=a;c="";break;case "ios":d=12;e=a;c="";break;case "facebook":d=3;e="";c=a;break;case "rotation":d=1;r="border-color";e="";c=a;break;case "timer":d=2;p="border-color:"+f.color;e=a;c="";break;case "pulse":d=1;r="border-color";e="";c=a;break;case "progressBar":d=
1;e="";c=a;break;case "bouncePulse":d=3;e="";c=a;break;case "img":d=1,e="",c=a}""===f.sizeW&&""===f.sizeH&&(e=c="");""!==e&&""!==p&&(p=";"+p);if(0<d){u=b('<div class="waitMe_progress '+f.effect+'"></div>');if("img"==f.effect)q='<img src="'+f.source+'" style="'+c+'">';else for(a=1;a<=d;++a)q=v?q+('<div class="waitMe_progress_elem'+a+'" style="'+c+'"><div style="'+r+":"+f.color+'"></div></div>'):q+('<div class="waitMe_progress_elem'+a+'" style="'+r+":"+f.color+";"+c+'"></div>');u=b('<div class="waitMe_progress '+
f.effect+'" style="'+e+p+'">'+q+"</div>")}f.text&&(y=b('<div class="waitMe_text" style="color:'+f.color+'">'+f.text+"</div>"));(a=g.find("> .waitMe"))&&a.remove();a=b('<div class="waitMe_content"></div>');a.append(u,y);w.append(a);"HTML"==g[0].tagName&&(g=b("body"));g.addClass("waitMe_container").attr("data-waitme_id",x).append(w);var a=g.find("> .waitMe"),t=g.find(".waitMe_content");a.css({background:f.bg});t.css({marginTop:-t.outerHeight()/2+"px"});if(g.outerHeight()>b(window).height()){var a=b(window).scrollTop(),
h=t.outerHeight(),m=g.offset().top,l=g.outerHeight(),k=a-m+b(window).height()/2;0>k&&(k=Math.abs(k));0<=k-h&&k+h<=l?m-a>b(window).height()/2&&(k=h):k=a>m+l-h?a-m-h:a-m+h;z(k);b(document).scroll(function(){var a=b(window).scrollTop()-m+b(window).height()/2;0<=a-h&&a+h<=l&&z(a)})}},hide:function(){var b=g.attr("data-waitme_id");g.removeClass("waitMe_container").removeAttr("data-waitme_id");g.find('.waitMe[data-waitme_id="'+b+'"]').remove()}};if(l[n])return l[n].apply(this,Array.prototype.slice.call(arguments,
1));if("object"===typeof n||!n)return l.init.apply(this,arguments);b.event.special.destroyed={remove:function(b){b.handler&&b.handler()}}})};b(window).load(function(){b("body.waitMe_body").addClass("hideMe");setTimeout(function(){b("body.waitMe_body").find(".waitMe_container:not([data-waitme_id])").remove();b("body.waitMe_body").removeClass("waitMe_body hideMe")},200)})})(jQuery);