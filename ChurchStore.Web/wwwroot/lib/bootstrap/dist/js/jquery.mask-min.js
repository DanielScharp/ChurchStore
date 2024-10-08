﻿// jQuery Mask Plugin v1.10.3
// github.com/igorescobar/jQuery-Mask-Plugin
//https://igorescobar.github.io/jQuery-Mask-Plugin/

(function (c) { "function" === typeof define && define.amd ? define(["jquery"], c) : c(window.jQuery || window.Zepto) })(function (c) {
    var m = function (a, d, e) {
        a = c(a); var g = this, p = a.val(), m; d = "function" === typeof d ? d(a.val(), void 0, a, e) : d; var b = {
            invalid: [], getCaret: function () { try { var h, q = 0, b = a.get(0), f = document.selection, d = b.selectionStart; if (f && !~navigator.appVersion.indexOf("MSIE 10")) h = f.createRange(), h.moveStart("character", a.is("input") ? -a.val().length : -a.text().length), q = h.text.length; else if (d || "0" === d) q = d; return q } catch (c) { } },
            setCaret: function (h) { try { if (a.is(":focus")) { var q, b = a.get(0); b.setSelectionRange ? b.setSelectionRange(h, h) : b.createTextRange && (q = b.createTextRange(), q.collapse(!0), q.moveEnd("character", h), q.moveStart("character", h), q.select()) } } catch (f) { } }, events: function () {
                a.on("keyup.mask", b.behaviour).on("paste.mask drop.mask", function () { setTimeout(function () { a.keydown().keyup() }, 100) }).on("change.mask", function () { a.data("changed", !0) }).on("blur.mask", function () {
                    p === a.val() || a.data("changed") || a.trigger("change");
                    a.data("changed", !1)
                }).on("keydown.mask, blur.mask", function () { p = a.val() }).on("focusout.mask", function () { e.clearIfNotMatch && !m.test(b.val()) && b.val("") })
            }, getRegexMask: function () {
                for (var h = [], a, b, f, c, e = 0; e < d.length; e++) (a = g.translation[d[e]]) ? (b = a.pattern.toString().replace(/.{1}$|^.{1}/g, ""), f = a.optional, (a = a.recursive) ? (h.push(d[e]), c = { digit: d[e], pattern: b }) : h.push(f || a ? b + "?" : b)) : h.push(d[e].replace(/[-\/\\^$*+?.()|[\]{}]/g, "\\$&")); h = h.join(""); c && (h = h.replace(RegExp("(" + c.digit + "(.*" + c.digit +
                    ")?)"), "($1)?").replace(RegExp(c.digit, "g"), c.pattern)); return RegExp(h)
            }, destroyEvents: function () { a.off("keydown keyup paste drop blur focusout ".split(" ").join(".mask ")) }, val: function (h) { var b = a.is("input") ? "val" : "text"; 0 < arguments.length ? (a[b](h), b = a) : b = a[b](); return b }, getMCharsBeforeCount: function (b, a) { for (var c = 0, f = 0, e = d.length; f < e && f < b; f++) g.translation[d.charAt(f)] || (b = a ? b + 1 : b, c++); return c }, caretPos: function (a, c, e, f) {
                return g.translation[d.charAt(Math.min(a - 1, d.length - 1))] ? Math.min(a + e -
                    c - f, e) : b.caretPos(a + 1, c, e, f)
            }, behaviour: function (a) { a = a || window.event; b.invalid = []; var e = a.keyCode || a.which; if (-1 === c.inArray(e, g.byPassKeys)) { var d = b.getCaret(), f = b.val(), n = f.length, k = d < n, r = b.getMasked(), l = r.length, p = b.getMCharsBeforeCount(l - 1) - b.getMCharsBeforeCount(n - 1); r !== f && b.val(r); !k || 65 === e && a.ctrlKey || (8 !== e && 46 !== e && (d = b.caretPos(d, n, l, p)), b.setCaret(d)); return b.callbacks(a) } }, getMasked: function (a) {
                var c = [], p = b.val(), f = 0, n = d.length, k = 0, r = p.length, l = 1, m = "push", u = -1, t, w; e.reverse ? (m = "unshift",
                    l = -1, t = 0, f = n - 1, k = r - 1, w = function () { return -1 < f && -1 < k }) : (t = n - 1, w = function () { return f < n && k < r }); for (; w();) { var x = d.charAt(f), v = p.charAt(k), s = g.translation[x]; if (s) v.match(s.pattern) ? (c[m](v), s.recursive && (-1 === u ? u = f : f === t && (f = u - l), t === u && (f -= l)), f += l) : s.optional ? (f += l, k -= l) : s.fallback ? (c[m](s.fallback), f += l, k -= l) : b.invalid.push({ p: k, v: v, e: s.pattern }), k += l; else { if (!a) c[m](x); v === x && (k += l); f += l } } a = d.charAt(t); n !== r + 1 || g.translation[a] || c.push(a); return c.join("")
            }, callbacks: function (c) {
                var g = b.val(), m = g !==
                    p, f = [g, c, a, e], n = function (a, b, c) { "function" === typeof e[a] && b && e[a].apply(this, c) }; n("onChange", !0 === m, f); n("onKeyPress", !0 === m, f); n("onComplete", g.length === d.length, f); n("onInvalid", 0 < b.invalid.length, [g, c, a, b.invalid, e])
            }
        }; g.mask = d; g.options = e; g.remove = function () { var c = b.getCaret(); b.destroyEvents(); b.val(g.getCleanVal()); b.setCaret(c - b.getMCharsBeforeCount(c)); return a }; g.getCleanVal = function () { return b.getMasked(!0) }; g.init = function (d) {
            d = d || !1; e = e || {}; g.byPassKeys = c.jMaskGlobals.byPassKeys; g.translation =
                c.jMaskGlobals.translation; g.translation = c.extend({}, g.translation, e.translation); g = c.extend(!0, {}, g, e); m = b.getRegexMask(); !1 === d ? (e.placeholder && a.attr("placeholder", e.placeholder), a.attr("autocomplete", "off"), b.destroyEvents(), b.events(), d = b.getCaret(), b.val(b.getMasked()), b.setCaret(d + b.getMCharsBeforeCount(d, !0))) : (b.events(), b.val(b.getMasked()))
        }; g.init(!a.is("input"))
    }; c.maskWatchers = {}; var z = function () {
        var a = c(this), d = {}, e = a.attr("data-mask"); a.attr("data-mask-reverse") && (d.reverse = !0); a.attr("data-mask-clearifnotmatch") &&
            (d.clearIfNotMatch = !0); if (y(a, e, d)) return a.data("mask", new m(this, e, d))
    }, y = function (a, d, e) { e = e || {}; a = c(a).data("mask"); var g = JSON.stringify; try { return "object" !== typeof a || g(a.options) !== g(e) || a.mask !== d } catch (m) { } }; c.fn.mask = function (a, d) {
        d = d || {}; var e = this.selector, g = c.jMaskGlobals, p = function () { if (y(this, a, d)) return c(this).data("mask", new m(this, a, d)) }; c(this).each(p); g.watchInputs && e && "" !== e && !c.maskWatchers[e] && (c.maskWatchers[e] = setInterval(function () { c(document).find(e).each(p) }, 300)); g.dataMask &&
            c("*[data-mask]").each(z); g.watchDataMask && setInterval(function () { c(document).find(g.nonInput).filter("*[data-mask]").each(z) }, 300)
    }; c.fn.unmask = function () { clearInterval(c.maskWatchers[this.selector]); delete c.maskWatchers[this.selector]; return this.each(function () { c(this).data("mask") && c(this).data("mask").remove().removeData("mask") }) }; c.fn.cleanVal = function () { return this.data("mask").getCleanVal() }; c.jMaskGlobals = {
        nonInput: "td,span,div", dataMask: !0, watchInputs: !0, watchDataMask: !1, byPassKeys: [9,
            16, 17, 18, 36, 37, 38, 39, 40, 91], translation: { 0: { pattern: /\d/ }, 9: { pattern: /\d/, optional: !0 }, "#": { pattern: /\d/, recursive: !0 }, A: { pattern: /[a-zA-Z0-9]/ }, S: { pattern: /[a-zA-Z]/ } }
    }
});