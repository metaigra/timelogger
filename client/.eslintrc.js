module.exports = {
	parser: '@typescript-eslint/parser',
	parserOptions: {
		project: 'tsconfig.json',
		tsconfigRootDir: __dirname,
		sourceType: 'module'
	},
	plugins: ['@typescript-eslint/eslint-plugin'],
	extends: [
		'plugin:@typescript-eslint/recommended',
		'plugin:prettier/recommended'
	],
	root: true,
	env: {
		node: true,
		jest: true
	},
	ignorePatterns: ['.eslintrc.js', '.ejs'],
	rules: {
		'no-empty': 'error',
		'no-multiple-empty-lines': 'warn',
		'no-var': 'error',
		'prefer-const': 'error',
		semi: ['error', 'always'],
		quotes: ['error', 'single'],
		//"brace-style": [2, "allman", { "allowSingleLine": true }],
		curly: ['error', 'multi-line'], // This enables the curly rule for multi-line statements

		'@typescript-eslint/interface-name-prefix': 'off',
		'@typescript-eslint/explicit-function-return-type': 'off',
		'@typescript-eslint/explicit-module-boundary-types': 'off',
		'@typescript-eslint/no-explicit-any': 'off',

		'prettier/prettier': [
			'error',
			{
				endOfLine: 'auto'
			}
		]
	}
};
