import { useQuery } from 'react-query';
import APIClient from './apiClient';
import { API_PROJECT_SLAG } from './const';

export interface Project {
	id: number;
	name: string;
	state: 'start' | 'stop' | 'completed';
	deadline: Date;
}

const useProjects = () => {
	const projectService = new APIClient<Project>(`/${API_PROJECT_SLAG}`);

	const { data: projects, error } = useQuery<Project[], Error>({
		queryKey: [API_PROJECT_SLAG],
		queryFn: projectService.getAll,
		staleTime: 60_000
	});

	return { projects, error };
};

export default useProjects;
