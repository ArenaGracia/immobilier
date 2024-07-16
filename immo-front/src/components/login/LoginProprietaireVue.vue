<template>
        <div class="container-scroller">
        <div class="container-fluid page-body-wrapper full-page-wrapper">
            <div class="content-wrapper d-flex align-items-center auth px-0">
                <div class="row w-100 mx-0">
                    <div class="col-lg-4 mx-auto">
                        <div class="auth-form-light text-left py-5 px-4 px-sm-5">
                            <div class="brand-logo">
                                <img src="/assets/images/logo.svg" alt="logo">
                            </div>
                            <h4>Hello! Propriétaire</h4>
                            <strong v-if="errorMessage" class="error">{{ errorMessage }}</strong> <!-- Afficher le message d'erreur -->

                            <form class="pt-3" @submit.prevent="handleLogin">
                                <div class="form-group">
                                    <input type="text" v-model="contact" class="form-control form-control-lg" id="exampleInputEmail1" placeholder="Contact">
                                </div>
                                
                                <div class="mt-3">
                                    <input type="submit" class="btn btn-primary mr-2" value="Se connecter" />
                                </div>
                            </form>
                            
                        </div>
                    </div>
                </div>
            </div>
        </div>
        </div>
</template>
  
<script>
    import { loginProprietaire } from '../../services/loginService'; 
    import { storeUtilisateur } from '../../store/auth';

  
    export default {
        name: 'LoginProprietaireVue',
        data() {
            return {
                email: '',
                mdp: '',
                contact: '',
                errorMessage: ''
            };
        },

        methods: {
            async handleLogin() {
                const data = await loginProprietaire(this.email, this.mdp, this.contact);
                console.log(data);
                if (data["erreur"]===null) {
                    const authStore = storeUtilisateur();
                    sessionStorage.setItem("token",data["data"].token);
                    authStore.setRole(data["data"].role);
                    this.$router.push('/accueilProprietaire');
                } else {
                    this.errorMessage = data["erreur"]; 
                }
            }
        }
    };
</script>
  
<style scoped>
    .error {
        color: red;
    }
</style>
  