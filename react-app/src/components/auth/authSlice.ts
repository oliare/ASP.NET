import { createSlice, PayloadAction } from '@reduxjs/toolkit';

interface User {
  image?: string;
  firstName?: string;
  lastName?: string;
}

interface AuthState {
  isAuth: boolean; 
  user: User | null;
}

const initialState: AuthState = {
  isAuth: false,
  user: null,
};

const authSlice = createSlice({
  name: 'auth',
  initialState,
  reducers: {
    registerSuccess(state, action: PayloadAction<User>) {
      state.isAuth = true;
      state.user = action.payload;
    },
    loginSuccess(state, action: PayloadAction<User>) {
      state.isAuth = true;
      state.user = action.payload;
    },
    logoutSuccess(state) {
      state.isAuth = false;
      state.user = null;
    },
  },
});

export const { registerSuccess, loginSuccess, logoutSuccess } = authSlice.actions;
export default authSlice.reducer;
