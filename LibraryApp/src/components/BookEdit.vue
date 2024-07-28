<template>
    <v-main class="bg-grey-lighten-3">
        <v-container>
            <v-sheet min-height="10vh" rounded="lg">
                <v-container>

                    <v-form class="px-4">
                        <v-row>
                            <v-col lg='6' xl="4" sm="12">

                                <v-text-field v-model="v$.title.$model" label="Name"
                                    :error-messages="v$.title.$errors.map(e => e.$message)" />

                                <v-text-field v-model="v$.author.$model" label="Author"
                                    :error-messages="v$.author.$errors.map(e => e.$message)" />

                                <v-text-field v-model="v$.description.$model" label="Description"
                                    :error-messages="v$.description.$errors.map(e => e.$message)" />

                                <v-text-field v-model="v$.publisher.$model" label="Publisher"
                                    :error-messages="v$.publisher.$errors.map(e => e.$message)" />

                                <v-text-field v-model="v$.publicationDate.$model" label="Publication Date" type="date" :value="state.publicationDate ? new Date(state.publicationDate).toISOString().substr(0, 10) : ''"
                                    :error-messages="v$.publicationDate.$errors.map(e => e.$message)" />

                                <v-text-field v-model="v$.category.$model" label="Category"
                                    :error-messages="v$.category.$errors.map(e => e.$message)" />

                                <v-text-field v-model="v$.isbn.$model" label="ISBN"
                                    :error-messages="v$.isbn.$errors.map(e => e.$message)" />

                                <v-text-field v-model="v$.pageCount.$model" label="Page Count" type="number"
                                    :error-messages="v$.pageCount.$errors.map(e => e.$message)" />


                            </v-col>
                        </v-row>


                        <div class="button-wrapper">
                            <v-btn class="btn btn-success" @click="onSubmit">submit</v-btn>
                        </div>
                    </v-form>
                    <!-- <pre>{{ state }}</pre> -->
                    <pre>{{ submitError }}</pre>
                </v-container>
            </v-sheet>
        </v-container>
    </v-main>
</template>

<script setup>
import axios from 'axios'
import { reactive, ref } from 'vue' // "from '@vue/composition-api'" if you are using Vue <2.7
import { useVuelidate } from '@vuelidate/core'
import { required, email, helpers, integer } from '@vuelidate/validators'
import { useRouter } from 'vue-router'

const router = useRouter()

let currentPathObject = router.currentRoute.value

let state
if (router.currentRoute.value.params.id) {
    const book = await axios.get(`${import.meta.env.VITE_API_BASE_URL}/Book/${router.currentRoute.value.params.id}`)
    state = reactive(book.data);
} else {
    state = reactive({
        id: 0,
        title: null,
        author: null,
        description: null,
        publisher: null,
        publicationDate: null,
        category: null,
        isbn: null,
        pageCount: null,
    })
}

const rules = {
    id: { required },
    title: { required: helpers.withMessage('This field cannot be empty', required) },
    author: { required },
    description: { required },
    publisher: { required },
    publicationDate: { required },
    category: { required },
    isbn: { required },
    pageCount: { required, integer },
}

const v$ = useVuelidate(rules, state)

const submitError = ref(null)

async function onSubmit() {
    await v$.value.$validate()
    if (v$.value.$invalid) {
        return
    }
    if (!state.id) {
        try {
            const response = await axios.post(`${import.meta.env.VITE_API_BASE_URL}/Book`, state);
        } catch (error) {
            console.error(error)
            if (error.response.status === 500) {
                submitError.value = "There was a server error"
            }
            return
        }
    } else {
        const response = await axios.put(`${import.meta.env.VITE_API_BASE_URL}/Book`, state);
    }
    router.push('/')
}

</script>