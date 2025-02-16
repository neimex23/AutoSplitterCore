export const Footer = () => {
    return (
        <footer className="w-full bg-gray-900 text-white py-4 text-center rounded">
            <div className="container mx-auto flex flex-col items-center gap-2">
                <div className="flex flex-wrap justify-center gap-3 text-sm md:text-base">
                    <a
                        href="https://github.com/neimex23/AutoSplitterCore"
                        target="_blank"
                        rel="noopener noreferrer"
                        className="hover:underline"
                    >
                        Open Source on GitHub
                    </a>
                    <span className="opacity-50">|</span>
                    <a
                        href="https://neimex23.github.io/AutoSplitterCore/TermsAndConditions.html"
                        target="_blank"
                        rel="noopener noreferrer"
                        className="hover:underline"
                    >
                        Terms and Conditions
                    </a>
                    <span className="opacity-50">|</span>
                    <a
                        href="https://neimex23.github.io/AutoSplitterCore/PrivacyPolicy.html"
                        target="_blank"
                        rel="noopener noreferrer"
                        className="hover:underline"
                    >
                        Privacy Policy
                    </a>
                </div>
                <p className="text-xs md:text-sm opacity-75">
                    © 2025 AutoSplitterCore. Open Source Project.
                </p>
            </div>
        </footer>
    );
};
