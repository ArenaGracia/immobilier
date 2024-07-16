import { createRouter, createWebHistory } from 'vue-router'
import HelloWorld from '../components/HelloWorld.vue'
import LoginAdminVue from '../components/login/LoginAdminVue.vue'
import LoginProprietaireVue from '../components/login/LoginProprietaireVue.vue'
import LoginClientVue from '../components/login/LoginClientVue.vue'
import LogoutVue from '../components/LogoutVue.vue'
import AccueilComponent from '../components/AccueilComponent.vue'
import { storeUtilisateur } from '../store/auth'

const routes = [
  {
    path: '/',
    name: 'HelloWorld',
    component: HelloWorld,
    props: { message: 'Welcome to the Home page!' },
    meta: { requiresAuth: true }
  },
  {
    path: '/accueil',
    name: 'AccueilComponent',
    component: AccueilComponent,
    meta: { requiresAuth: true, role: 'admin' } 
  },
  {
    path: '/loginAdmin',
    name: 'LoginAdminVue',
    component: LoginAdminVue
  },
  {
    path: '/loginProprietaire',
    name: 'LoginProprietaireVue',
    component: LoginProprietaireVue
  },
  {
    path: '/loginClient',
    name: 'LoginClientVue',
    component: LoginClientVue
  },
  {
    path: '/logout',
    name: 'LogoutVue',
    component: LogoutVue
  }
]

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes
});

router.beforeEach((to, from, next) => {
    const isAuthenticated = !!sessionStorage.getItem('token');
    const requiresAuth = to.matched.some(record => record.meta.requiresAuth);
    const authRole = storeUtilisateur();
    if (requiresAuth && !isAuthenticated && !authRole.isAuthenticated) {
      next({ name: 'login' });
    } else if (requiresAuth && to.meta.role && to.meta.role !== authRole.role) {
      next('/erreur');
    } else {
      next();
    }
});

export default router
