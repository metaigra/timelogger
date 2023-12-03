import React from 'react';
import { UiButton } from '../../app/ui/UiButton';

export const AddProject = () => {
	// TODO: any
	function onClickHandle(event: any): void {
		throw new Error('Function not implemented.' + event);
	}

	return (
		<>
			<div className='flex items-center my-6'>
				<div className='w-1/2'>
					<UiButton onClick={onClickHandle}>Add entry</UiButton>
				</div>
			</div>
		</>
	);
};
