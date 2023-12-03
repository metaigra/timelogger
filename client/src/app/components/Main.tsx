import React from 'react';
import { AddProject } from '../../features/projects/AddProject';
import ProjectsList from '../../features/projects/ProjectList';

export const Main = () => {
	return (
		<>
			<main>
				<div className='container mx-auto'>
					<AddProject />
					<ProjectsList />
				</div>
			</main>
		</>
	);
};
