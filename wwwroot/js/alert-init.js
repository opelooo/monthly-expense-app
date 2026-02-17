// SweetAlert2 initialization - handles alert display from TempData
document.addEventListener('DOMContentLoaded', function () {
    // Check if there's alert data to display
    const alertDataElement = document.getElementById('alert-config-data');
    if (!alertDataElement) return;

    try {
        const alertConfig = JSON.parse(alertDataElement.textContent);

        // Get current theme
        const currentTheme = document.documentElement.getAttribute('data-theme') || 'dark';
        const isDarkMode = currentTheme === 'dark';

        Swal.fire({
            title: alertConfig.title || 'Info',
            text: alertConfig.message || '',
            icon: alertConfig.icon || 'info',
            confirmButtonText: alertConfig.confirmButtonText || 'OK',
            cancelButtonText: alertConfig.cancelButtonText || 'Cancel',
            showCancelButton: alertConfig.showCancelButton || false,
            showConfirmButton: alertConfig.showConfirmButton !== false,
            allowOutsideClick: alertConfig.allowOutsideClick !== false,
            allowEscapeKey: alertConfig.allowEscapeKey !== false,
            timer: alertConfig.timer || undefined,
            timerProgressBar: alertConfig.timer ? true : false,
            // Dark mode configuration
            background: isDarkMode ? '#2c3034' : '#ffffff',
            color: isDarkMode ? '#f8f9fa' : '#212529',
            confirmButtonColor: '#0d6efd',
            cancelButtonColor: '#6c757d',
            iconColor: isDarkMode ? '#6ea8fe' : undefined
        }).then(() => {
            // Clear the alert config element after showing
            alertDataElement.remove();
        });
    } catch (e) {
        console.error('Failed to parse alert config:', e);
    }
});
