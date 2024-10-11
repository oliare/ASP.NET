import { createSlice, PayloadAction } from '@reduxjs/toolkit';
import { IUser } from '../../interfaces/auth';

interface AuthState {
  isAuth: boolean; 
  user: IUser | null;
}

const initialState: AuthState = {
  isAuth: false,
  user: null,
};

const authSlice = createSlice({
  name: 'auth',
  initialState,
  reducers: {
    loginSuccess(state, action: PayloadAction<IUser>) {
      state.isAuth = true;
      state.user = action.payload;
    },
    registerSuccess(state, action: PayloadAction<IUser>) {
      state.isAuth = true;
      state.user = action.payload;
    },
    logoutSuccess(state) {
      state.isAuth = false;
      state.user = null;
    },
  },
});

export const { loginSuccess, registerSuccess, logoutSuccess } = authSlice.actions;
export default authSlice.reducer;
