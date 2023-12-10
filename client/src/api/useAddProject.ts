import { useMutation } from 'react-query';
import { Project } from './useProjects';
import APIClient from './apiClient';
import { API_PROJECT_SLAG } from './const';
import { useQueryClient } from 'react-query';

export const useAddProject = () => {
	const queryClient = useQueryClient();

	return useMutation({
		mutationFn: (project: Project) => {
			const projectService = new APIClient<Project>(
				`/${API_PROJECT_SLAG}`
			);
			return projectService.post(project);
		},
		onSuccess: () => {
			queryClient.invalidateQueries(API_PROJECT_SLAG);
		}
	});
};
