window.CookieService = {
    key: "cookieConsent",

    get() {
        try {
            const raw = localStorage.getItem(this.key);
            return raw ? JSON.parse(raw) : null;
        } catch (e) {
            console.warn("CookieService.get error", e);
            return null;
        }
    },

    set(data) {
        try {
            localStorage.setItem(this.key, JSON.stringify({
                ...data,
                updatedAt: new Date().toISOString(),
                version: 1
            }));
        } catch (e) {
            console.warn("CookieService.set error", e);
        }
    },

    clear() {
        localStorage.removeItem(this.key);
    },

    hasConsent() {
        return !!this.get();
    }
};
