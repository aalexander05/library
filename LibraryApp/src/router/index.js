import { createWebHistory, createRouter } from "vue-router";
import { signInManager } from "@/config/signInManager";
import { state } from "@/config/signInConfig";

import Books from "@/components/Books.vue";
import About from "@/components/About.vue";
import BookEdit from "@/components/BookEdit.vue";
import JobView from "@/components/BookView.vue";
import Login from "@/components/Login.vue";
import SignUp from "@/components/SignUp.vue";
import ChooleRole from "@/components/ChooseRole.vue"

const routes = [
  {
    path: "/",
    name: "Home",
    component: Books,
    meta: {
      requiresAuth: true,
    },
  },
  {
    path: "/login",
    name: "Login",
    component: Login,
  },
  {
    path: "/signup",
    name: "Sign Up",
    component: SignUp,
  },
  {
    path: "/choose-role",
    name: "Choose Role",
    component: ChooleRole,
  },
  {
    path: "/about",
    name: "About",
    component: About,
  },
  {
    path: "/book/edit/:id?",
    name: "Book Edit",
    alias: "/job/edit/:id",
    component: BookEdit,
    meta: {
      requiresAuth: true,
    },
  },
  {
    path: "/book/view/:id?",
    name: "Job View",
    component: JobView,
    meta: {
      requiresAuth: true,
    },
  },
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

router.beforeEach(async (to, from, next) => {
  if (to.matched.some((record) => record.meta.requiresAuth)) {
    // this route requires auth, check if logged in
    // if not, redirect to login page.

    if (!state.isAuthenticated) {
      next({ name: "Login" });
    } else {
      next(); // go to wherever I'm going
    }
  } else {
    next(); // does not require auth, make sure to always call next()!
  }
});

export default router;
