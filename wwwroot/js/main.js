 //import './input.css'


document.addEventListener('click', function (event) {
  const dropdowns = document.querySelectorAll('[id^="mega-menu-full-image-dropdown-"]');
  const toggles = document.querySelectorAll('[data-collapse-toggle]');

  let clickedToggle = false;

  toggles.forEach(toggle => {
    if (toggle.contains(event.target)) {
      const targetId = toggle.getAttribute('data-collapse-toggle');
      const dropdown = document.getElementById(targetId);

      // Переключаем видимость
      if (dropdown) {
        dropdown.classList.toggle('hidden');
      }

      clickedToggle = true;
    }
  });

  if (!clickedToggle) {
    // Клик был вне переключателей — закрываем все dropdown
    dropdowns.forEach(dropdown => dropdown.classList.add('hidden'));
  }
});



