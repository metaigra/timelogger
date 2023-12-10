import React, { useState } from 'react';
import { UiButton } from '../../ui/UiButton';
import { UiModal } from '../../ui/UiModal';
import { SubmitHandler, useForm } from 'react-hook-form';
import { z } from 'zod';
import { zodResolver } from '@hookform/resolvers/zod';
import { Project } from '../../api/useProjects';
import { useAddProject } from '../../api/useAddProject';

const ProjectSchema = z.object({
	name: z.string().min(4, 'At least 4 characters'),
	deadline: z.coerce
		.date()
		.refine((date) => date || new Date() instanceof Date)
		.refine((date) => date >= new Date(), {
			message: 'Deadline should be in the future'
		})
		.refine((date) => date instanceof Date && !isNaN(date.getTime()), {
			message: 'Invalid date format'
		})
});

type ProjectSchemaType = z.infer<typeof ProjectSchema>;

export const AddProject = () => {
	const addProject = useAddProject();
	const [addProjectDialog, setAddProjectDialog] = useState(false);
	const {
		register,
		handleSubmit,
		formState: { errors }
	} = useForm<ProjectSchemaType>({ resolver: zodResolver(ProjectSchema) });

	const onSubmit: SubmitHandler<ProjectSchemaType> = (data) => {		
		const project: Project = { ...data, state: 'stop', id: 0 };
		addProject.mutate(project);
	};

	return (
		<>
			<div className='flex items-center my-6'>
				<div className='w-1/2'>
					<UiButton onClick={() => setAddProjectDialog(true)}>
						Add entry
					</UiButton>
				</div>
				<UiModal
					isVisible={addProjectDialog}
					onClose={() => setAddProjectDialog(false)}>
					{errors.name && (
						<div className='text-red-500'>
							{errors.name.message}
						</div>
					)}
					{errors.deadline && (
						<div className='text-red-500'>
							{errors.deadline.message}
						</div>
					)}
					<form
						onSubmit={handleSubmit(onSubmit)}
						className='flex flex-col gap-2'>
						<input
							{...register('name')}
							className='border border-black p-1'
						/>
						<input
							type='date'
							{...register('deadline')}
							className='border border-black p-1'
						/>
						<input
							type='submit'
							className='bg-slate-100 px-2 py-1 hover:bg-slate-400 hover:cursor-pointer'
						/>
					</form>
				</UiModal>
			</div>
		</>
	);
};
