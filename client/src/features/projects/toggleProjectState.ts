import { useMutation, useQueryClient } from '@tanstack/react-query';
import APIClient from '../../api/apiClient';

interface ProjectUpdateDto {
	Id: number;
	State: 'start' | 'stop' | 'completed';
}

export const START = 'start';
export const STOP = 'stop';
export const COMPLETED = 'completed';

export const toggleProjectState = () => {
	const queryClient = useQueryClient();
	const projectSlug = 'projects';
	const projectService = new APIClient<ProjectUpdateDto>(`/${projectSlug}`);

	return useMutation({
		mutationFn: projectService.update,
		onSuccess: () => {
			queryClient.invalidateQueries({ queryKey: [projectSlug] });
		}
	});
};
