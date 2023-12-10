import React, { MouseEventHandler } from 'react';

export const UiButton = ({
	onClick,
	children,
	color = 'blue'
}: {
	onClick: MouseEventHandler<HTMLButtonElement>;
	children: string;
	color?: 'blue' | 'green';
}) => {
	
	// HACK: colors purged by webpack (???)
	return (
		<>
			{color == 'blue' && (
				<button onClick={onClick} className="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded">
					{children}
				</button>
			)}
			{color == 'green' && (
				<button onClick={onClick} className="bg-green-500 hover:bg-green-700 text-white font-bold py-2 px-4 rounded">
					{children}
				</button>
			)}
		</>
	);
};
