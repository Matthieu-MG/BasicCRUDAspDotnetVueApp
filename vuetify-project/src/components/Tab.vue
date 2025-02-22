<script setup>

const props = defineProps({
    title: String
})

</script>

<template>
    <v-tab class="tab">
        {{ title }}
    </v-tab>
</template>

<style scoped lang="css">
    .tab{
        font-size: large;
    }
</style>