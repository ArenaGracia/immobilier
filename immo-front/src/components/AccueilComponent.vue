<template>
        <div class="container-scroller">
            <NavBarComponent />
            <div class="container-fluid page-body-wrapper">
                <SidebarComponent />
                <div class="main-panel">
                    <div class="content-wrapper">
                        <div class="row">
                        </div>
                    </div>
                    <FooterComponent />
                </div>
            </div>
        </div>

</template>
<script>
    import FooterComponent from './template/FooterComponent.vue';
    import SidebarComponent from './template/SidebarComponent.vue';
    import NavBarComponent from './template/NavBarComponent.vue';

    export default {
        name: 'AccueilComponent',
        
        components: {
            SidebarComponent,
            NavBarComponent,
            FooterComponent
        }  
    };
</script>