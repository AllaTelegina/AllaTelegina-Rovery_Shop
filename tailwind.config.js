/** @type {import('tailwindcss').Config} */
module.exports = {
  content: [
    "./index.html",
      "./*.html",
      "./src/**/*.{js,ts,jsx,tsx}",
      "./node_modules/flowbite/**/*.js"
  ],

  theme: {
    extend: {
       keyframes: {
            rotate: {
                '0%': { transform: 'perspective(1000px) rotateY(0deg)'},
                '100%': { transform: 'perspective(1000px) rotateY(360deg)'}
            }
        },
        animation: {
            rotate: 'rotate 30s linear infinite',
        },
        colors: {
            'regal-grin': '#092F2D',
            'regal-blue': '#29D6CB',
            'regal-blue-l': '#28D7CE',
            'regal-blue-lig': '#b2f0ed',
            'regal-blue-light': '#e4faf9',
            'regal-red': '#D62934',

            'dark-bg': '#0B3936',
            'dark-text': '#e0e0e0',
            'accent': '#D62934',
        },
    
    },
  },
  
  plugins: [
      require('flowbite/plugin'),
      require('flowbite-typography'),
  ],
}