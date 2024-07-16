<script>
    export default{
        name: 'LogoutVue',

        created() {
            sessionStorage.removeItem('token');
            this.$router.push('/login');
        }
    }
</script>