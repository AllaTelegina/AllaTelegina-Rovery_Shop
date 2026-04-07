console.log("COOKIE INIT");

function cookieCenter() {
    return {
        visible: false,
        openSettings: false,

        performance: false,
        functional: false,
        advertising: false,

        lang: 'pl',
        t: {},

        init() {
            this.lang = localStorage.getItem('lang') || 'pl';
            this.t = window.cookieTranslations[this.lang];

            const saved = JSON.parse(localStorage.getItem("cookiePrefs"));
            if (saved) {
                this.performance = saved.performance;
                this.functional = saved.functional;
                this.advertising = saved.advertising;
                this.visible = false;
            } else {
                this.visible = true;
            }
        },

        acceptAll() {
            this.performance = true;
            this.functional = true;
            this.advertising = true;
            this.savePreferences();
        },

        acceptNecessary() {
            this.performance = false;
            this.functional = false;
            this.advertising = false;
            this.savePreferences();
        },

        savePreferences() {
                localStorage.setItem("cookiePrefs", JSON.stringify({
                performance: this.performance,
                functional: this.functional,
                advertising: this.advertising
            }));

                this.visible = false;
                this.openSettings = false;
        },

        setLang(code) {
               this.lang = code;
               localStorage.setItem("lang", code);
               this.t = window.cookieTranslations[code];
       }
    }
}
