import React, { MouseEventHandler } from 'react';

export const UiButton = ({
	onClick,
	children,
	color = 'blue'
}: {
	onClick: MouseEventHandler<HTMLButtonElement>;
	children: string;
	color?: 'blue' | 'green' | "grey";
}) => {
	const buttonClassName = ` bg-${color}-500 hover:bg-${color}-700 text-white font-bold py-2 px-4 rounded`;

	console.log(buttonClassName);
	return (
		<button onClick={onClick} className={buttonClassName}>
			{children}
		</button>
	);
};
