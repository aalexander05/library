<template>
    <v-main class="bg-grey-lighten-3">
        <v-container>
            <v-sheet min-height="10vh" rounded="lg">
                <v-container>
                    <div class="button-wrapper">
                        <v-btn class="btn btn-success" @click="becomeCustomer">Become Customer</v-btn>
                        <v-btn class="btn btn-success" @click="becomeLibrarian">Become Librarian</v-btn>
                    </div>
                </v-container>
            </v-sheet>
        </v-container>
    </v-main>
</template>

<script setup>
import axios from 'axios'
import { useRouter } from 'vue-router'
import { state } from '@/config/signInConfig'
import { signInManager } from '@/config/signInManager'

const { login, logout } = signInManager()

const router = useRouter()

async function becomeCustomer() {
    const response = await axios.put(`${import.meta.env.VITE_API_BASE_URL}/Role/BecomeCustomer`);
    await logout()
    router.push('/login')

}

async function becomeLibrarian() {
    const response = await axios.put(`${import.meta.env.VITE_API_BASE_URL}/Role/BecomeLibrarian`);
    await logout()
    router.push('/login')
}

</script>