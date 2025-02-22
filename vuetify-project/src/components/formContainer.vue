<script setup>

    import { computed, useSlots } from 'vue';

    const props = defineProps({
        modelValue: String
    })

    const slots = useSlots();

    // Gets the root slot that has the same value as modelValue
    const match = computed( () => {

        const c = Object.values(slots.default())[0].children;

        const e = c.filter( (child) => {
            return child.key == props.modelValue
        })

        return e;
    })
</script>

<template>

  <template v-for="m in match">
    <component :is="m" />
  </template>

</template>