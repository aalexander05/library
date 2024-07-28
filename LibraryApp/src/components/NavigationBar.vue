<template>
    <v-app-bar flat>
        <v-container class="mx-auto d-flex align-center justify-center">
            <v-avatar class="me-4 " color="grey-darken-1" size="32"></v-avatar>

            <v-btn :text="'Books'" :to="'/'"></v-btn>
            <v-btn :text="'Add New'" :to="'/book/edit'"></v-btn>
            <v-btn :text="'About'" :to="'/about'"></v-btn>

            <v-spacer></v-spacer>
            <span v-if="state.isAuthenticated" class="mx-5">
                <span v-if="state.roles.includes('Librarian')">Welcome, Librarian! </span>
                <span v-if="state.roles.includes('Customer')">Welcome, Customer!</span>
                <span v-if="state.roles.length === 0">Welcome!</span>
            </span>

            <span v-if="state.isAuthenticated">
                <v-btn :text="'Log Out'" variant="outlined" @click="handleLogout"></v-btn>
            </span>

            <span v-else>
                <v-btn :text="'Sign Up'" variant="outlined" :to="'/signup'" class="mx-3"></v-btn>
                <v-btn :text="'Log In'" variant="outlined" @click="handleLogin"></v-btn>
            </span>

            <!-- <v-responsive max-width="160">

                <v-text-field density="compact" label="Search" rounded="lg" variant="solo-filled" flat hide-details
                    single-line></v-text-field>
            </v-responsive> -->
        </v-container>
    </v-app-bar>
</template>

<script setup>
import axios from 'axios'
import { onMounted, ref } from 'vue'
import { signInManager } from '@/config/signInManager'
import { state } from '@/config/signInConfig'
import { useRouter } from 'vue-router'
const router = useRouter()
const { login, logout, registerAuthorizationHeaderInterceptor } = signInManager()


async function handleLogin() {
    router.push('/login')
}

async function handleSignUp() {
    router.push('/signup')
}

function handleLogout() {
    logout()
    router.push('/login')
}

async function initialize() {
    try {
        registerAuthorizationHeaderInterceptor() // Call the initialize function
    } catch (error) {
        console.log('Initialization error', error)
    }
}

onMounted(async () => {
    await initialize()
})

</script>
