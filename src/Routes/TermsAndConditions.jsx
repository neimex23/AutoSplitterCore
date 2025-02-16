import { Header } from "../Components/Home/Header";
import { Footer } from "../Components/Home/Footer";
import { FireParticles } from "../Components/Home/FireParticles";

export const TermsAndConditions = () => {
  return (
    <>
      <div className="relative w-full h-screen">
        <img
          src={`${import.meta.env.BASE_URL}assets/sek.webp`}
          alt="Privacy Background"
          className="fixed inset-0 w-full h-full object-cover -z-10"
        />

        <FireParticles />
        <Header h2Valor={"Terms & Conditions"} />
        <div className="max-w-4xl mx-auto p-4 m-4 bg-white shadow-lg rounded-lg">
          <h1 className="text-3xl font-bold text-gray-800 mb-4">
            Cloud Profile
          </h1>
          <p>
            Welcome to the Cloud Profile feature of AutoSplitterCore. By using
            this feature, you agree to abide by the following terms and
            conditions. These rules are designed to ensure a positive and
            respectful experience for all members of our community. If you do
            not agree to these terms, please refrain from using the Cloud
            Profile functionality.
          </p>

          <h2 className="text-2xl font-semibold text-gray-700 mt-6">
            1. Purpose of the Cloud Profile Feature
          </h2>
          <p>
            The Cloud Profile feature allows users to upload and download
            program profiles (configurations) shared by the community. Each
            profile may include:
          </p>
          <ul className="list-disc list-inside">
            <li>
              <strong>Title:</strong> A descriptive name for the profile.
            </li>
            <li>
              <strong>Author:</strong> The name or alias of the profile creator.
            </li>
            <li>
              <strong>Description:</strong> A summary of the profile’s purpose
              or features.
            </li>
            <li>
              <strong>Configuration or Splits:</strong> Specific settings or
              splits used in the program.
            </li>
          </ul>

          <h2 className="text-2xl font-semibold text-gray-700 mt-6">
            2. Guidelines for Profile Uploads
          </h2>
          <h3 className="text-xl font-medium text-gray-600 mt-4">
            2.1 Content Standards
          </h3>
          <ul className="list-disc list-inside">
            <li>
              Titles, author names, and descriptions must be meaningful and
              relevant.
            </li>
            <li>
              Profiles should not contain inappropriate, offensive, or vulgar
              language.
            </li>
            <li>
              Meme content is allowed if it remains respectful and
              non-offensive.
            </li>
          </ul>

          <h3 className="text-xl font-medium text-gray-600 mt-4">
            2.2 Prohibited Content
          </h3>
          <ul className="list-disc list-inside">
            <li>No malicious, irrelevant, or nonsensical content (“trash”).</li>
            <li>Avoid repetitive uploads of identical profiles.</li>
            <li>
              Hate speech, discrimination, harassment, or abuse is strictly
              prohibited.
            </li>
          </ul>

          <h2 className="text-2xl font-semibold text-gray-700 mt-6">
            3. Community Expectations
          </h2>
          <ul className="list-disc list-inside">
            <li>Contribute high-quality and thoughtful profiles.</li>
            <li>Respect other users and their contributions.</li>
            <li>Provide accurate and honest information.</li>
          </ul>

          <h2 className="text-2xl font-semibold text-gray-700 mt-6">
            4. Moderation and Enforcement
          </h2>
          <ul className="list-disc list-inside">
            <li>
              Uploaded profiles are subject to review. Violations may result in
              removal.
            </li>
            <li>Repeated violations may lead to suspension of access.</li>
          </ul>

          <h2 className="text-2xl font-semibold text-gray-700 mt-6">
            5. Disclaimer
          </h2>
          <ul className="list-disc list-inside">
            <li>
              AutoSplitterCore is not responsible for the accuracy or quality of
              user-uploaded profiles.
            </li>
            <li>
              The Cloud Profile feature is provided “as-is” without guarantees.
            </li>
          </ul>

          <h2 className="text-2xl font-semibold text-gray-700 mt-6">
            6. Changes to These Terms
          </h2>
          <p>
            AutoSplitterCore reserves the right to update these terms at any
            time. Updates will be communicated through official channels.
          </p>

          <h2 className="text-2xl font-semibold text-gray-700 mt-6">
            7. Contact
          </h2>
          <p>If you have any questions or concerns, contact us at:</p>
          <p className="text-blue-600">
            <a href="mailto:ezequielmedina23@gmail.com">
              ezequielmedina23@gmail.com
            </a>
          </p>
        </div>
        <Footer />
      </div>
    </>
  );
};
