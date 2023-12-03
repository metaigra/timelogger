import React from 'react';
import { AddProject } from '../../features/projects/AddProject';
import Projects from '../../features/projects/Projects';

export const Main = () => {
	return (
		<>
			<main>
				<div className='container mx-auto'>
					<AddProject />
					<Projects />
				</div>
			</main>
		</>
	);
};
