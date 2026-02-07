// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// Theme Toggle Functionality
(function() {
    // Default to dark mode
    const defaultTheme = 'dark';
    
    // Get saved theme or use default
    function getSavedTheme() {
        const savedTheme = localStorage.getItem('theme');
        if (savedTheme) {
            return savedTheme;
        }
        // Check system preference if no saved theme
        if (window.matchMedia && window.matchMedia('(prefers-color-scheme: dark)').matches) {
            return 'dark';
        }
        return defaultTheme;
    }
    
    // Apply theme to document
    function applyTheme(theme) {
        document.documentElement.setAttribute('data-theme', theme);
        localStorage.setItem('theme', theme);
        
        // Update icon
        const icon = document.getElementById('theme-icon');
        if (icon) {
            if (theme === 'dark') {
                icon.classList.remove('bi-sun-fill');
                icon.classList.add('bi-moon-fill');
            } else {
                icon.classList.remove('bi-moon-fill');
                icon.classList.add('bi-sun-fill');
            }
        }
    }
    
    // Toggle theme
    function toggleTheme() {
        const currentTheme = document.documentElement.getAttribute('data-theme');
        const newTheme = currentTheme === 'dark' ? 'light' : 'dark';
        applyTheme(newTheme);
    }
    
    // Initialize theme on page load
    function initTheme() {
        const theme = getSavedTheme();
        applyTheme(theme);
    }
    
    // Make toggleTheme available globally
    window.toggleTheme = toggleTheme;
    
    // Initialize when DOM is ready
    if (document.readyState === 'loading') {
        document.addEventListener('DOMContentLoaded', initTheme);
    } else {
        initTheme();
    }
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

// Confirm Delete Function (for Expense deletion)
function confirmDelete(btn) {
    Swal.fire({
        title: 'Hapus Expense?',
        text: 'Anda yakin ingin menghapus expense ini?',
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
