setTitle = (title) => { document.title = title; };

particlesJS.load('particles-js', 'js/particles.json', function () {
    console.log('callback - particles.js config loaded');
});

$(function () {
    $('[data-toggle="tooltip"]').tooltip();
});