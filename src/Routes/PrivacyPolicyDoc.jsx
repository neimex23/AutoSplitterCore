import { Header } from "../Components/Home/Header";
import { Footer } from "../Components/Home/Footer";
import { FireParticles } from "../Components/Home/FireParticles";

export const PrivacyPolicyDoc = () => {
  return (
    <>
      <div className="relative w-full h-screen">
        <img
          src={`${import.meta.env.BASE_URL}assets/hol.webp`}
          alt="Privacy Background"
          className="fixed inset-0 w-full h-full object-cover -z-10"
        />
        <FireParticles />
        <Header h2Valor={"Privacy Policy"} />
        <div className="max-w-4xl mx-auto p-6 bg-white rounded-lg shadow-md m-5">
          <p className="text-gray-600 mb-6">
            <strong>Last Updated:</strong>{" "}
            <span className="text-gray-800">02-01-25</span>
          </p>
          <p className="text-gray-600 mb-4">
            AutoSplitterCore is committed to protecting your privacy. This
            Privacy Policy explains how we collect, use, and protect the
            information you provide when using our application.
          </p>

          {/* Section: Data We Collect */}
          <section className="mb-6">
            <h2 className="text-2xl font-semibold text-gray-800 mb-2">
              1. Data We Collect
            </h2>
            <p className="text-gray-600 mb-2">
              We collect the following Google user data:
            </p>
            <ul className="list-disc list-inside text-gray-700 space-y-2">
              <li>
                <strong>Email Address:</strong> Retrieved via Google
                authentication when the user logs in.
              </li>
              <li>
                <strong>Files on Google Drive:</strong> The application allows
                users to upload and download files from a shared folder.
              </li>
              <li>
                <strong>Activity Logs:</strong> Stored in Firestore for security
                and auditing purposes.
              </li>
              <li>
                <strong>Stored Profiles:</strong> User profiles and settings are
                stored in Google Drive.
              </li>
            </ul>
          </section>

          {/* Section: How We Use Your Data */}
          <section className="mb-6">
            <h2 className="text-2xl font-semibold text-gray-800 mb-2">
              2. How We Use Your Data
            </h2>
            <ul className="list-disc list-inside text-gray-700 space-y-2">
              <li>
                Managing user access and interactions within the application.
              </li>
              <li>
                Logging activities in Google Sheets for security monitoring.
              </li>
              <li>
                Preventing misuse, such as uploading inappropriate content.
              </li>
              <li>Storing user emails securely in Firestore.</li>
            </ul>
          </section>

          {/* Section: Access and Use of Google Data */}
          <section className="mb-6">
            <h2 className="text-2xl font-semibold text-gray-800 mb-2">
              3. Access and Use of Google Data
            </h2>
            <p className="text-gray-600">
              AutoSplitterCore accesses the following Google services:
            </p>
            <ul className="list-disc list-inside text-gray-700 space-y-2">
              <li>
                <strong>Google Drive:</strong> To enable users to upload and
                download profile files from a shared folder.
              </li>
              <li>
                <strong>Firestore:</strong> To log and manage user activity on
                the application.
              </li>
            </ul>
            <p className="text-gray-800 font-semibold mt-2">
              We do not access, collect, or analyze any other data from your
              Google account.
            </p>
          </section>

          {/* Section: How We Protect Your Information */}
          <section className="mb-6">
            <h2 className="text-2xl font-semibold text-gray-800 mb-2">
              4. How We Protect Your Information
            </h2>
            <ul className="list-disc list-inside text-gray-700 space-y-2">
              <li>
                All collected data is securely stored in Firestore with
                restricted access.
              </li>
              <li>
                We do not share or sell your personal data to third parties.
              </li>
              <li>
                Security measures such as authentication and access controls are
                implemented to prevent unauthorized access.
              </li>
            </ul>
          </section>

          {/* Section: Data Sharing */}
          <section className="mb-6">
            <h2 className="text-2xl font-semibold text-gray-800 mb-2">
              5. Data Sharing
            </h2>
            <p className="text-gray-600">
              We do not share your personal data with third parties, except when
              required to:
            </p>
            <ul className="list-disc list-inside text-gray-700 space-y-2">
              <li>Comply with legal obligations or regulations.</li>
              <li>Respond to valid legal requests, such as court orders.</li>
            </ul>
          </section>

          {/* Section: Data Retention */}
          <section className="mb-6">
            <h2 className="text-2xl font-semibold text-gray-800 mb-2">
              6. Data Retention
            </h2>
            <p className="text-gray-600">
              We retain user data only as long as necessary to:
            </p>
            <ul className="list-disc list-inside text-gray-700 space-y-2">
              <li>Manage access and application usage.</li>
              <li>Prevent service misuse.</li>
              <li>Comply with legal requirements.</li>
            </ul>
            <p className="text-gray-600 mt-2">
              Once the data is no longer needed, it will be securely deleted.
            </p>
          </section>

          {/* Section: Changes to This Policy */}
          <section className="mb-6">
            <h2 className="text-2xl font-semibold text-gray-800 mb-2">
              7. Changes to This Policy
            </h2>
            <p className="text-gray-600">
              We reserve the right to update this policy at any time.
              Significant changes will be communicated via email or through the
              application.
            </p>
          </section>

          {/* Section: Contact */}
          <section className="mb-6">
            <h2 className="text-2xl font-semibold text-gray-800 mb-2">
              8. Contact
            </h2>
            <p className="text-gray-600">
              If you have any questions or concerns regarding this Privacy
              Policy, you can contact us at:
            </p>
            <ul className="list-disc list-inside text-gray-700 space-y-2">
              <li>
                Email:{" "}
                <a
                  href="mailto:ezequielmedina23@gmail.com"
                  className="text-blue-600 hover:underline">
                  ezequielmedina23@gmail.com
                </a>
              </li>
            </ul>
          </section>
        </div>
        <Footer />
      </div>
    </>
  );
};
