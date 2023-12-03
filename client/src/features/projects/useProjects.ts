import { useQuery } from '@tanstack/react-query';
import APIClient from '../../app/api/apiClient';

export interface Project {
	id: number;
	name: string;
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
