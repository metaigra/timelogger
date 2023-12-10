import React from 'react';

export const UiInput = ({ ...props }) => {
	return (
		<>
			<input {...props} className='border border-black p-1' />
		</>
	);
};
