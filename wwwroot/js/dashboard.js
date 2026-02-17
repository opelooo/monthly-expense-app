// Dashboard loading functionality
document.addEventListener('DOMContentLoaded', function () {
    loadDashboard();
});

async function loadDashboard() {
    const container = document.getElementById('dashboard-container');
    try {
        // 1. Ambil data mentah dari API
        const response = await fetch('/DashboardAPI/GetDashboardData');
        if (!response.ok) throw new Error("Gagal mengambil data API");
        const data = await response.json();

        // 2. Kirim data ke Controller untuk dirender jadi HTML
        const componentResponse = await fetch('/Home/RenderDashboardPartial', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(data)
        });

        if (!componentResponse.ok) {
            const errorText = await componentResponse.text();
            console.error("Server Error:", errorText);
            throw new Error("Gagal merender tampilan dashboard");
        }

        const html = await componentResponse.text();

        // 3. Masukkan ke HTML
        container.innerHTML = html;

        const skeleton = document.getElementById('skeleton-loading');
        const content = document.getElementById('dashboard-content');

        if (skeleton) skeleton.style.display = 'none';
        content.classList.add('fade-in', 'loaded');

    } catch (error) {
        console.error(error);
        container.innerHTML = `
            <div class="alert alert-danger shadow-sm">
                <i class="bi bi-exclamation-triangle-fill me-2"></i>
                Gagal memuat dashboard: ${error.message}
            </div>`;
    }
}
