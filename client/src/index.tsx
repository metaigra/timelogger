import React from 'react';
import ReactDOM from 'react-dom/client';
import App from './app/App';
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import { ProjectView } from './features/project/ProjectView';
import { Main } from './components/Main';

ReactDOM.createRoot(document.getElementById('root') as HTMLElement).render(
	<React.StrictMode>
		<BrowserRouter>
			<Routes>
				<Route path='/' element={<App />}>
					<Route path='/' element={<Main />} />
					<Route path='/project/:id' element={<ProjectView />} />
				</Route>
			</Routes>
		</BrowserRouter>
	</React.StrictMode>
);
