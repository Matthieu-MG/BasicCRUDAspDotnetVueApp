<script setup>
    import { RequestBackendRoute } from '@/services/requestBackend';
    import { onMounted } from 'vue';

    const props = defineProps({
       route : String,
       label: String,
       fieldName : String,
       fieldValue : String,
       fieldId : String,
       enum : Boolean
    })

    const searchInput = ref("");
    const fieldId = ref(null);
    const autocompleteItems = ref([]);
    let Put = false;

    // If prefilled data is passed in (PUT Operation)
    if(props.fieldId != null && props.fieldValue != null)
    {
        Put = true;
    }

    onMounted( async () => {
        if(props.enum) {
            autocompleteItems.value = await RequestBackendRoute('GET', `${props.route}`)

            if(autocompleteItems.value.length === 0 || !Put){
                return;
            }

            // Pre-fill field if this is a put operation
            const index = autocompleteItems.value.findIndex( (element) => element.name == props.fieldValue )
            fieldId.value = autocompleteItems.value[index].value;
        }
        // If this is a put operation, pre-fill the autocomplete field
        else if(Put){
            fieldId.value = props.fieldId;
            // Allows autocomplete component to map the id to the proper name
            autocompleteItems.value = [{id: fieldId.value, name: props.fieldValue}];
        }
    })

    async function onSearchInput(args) {
        
        // Value is less than 3 or empty, no autocomplete
        if(searchInput.value === null || searchInput.value.length < 3) {
            autocompleteItems.value = []
            return;
        }
        
        // Fetch possible matching values from backend, if no autocomplete fetched yet
        // TODO: Could use a flag instead
        if(autocompleteItems.value.length === 0){
            autocompleteItems.value = await RequestBackendRoute('GET', `${props.route}${searchInput.value}`)

            // Concatenate Full name of employees into a single string
            if(props.label.includes("Employee Name"))
            {
                autocompleteItems.value.forEach(employee => {
                    employee.name = employee.name + ' ' + employee.surname
                });
            }
        }
        else
        {
            autocompleteItems.value.filter( (x) => x.name.includes(searchInput.value) );
        }
    }

</script>

<template>
    <!-- User Input Field -->
    <v-autocomplete
        v-if="!props.enum"
        v-model="fieldId"
        v-model:search="searchInput"
        :items = "autocompleteItems"
        item-value="id"
        item-title="name"
        :label="props.label"
        @update:search="onSearchInput"
        autocomplete="off"
    />
    
    <v-autocomplete
        v-else
        v-model="fieldId"
        :items = "autocompleteItems"
        item-value="value"
        item-title="name"
        :label="props.label"
        autocomplete="off"
    />

    <input type="hidden" :value="fieldId" :name="props.fieldName"/>
</template>