// Expense Index page initialization
document.addEventListener('DOMContentLoaded', function () {
    // Hide skeleton, show content immediately
    const skeleton = document.getElementById('skeleton-loading');
    const content = document.getElementById('expenses-content');
    
    if (skeleton) skeleton.style.display = 'none';
    content.classList.add('fade-in', 'loaded');
});
