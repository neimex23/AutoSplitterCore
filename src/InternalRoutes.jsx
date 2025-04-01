import { Routes, Route, Navigate } from "react-router-dom";
import { Home } from "./Routes/Home";
import { ThirdPartyLicense } from "./Routes/ThirdPartyLicense";
import { PrivacyPolicyDoc } from "./Routes/PrivacyPolicyDoc";
import { TermsAndConditions } from "./Routes/TermsAndConditions";
import { BetaDownload } from "./Routes/BetaDownload";

export default function InternalRoutes() {
  return (
    <Routes>
      <Route path="/" element={<Home />} />
      <Route path="/ThirdPartyLicense" element={<ThirdPartyLicense />} />
      <Route path="/TermsAndConditions" element={<TermsAndConditions />} />
      <Route path="/PrivacyPolicy" element={<PrivacyPolicyDoc />} />
      <Route path="/OpenBeta" element={<BetaDownload />} />
      <Route path="*" element={<Navigate to="/" replace />} />
    </Routes>
  );
}
