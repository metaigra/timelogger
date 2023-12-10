import React from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import useProjects from '../../api/useProjects';
import { UiButton } from '../../ui/UiButton';

export const ProjectView = () => {
	const { projects } = useProjects();
	const navigate = useNavigate();
	const params = useParams<{ id: string }>();
	const projectId = parseInt(params.id?.toString() ?? '');
	const project = projects?.find((p) => p.id == projectId);
	if (!project) navigate('/');

	console.log(project);
	return (
		<>
			<div>Name: {project?.name}</div>
			<div>
				{project?.intervals.map((i) => {
					const startedTime = new Date(i.started);
					const completedTime = new Date(i.completed);
					const durationInMilliseconds: number =
						completedTime.getTime() - startedTime.getTime();
					const durationInMinutes: number = Math.floor(
						durationInMilliseconds / (1000 * 60)
					);
					const durationInHours: number = Math.floor(
						durationInMinutes / 60
					);

					return (
						<div key={project.id}>
							{`${durationInHours} hours ${
								durationInMinutes % 60
							} minutes (${durationInMilliseconds} ms)`}
						</div>
					);
				})}
			</div>
			<UiButton onClick={() => navigate(-1)}>Back</UiButton>
		</>
	);
};
