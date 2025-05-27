/** @type {import('tailwindcss').Config} */
module.exports = {
    content: [
        "./Pages/**/*.{cshtml,html}",
        "./Views/**/*.{cshtml,html}",
        "./Shared/**/*.{cshtml,html}",
        "./wwwroot/**/*.{html,js}",
        "./**/*.cshtml"
    ],
    theme: {
        extend: {
            colors: {
                'custom-blue': '#2F80ED',
                'custom-dark': '#252C32',
                'custom-light': '#E5E9EB',
                'custom-lighter': '#F8FBFF',
                'custom-lightest': '#F7F7F7',
                'custom-gray': '#777777',
                'custom-blue-light': '#2568C1'
            },
            borderColor: {
                'custom-blue': '#2F80ED',
                'custom-dark': '#252C32',
                'custom-light': '#E5E9EB',
                'custom-lighter': '#F8FBFF',
                'custom-lightest': '#F7F7F7',
                'custom-gray': '#777777',
                'custom-blue-light': '#2568C1'
            }
        }
    },
    plugins: [require("daisyui")],
    daisyui: {
        themes: true,
        base: true,
        styled: true,
        utils: true,
        logs: true
    }
}