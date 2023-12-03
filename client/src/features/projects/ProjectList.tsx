import React from 'react';
import useProjects from './useProjects';
import { UiButton } from '../../app/ui/UiButton';

export default function ProjectsList() {
	const { projects, error } = useProjects();

	return (
		<>
			<table className='table-fixed w-full'>
				<thead className='bg-gray-200'>
					<tr>
						<th className='border px-4 py-2 w-12'>#</th>
						<th className='border px-4 py-2'>Project Name</th>
						<th className='border px-4 py-2'>Action</th>
						<th className='border px-4 py-2'>Deadline</th>
					</tr>
				</thead>
				<tbody>
					{projects &&
						projects.map((project) => (
							<tr key={project.id} className="hover:bg-gray-100 cursor-pointer">
								<td className='border px-4 py-2 w-12'>
									{project.id}
								</td>
								<td className='border px-4 py-2'>
									{project.name}
								</td>
								<td className='border px-4 py-2 text-center'>
									<UiButton>START</UiButton>
								</td>
								<td className='border px-4 py-2'>
									{project.deadline}
								</td>
							</tr>
						))}
				</tbody>
			</table>
			{error && <div className='pt-10'>ERROR: {error.message}</div>}
		</>
	);
}
