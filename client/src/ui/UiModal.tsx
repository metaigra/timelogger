import React, { ReactNode } from 'react';

export const UiModal = ({
	isVisible,
	children,
	onClose
}: {
	isVisible: boolean;
	children: ReactNode;
	onClose: () => void;
}) => {
	if (!isVisible) {
		return <></>;
	}

	return (
		<div
			className='fixed inset-0 bg-black bg-opacity-25
				 backdrop-blur-sm flex justify-center items-center
				 '>
			<div className='flex flex-col w-[400px] bg-white'>
				<button
					onClick={onClose}
					className='text-xl place-self-end p-1 hover:bg-gray-100'>
					X
				</button>
				<div className='rounded-sm text-black py-2 px-3'>
					{children}
				</div>
			</div>
		</div>
	);
};
