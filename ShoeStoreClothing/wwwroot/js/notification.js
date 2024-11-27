document.addEventListener('DOMContentLoaded', function () {
    const notificationBtn = document.getElementById('notificationBtn');
    const notificationDropdown = document.getElementById('notificationDropdown');

    notificationBtn.addEventListener('click', function (e) {
        e.preventDefault();
        notificationDropdown.classList.toggle('active');
    });

    // Close notification dropdown if clicked outside
    document.addEventListener('click', function (e) {
        if (!notificationDropdown.contains(e.target) && !notificationBtn.contains(e.target)) {
            notificationDropdown.classList.remove('active');
        }
    });
});
