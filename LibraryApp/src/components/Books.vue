<template>
  <v-main class="bg-grey-lighten-3">
    <v-container>
      <v-row>
        <v-col>
          <v-sheet min-height="70vh" rounded="lg">
            <v-container>
              <v-row class="mx-auto d-flex align-top justify-center">
                <v-col cols="4">

                  <v-text-field v-model="search" label="Search Titles" v-on:keyup.enter="onSearch"></v-text-field>
                </v-col>
                <v-col cols="1">
                  <v-btn @click="onSearch">Search</v-btn>
                </v-col>
                <v-col cols="1">
                  <v-btn @click="clearSearch">Clear</v-btn>
                </v-col>
              </v-row>

              <v-data-table :items="books" :headers="headers">

                <template v-slot:item.publicationDate="{ item }">
                  <span>{{ new Date(item.publicationDate).toLocaleString() }}</span>
                </template>
                <template v-slot:item.returnDueDate="{ item }">
                  <span>{{ new Date(item.returnDueDate).toLocaleString() }}</span>
                </template>
                <template v-if="state.roles.includes('Librarian')" v-slot:item.actions="{ item }">
                  <router-link :to="`/book/view/${item.id}`">
                    <v-icon class="me-2" size="small">
                      mdi-eye
                    </v-icon>
                  </router-link>
                  <router-link :to="`/book/edit/${item.id}`">
                    <v-icon class="me-2" size="small">
                      mdi-pencil
                    </v-icon>
                  </router-link>
                  <v-icon size="small" @click="deleteItem(item)">
                    mdi-delete
                  </v-icon>
                </template>
                <template v-if="state.roles.includes('Customer')" v-slot:item.actions="{ item }">
                  <v-btn :disabled="item.isCheckedOut" color="blue-darken-1" variant="text"
                    @click="checkOut(item.id)">Check Out</v-btn>
                </template>
              </v-data-table>
            </v-container>
          </v-sheet>
        </v-col>
      </v-row>
    </v-container>
  </v-main>

  <v-dialog v-model="showDeleteDialog" max-width="500px">
    <v-card>
      <v-card-title class="text-h5">Are you sure you want to delete this item?</v-card-title>
      <v-card-actions>
        <v-spacer></v-spacer>
        <v-btn color="blue-darken-1" variant="text" @click="closeDeleteDialog">Cancel</v-btn>
        <v-btn color="blue-darken-1" variant="text" @click="deleteItemConfirm">OK</v-btn>
        <v-spacer></v-spacer>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>

<script setup>
import { ref, onMounted } from 'vue'
import axios from 'axios'
import { state } from '@/config/signInConfig'

const books = ref([])
const search = ref("")


onMounted(async () => {
  await getBooks()
})

async function getBooks() {
  const response = await axios.get(`${import.meta.env.VITE_API_BASE_URL}/CheckoutBook/GetAllBooks`)
  books.value = response.data
}

const headers = [
  { title: 'Title', key: 'title' },
  { title: 'Author', key: 'author' },
  { title: 'Publication Date', key: 'publicationDate' },
  { title: 'Checked Out', key: 'isCheckedOut' },
  { title: 'Return Due Date', key: 'returnDueDate' },
  { title: 'Actions', key: 'actions' },
];

let showDeleteDialog = ref(false)
let bookToDelete;

function deleteItem(item) {
  bookToDelete = item
  showDeleteDialog.value = true

}

function closeDeleteDialog() {
  showDeleteDialog.value = false
}


async function deleteItemConfirm() {
  const result = await axios.delete(`${import.meta.env.VITE_API_BASE_URL}/Book/${bookToDelete.id}`);
  await getBooks()
  closeDeleteDialog()
}

async function checkOut(id) {
  const result = await axios.put(`${import.meta.env.VITE_API_BASE_URL}/CheckoutBook/${id}`);
  await getBooks()
}

async function onSearch() {
  const result = await axios.get(`${import.meta.env.VITE_API_BASE_URL}/CheckoutBook/Search?title=${search.value}`);
  books.value = result.data
}

async function clearSearch() {
  search.value = ""
  await getBooks()
}

</script>