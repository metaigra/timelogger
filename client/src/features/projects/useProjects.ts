import { useQuery } from '@tanstack/react-query';
import APIClient from '../../api/apiClient';

export interface Project {
	id: number;
	name: string;
	state: 'start' | 'stop' | 'completed';
	deadline: string;
}

const useProjects = () => {
	const projectSlug = 'projects';
	const projectService = new APIClient<Project>(`/${projectSlug}`);

	const { data: projects, error } = useQuery<Project[], Error>({
		queryKey: [projectSlug],
		queryFn: projectService.getAll,
		staleTime: 60_000
	});

	return { projects, error };
};

export default useProjects;
