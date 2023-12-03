import React from 'react';
import useProjects from './useProjects';

export default function ProjectsList() {
	const { projects, error } = useProjects();

	console.log(projects);

	return (
		<>
			<table className='table-fixed w-full'>
				<thead className='bg-gray-200'>
					<tr>
						<th className='border px-4 py-2 w-12'>#</th>
						<th className='border px-4 py-2'>Project Name</th>
						<th className='border px-4 py-2'>abc</th>
						<th className='border px-4 py-2'>xyz</th>
					</tr>
				</thead>
				<tbody>
					{projects &&
						projects.map((project) => (
							<tr>
								<td className='border px-4 py-2 w-12'>
									{project.id}
								</td>
								<td className='border px-4 py-2'>
									{project.name}
								</td>
								<td className='border px-4 py-2'>
									<button>START</button>
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
