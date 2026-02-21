// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// Theme Toggle Functionality
(function () {

    const root = document.documentElement;
    let themeIcon = null;

    const defaultTheme = 'dark';

    function getSavedTheme() {
        const saved = localStorage.getItem('theme');
        if (saved) return saved;

        if (window.matchMedia?.('(prefers-color-scheme: dark)').matches)
            return 'dark';

        return defaultTheme;
    }

    function applyTheme(theme) {

        root.setAttribute('data-theme', theme);
        localStorage.setItem('theme', theme);

        if (!themeIcon) return;

        themeIcon.classList.toggle('bi-moon-fill', theme === 'dark');
        themeIcon.classList.toggle('bi-sun-fill', theme !== 'dark');
    }

    function toggleTheme() {
        applyTheme(
            root.getAttribute('data-theme') === 'dark'
                ? 'light'
                : 'dark'
        );
    }

    function initTheme() {
        themeIcon = document.getElementById('theme-icon');
        applyTheme(getSavedTheme());
    }

    window.toggleTheme = toggleTheme;

    if (document.readyState === 'loading')
        document.addEventListener('DOMContentLoaded', initTheme);
    else
        initTheme();

})();

// Helper function to get SweetAlert dark mode options
function getSwalOptions() {
    const currentTheme = document.documentElement.getAttribute('data-theme') || 'dark';
    const isDarkMode = currentTheme === 'dark';
    return {
        background: isDarkMode ? '#2c3034' : '#ffffff',
        color: isDarkMode ? '#f8f9fa' : '#212529',
        iconColor: isDarkMode ? '#6ea8fe' : undefined
    };
}

// Confirm Logout Function
function confirmLogout() {
    Swal.fire({
        title: 'Konfirmasi Logout',
        text: 'Apakah Anda yakin ingin logout?',
        icon: 'question',
        confirmButtonText: 'Ya, Logout',
        cancelButtonText: 'Batal',
        showCancelButton: true,
        confirmButtonColor: '#dc3545',
        cancelButtonColor: '#6c757d',
        ...getSwalOptions()
    }).then((result) => {
        if (result.isConfirmed) {
            document.getElementById('logoutForm').submit();
        }
    });
}

function confirmDeleteGeneric(btn, type) {
    Swal.fire({
        title: `Hapus ${type}?`,
        text: `Anda yakin ingin menghapus ${type.toLowerCase()} ini?`,
        icon: 'warning',
        confirmButtonText: 'Ya, Hapus',
        cancelButtonText: 'Batal',
        showCancelButton: true,
        confirmButtonColor: '#dc3545',
        cancelButtonColor: '#6c757d',
        ...getSwalOptions()
    }).then((result) => {
        if (result.isConfirmed) {
            btn.form.submit();
        }
    });
}

// Event Delegation for data-action attributes
document.addEventListener('DOMContentLoaded', function () {

    document.addEventListener('click', function (e) {
        const toggleBtn = e.target.closest('[data-action="toggle-theme"]');
        if (toggleBtn) return toggleTheme();

        const logoutBtn = e.target.closest('[data-action="confirm-logout"]');
        if (logoutBtn) return confirmLogout();

        const deleteBtn = e.target.closest('[data-action="confirm-delete"]');
        if (deleteBtn) {
            const form = deleteBtn.closest('form');

            const type =
                form?.action?.includes('/Income/Delete')
                    ? 'Income'
                    : 'Expense';

            confirmDeleteGeneric(deleteBtn, type);
        }
    });


    // Handle page size select
    const pageSizeSelect = document.getElementById('pageSizeSelect');
    if (pageSizeSelect) {
        pageSizeSelect.addEventListener('change', function () {
            const pageSize = this.value;
            const currentUrl = window.location.pathname;
            window.location.href = `${currentUrl}?page=1&pageSize=${pageSize}`;
        });
    }
});
