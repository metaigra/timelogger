import React from 'react';
import useProjects, { Project } from '../../api/useProjects';
import { UiButton } from '../../ui/UiButton';
import {
	COMPLETED,
	START,
	STOP,
	toggleProjectState
} from './toggleProjectState';
import { useNavigate } from 'react-router-dom';

export default function ProjectsList() {
	const { projects, error } = useProjects();
	const toggle = toggleProjectState();
	const navigate = useNavigate();

	const sortProjectsByDeadline = (projects: Project[]) => {
		return projects.sort((a, b) => {
			const deadlineA = new Date(a.deadline).getTime();
			const deadlineB = new Date(b.deadline).getTime();
			return deadlineA - deadlineB;
		});
	};

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
						sortProjectsByDeadline(projects).map((project) => (
							<tr
								onClick={() =>
									navigate(`/project/${project.id}`)
								}
								key={project.id}
								className='hover:bg-gray-100 cursor-pointer'>
								<td className='border px-4 py-2 w-12'>
									{project.id}
								</td>
								<td className='border px-4 py-2'>
									{project.name}
								</td>
								<td className='border px-4 py-2 text-center'>
									{project.state != COMPLETED && (
										<UiButton
											color={
												project.state == STOP
													? 'blue'
													: 'green'
											}
											onClick={(event) => {
												event.stopPropagation();
												toggle.mutate({
													Id: project.id,
													State:
														project.state == START
															? STOP
															: START
												});
											}}>
											{project.state == START
												? 'STOP'
												: 'START'}
										</UiButton>
									)}
								</td>
								<td className='border px-4 py-2 text-center'>
									{project.deadline?.toString().split('T')[0]}
								</td>
							</tr>
						))}
				</tbody>
			</table>
			{error && <div className='pt-10'>ERROR: {error.message}</div>}
		</>
	);
}
