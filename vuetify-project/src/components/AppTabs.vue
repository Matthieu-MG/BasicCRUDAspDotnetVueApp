<script setup>
    import { RequestBackendRoute } from '@/services/requestBackend';
    import { camelCaseToSpaceCapitalized } from '@/services/utilities';
    import { onMounted, toRaw, ref, watch } from 'vue';
    import AddRecordBtn from './AddRecordBtn.vue';
    import addRecordForm from './addRecordForm.vue';
    import FormContainer from './formContainer.vue';

    const tab = ref(null);
    const dialog = ref(false);
    const record = ref([])

    const search = ref("")
    const sortBy = ref("")
    const sortOrder = ref("desc")
    const globalItemsPerPage = 5;

    watch(dialog, async (newValue, oldValue) => {
        // When dialog is closed, reset record value (reset auto-fill form on put action)
        if(newValue === false) {
            record.value = []
        }
    })

    function createTable(_label, _route)
    {
        return {
            label: _label,
            route: _route,
            data: {
                headers: [],
                items: [],
                totalRecords: 0,
                searchedTotalRecords: 0,
                sortOptions: [],
                postAttribs: []
            }
        }
    }

    const tables = ref([
        createTable("Quotations", "Quotation"),
        createTable("Products", "Product"),
        createTable("Societies", "Society"),
        createTable("Employees", "Employee")
    ])

    // General Utility Function To Get a specific page for a table
    async function GetPage(table, pageNumber)
    {
        return await RequestBackendRoute('GET', 
            `${table.route}/page?Page=${pageNumber}&ItemsPerPage=${globalItemsPerPage}&SortOrder=${sortOrder.value}&SortBy=${sortBy.value}&SearchTerm=${search.value}`)
    }

    // Called when table is updated, refreshes to first page
    async function refreshTable(route) {
        const table = tables.value.find(x => x.label === tab.value );
        table.data.items = (await GetPage(table, 1)).items;
    }

    // Callback to when table element changes to next page
    async function loadPage({page, itemsPerPage, _sortBy}) {

        const table = tables.value.find(x => x.label === tab.value );

        table.data.items = (await GetPage(table, page)).items;
    }

    // Callback when searching and/or sorting a specific table
    async function applyQueryParameters()
    {
        const table = tables.value.find(x => x.label === tab.value );

        const page = await GetPage(table, 1)

        table.data.searchedTotalRecords = page.totalItems;
        table.data.items = page.items;
    }

    onMounted(async () => {
        try {

            tables.value.forEach(async table => {    
                // TODO Batch these into a single Data Structure on Back-end to reduce API calls
                const page = await GetPage(table, 1);

                table.data.items = page.items;
                table.data.totalRecords = page.totalItems;
                table.data.searchedTotalRecords = table.data.totalRecords;

                const headers = Object.keys(await RequestBackendRoute('GET', `${table.route}/GetDTO`)).filter((x) => {
                    return !x.toLowerCase().includes('id');
                })
                
                table.data.headers = headers.map(header => ({
                    title: camelCaseToSpaceCapitalized(header),
                    align: 'start',
                    key: header
                }));
                table.data.headers.push({
                    title: 'Actions',
                    align: 'start',
                    width: "1px",
                    key: 'actions'
                });

                table.data.postAttribs = await RequestBackendRoute('GET', `${table.route}/PostDTO`)

                table.data.sortOptions = await RequestBackendRoute('GET', `${table.route}/SortOptions`);
            });
        }
        catch (error) {
            throw error;
        }
    });

    async function deleteItem(id, route) {
        try {
            await RequestBackendRoute('DELETE', `${route}/${id}`)
        }
        catch (e) {
            console.log(e)
        }
    }

    async function updateItem(item) {
        const recordData = toRaw(item);
        record.value = Array.from(Object.entries(recordData), ([key, value]) => value.toString())
        dialog.value = true;
    }

    async function showDashboard() {
        console.log("Show dashboard")
    }

</script>

<template>
    <v-app-bar>
        <v-app-bar-title>
            <v-icon icon="mdi-atom" @click="showDashboard"></v-icon>
            Atom
        </v-app-bar-title>

        <v-btn icon>
            <v-icon icon="mdi-account"></v-icon>
        </v-btn>
        <v-btn icon>
            <v-icon icon="mdi-logout"></v-icon>
        </v-btn>
    </v-app-bar>
    
    <v-main>
    <v-tabs v-model="tab">
        <Tab v-for="table in tables" :title="table.label" :value="table.label" :key="table.label"/>
    </v-tabs>

    <v-tabs-window v-model="tab">
        <v-tabs-window-item v-for="table in tables" :value="table.label" :key="table.label">
            <v-card width="100%" class="mx-auto">
                <v-card-text>
                
                    <!-- Search Bar & Sort Options -->
                    <form @submit.prevent="applyQueryParameters">

                        <v-text-field density="compact" v-model="search" 
                            placeholder="Search" variant="outlined" clearable
                            bg-color="teal"
                            base-color="grey-darken-3"
                            color="grey-darken-3"
                        >
                        
                            <template #prepend-inner>
                                <v-icon class="ma-3" icon="mdi-magnify"></v-icon>
                            </template>
                            
                        </v-text-field>
                    </form>

                    <v-expansion-panels bg-color="blue-lighten-1" class="pb-6">
                        <v-expansion-panel>
                            <v-expansion-panel-title>

                                <v-row no-gutters>
                                    Sort Records
                                </v-row>
                                
                            </v-expansion-panel-title>
                            <v-expansion-panel-text>
                                <v-row no-gutters>
                                    <v-radio-group inline v-model="sortBy" label="Sort By">
                                        <v-radio v-for="sortOption in table.data.sortOptions" :label="sortOption" :value="sortOption"></v-radio>
                                    </v-radio-group>
                                    
                                    <v-radio-group inline v-model="sortOrder" label="Sort Order">
                                        <v-radio label="Ascending" value="asc"></v-radio>
                                        <v-radio label="Descending" value="desc"></v-radio>
                                    </v-radio-group>
                                </v-row>

                                <v-row no-gutters>
                                    <v-btn color="white" @click="applyQueryParameters">Apply Sorting</v-btn>
                                </v-row>
                            </v-expansion-panel-text>
                        </v-expansion-panel>
                    </v-expansion-panels>
                    <!-- End Search Bar & Sort Options -->

                    <!-- Records - Table -->
                    <v-data-table-server
                    :headers="table.data.headers"
                    :items="table.data.items"
                    :items-length="table.data.searchedTotalRecords"
                    :items-per-page="globalItemsPerPage"
                    @update:options="loadPage"
                    >
                        <template v-slot:item.actions="{ item }">
                            <v-icon
                                class="me-2"
                                size="small"
                                @click="updateItem(item)"
                            >
                                mdi-pencil
                            </v-icon>
                            <v-icon
                                size="small"
                                @click="deleteItem(item.id, table.route)"
                            >
                                mdi-delete
                            </v-icon>
                        </template>
                    </v-data-table-server>

                </v-card-text>
            </v-card>

        </v-tabs-window-item>
    </v-tabs-window>

    <!-- Enter Record Form -->
    <v-dialog v-model="dialog">
        <v-card min-width="30%" class="mx-auto">
            <FormContainer v-model="tab">

                <template v-for="table in tables" :key="table.label">
                    <add-record-form
                        :value="table.label"
                        :data="toRaw(record)"
                        :post-attribs="table.data.postAttribs"
                        :route="table.route"
                        @refresh-table="refreshTable"
                    />
                </template>

            </FormContainer>
            <v-btn @click="dialog=false">Close</v-btn>
        </v-card>
    </v-dialog>
    <AddRecordBtn @click="dialog = true"/>
    <!-- End Enter Record Form -->
    </v-main>

</template>

<style scoped lang="css">
    
</style>