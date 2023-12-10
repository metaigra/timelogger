import React, { useState } from 'react';
import { UiButton } from '../../ui/UiButton';
import { ModalDialog } from '../../components/ModalDialog';
import { AddProjectForm } from './AddProjectForm';

export const AddProject = () => {
	const [addProjectDialog, setAddProjectDialog] = useState(false);

	return (
		<>
			<div className='flex items-center my-6'>
				<div className='w-1/2'>
					<UiButton onClick={() => setAddProjectDialog(true)}>
						Add entry
					</UiButton>
				</div>
				<ModalDialog
					isVisible={addProjectDialog}
					onClose={() => setAddProjectDialog(false)}>
					<AddProjectForm />
				</ModalDialog>
			</div>
		</>
	);
};
