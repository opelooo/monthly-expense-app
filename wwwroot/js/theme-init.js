// Theme flash prevention - must run before page render
(function () {
    const savedTheme = localStorage.getItem('theme');
    const defaultTheme = 'dark';
    const theme = savedTheme || defaultTheme;
    document.documentElement.setAttribute('data-theme', theme);
})();
