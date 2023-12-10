import * as React from 'react';
import './style.css';
import { QueryClient, QueryClientProvider } from 'react-query';
import { Header } from '../components/Header';
import { Outlet } from 'react-router-dom';

const queryClient = new QueryClient();

export default function App() {
	return (
		<QueryClientProvider client={queryClient}>
			<Header />
			<Outlet />
		</QueryClientProvider>
	);
}
