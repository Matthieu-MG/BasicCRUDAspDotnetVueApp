<script setup>

    import { URL } from '@/services/requestBackend';
    import PostAutocomplete from '@/components/PostAutocomplete.vue'

    const props = defineProps({
        postAttribs : Array,
        data: Array,
        value : String,
        route : String
    })

    const emit = defineEmits({
        refreshTable: ({ route }) => {
            if(route === null){
                console.warn("Route must not be null when refreshing table")
                return false;
            }
            return true;
        }
    })

    const submitButton = ref(null);
    const alertNotifier = ref(null);
    const fail = 'fail';
    const success = 'success';
    const prefilledPutData = ref([])

    function FilterGetDTODate()
    {
        if(props.postAttribs === null || props.postAttribs.length === 0 || props.data.length === 0) {
            return;
        }

        // Starts at 1, since props.data[0] is the primary key value of the record
        let getDataPointer = 1;

        props.postAttribs.forEach(element => {
            if(element.type === "foreign") {
                prefilledPutData.value.push({value: props.data[getDataPointer++], id: props.data[getDataPointer]});
            }
            else if(element.type === "price") {
                prefilledPutData.value.push(props.data[getDataPointer].replace(/[$,]/g, ''))
            }
            else {
                prefilledPutData.value.push(props.data[getDataPointer])
            }
            getDataPointer++;
        });
    }

    FilterGetDTODate();

    async function SubmitServerRequest(method, formData, submissionRoute)
    {
        try {
            if(submitButton.value) {
                submitButton.value.innerHTML = 'Processing...';
            }

            // Constructs an object from the array of entries that FormData stores as an array
            const data = Object.fromEntries( formData.entries() );

            props.postAttribs.forEach(element => {
                if(element.type === "enum") {
                    data[element.name] = parseInt(data[element.name])
                }
            });

            const response = await fetch(submissionRoute, {
                method: method,
                headers: {
                    'Content-Type' : 'application/json'
                },
                body : JSON.stringify(data)
            });

            if(!response.ok) {
                alertNotifier.value = fail;
                return;
            }

            alertNotifier.value = success;
            const route = props.route
            emit('refreshTable', { route })
        }

        catch (error) {
        throw error;
        }
    }

    async function formSubmission(event)
    {
        await SubmitServerRequest('post', new FormData(event.target), `${URL}/${props.route}`);
    }

    async function formPut(event)
    {
        await SubmitServerRequest('put', new  FormData(event.target),`${URL}/${props.route}/${props.data[0]}`);
    }

</script>

<template>

    <v-alert v-if="alertNotifier === success" type="success" title="Success" text="Record Successfully Added!">
    </v-alert>
    <v-alert v-else-if="alertNotifier === fail" type="error" title="Error" text="Failed to Add Record!">
    </v-alert>

    <v-form v-if="data.length != 0" method="put" @submit.prevent="formPut">
        <template v-for="(attrib, index) in postAttribs">

            <post-autocomplete v-if="attrib.type == 'foreign'"
                :label="attrib.label"
                :field-name="attrib.name"
                :route="attrib.route"
                :field-value="prefilledPutData[index].value"
                :field-id="prefilledPutData[index].id"
                :enum="false"
            />

            <post-autocomplete v-else-if="attrib.type == 'enum'"
                :label="attrib.label"
                :field-name="attrib.name"
                :route="attrib.route"
                :field-value="prefilledPutData[index]"
                :enum="true"
            />      

            <v-text-field v-else
            :model-value="prefilledPutData[index]"
            :label="attrib.label"
            :name="attrib.name"
            autocomplete="off"
            />

        </template>

        <v-btn type="submit" ref="submitButton">
            Create
        </v-btn>
    </v-form>

    <v-form v-else method="post" @submit.prevent="formSubmission">

        <template v-for="(attrib, index) in postAttribs">
            <post-autocomplete v-if="attrib.type == 'foreign'"
                :label="attrib.label"
                :field-name="attrib.name"
                :route="attrib.route"
                :enum="false"
            />

            <post-autocomplete v-else-if="attrib.type == 'enum'"
                :label="attrib.label"
                :field-name="attrib.name"
                :route="attrib.route"
                :enum="true"
            />   

            <v-text-field v-else
                :label="attrib.label"
                :name="attrib.name"
                autocomplete="off"
            />

        </template>

        <v-btn type="submit" ref="submitButton">
            Create
        </v-btn>
    </v-form>
</template>