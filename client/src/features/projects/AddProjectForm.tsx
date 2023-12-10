import React from 'react';
import { SubmitHandler, useForm } from 'react-hook-form';
import { UiInput } from '../../ui/UiInput';

type ProjectInput = {
	name: string;
	deadline: string;
};

export const AddProjectForm = () => {
	const { register, handleSubmit } = useForm<ProjectInput>();

	const onSubmit: SubmitHandler<ProjectInput> = (data) => console.log(data);

	return (
		<form onSubmit={handleSubmit(onSubmit)} className='flex flex-col gap-2'>
			<UiInput {...register('name')} />
			<UiInput {...register('deadline')} />
			<input
				type='submit'
				className='bg-slate-100 px-2 py-1 hover:bg-slate-400 hover:cursor-pointer'
			/>
		</form>
	);
};
