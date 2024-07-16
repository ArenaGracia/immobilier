import { defineStore } from 'pinia';

export const storeUtilisateur = defineStore('auth', {
    state: () => ({
        roleAuth: '',
    }),
    getters: {
        isAuthenticated: state => !!state.role,
        role: state => state.roleAuth,
    },
    actions: {
        logout() {
            this.role = '';
        },
        setRole(roleUser) {
            if (typeof roleUser === 'string') {
                this.roleAuth = roleUser;
              } else {
                console.warn('Invalid role:', roleUser);
              }
        }
    },
});
