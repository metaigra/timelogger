import React from 'react';

export const Header = () => {
	return (
		<>
			<header className='bg-gray-900 text-white flex items-center h-12 w-full'>
				<div className='container mx-auto'>
					<a className='navbar-brand' href='/'>
						Timelogger
					</a>
				</div>
			</header>
		</>
	);
};
