import React, { MouseEventHandler } from 'react';

export const UiButton = ({ onClick, children }: { onClick: MouseEventHandler<HTMLButtonElement>, children: string }) => {
	return (
		<button onClick={onClick} className='bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded'>
			{children}
		</button>
	);
};
