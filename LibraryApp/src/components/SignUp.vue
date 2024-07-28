<template>
    <v-main class="bg-grey-lighten-3">
        <v-container class="mt-3">
            <v-sheet min-height="70vh" rounded="lg">
                <v-container>
                    <v-row>
                        <v-col xs="12" lg="6">
                            <div v-if="state.isAuthenticated">
                                <v-btn :text="'Log Out'" variant="outlined" @click="handleLogout"
                                    class="ma-2 pa-2"></v-btn>
                                <v-btn :to="'/'" variant="outlined" class="ma-2 pa-2">Home</v-btn>
                            </div>

                            <span v-else>
                                <v-text-field v-model="v$.email.$model" label="Email"
                                    :error-messages="v$.email.$errors.map(e => e.$message)" />

                                <v-text-field v-model="v$.password.$model" label="Password" type="password"
                                    :error-messages="v$.password.$errors.map(e => e.$message)" />

                                <v-btn :text="'Register'" variant="outlined" @click="handleLogin"></v-btn>

                                <br />
                                <p class="mt-3">Already have an account? <router-link :to="'/login'">Log
                                        In</router-link>
                                </p>
                            </span>

                        </v-col>
                    </v-row>
                </v-container>
            </v-sheet>
        </v-container>
    </v-main>
</template>

<script setup>
import { signInManager } from '@/config/signInManager'
import { state } from '@/config/signInConfig'
import { useRouter } from 'vue-router'
import { useVuelidate } from '@vuelidate/core'
import { reactive, ref, computed } from 'vue'
import { required, email, helpers, sameAs, minLength } from '@vuelidate/validators'

const router = useRouter()
const { login, logout, register } = signInManager()

const loginState = reactive({
    email: '',
    password: ''
})


const rules = {
    email: { required, email },
    password: { required, minLength: minLength(8) }
}

const v$ = useVuelidate(rules, loginState)

async function handleLogin() {
    console.log(loginState.password)
    await v$.value.$validate()
    if (v$.value.$invalid) {
        return
    }
    await register(loginState)
    router.push('/login')
}

function handleLogout() {
    logout()
}
</script>