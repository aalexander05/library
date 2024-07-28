<template>
    <v-main class="bg-grey-lighten-3">
        <v-container>
            <v-sheet min-height="10vh" rounded="lg">
                <v-container>
                    <v-row>
                        <v-col cols="auto" lg="6" sm="12">
                            <v-table v-if="book">
                                <tbody>
                                    <tr>
                                        <th>Title:</th>
                                        <td>{{ book.title }}</td>
                                    </tr>
                                    <tr>
                                        <th>Author:</th>
                                        <td>{{ book.author }}</td>
                                    </tr>
                                    <tr>
                                        <th>Description:</th>
                                        <td>{{ book.description }}</td>
                                    </tr>
                                    <tr>
                                        <th>Publication Date:</th>
                                        <td>{{ new Date(book.publicationDate).toLocaleString() }}</td>
                                    </tr>
                                    <tr>
                                        <th>Category:</th>
                                        <td>{{ book.category }}</td>
                                    </tr>
                                    <tr>
                                        <th>ISBN:</th>
                                        <td>{{ book.isbn }}</td>
                                    </tr>
                                    <tr>
                                        <th>Page Count:</th>
                                        <td>{{ book.pageCount }}</td>
                                    </tr>
                                </tbody>
                            </v-table>
                        </v-col>
                    </v-row>
                </v-container>
            </v-sheet>
        </v-container>
    </v-main>
</template>

<script setup lang="ts">
import { useRouter } from 'vue-router'
import { ref } from 'vue'
import axios from 'axios'
import { Book } from '@/models/book';

const router = useRouter()

const book: any = ref<Book>()
if (router.currentRoute.value.params.id) {
    const response = await axios.get(`${import.meta.env.VITE_API_BASE_URL}/Book/${router.currentRoute.value.params.id}`)
    const bookDto: Book = response.data
    book.value = bookDto;
}

</script>
