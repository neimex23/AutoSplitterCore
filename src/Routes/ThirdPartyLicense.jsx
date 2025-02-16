import { Introduction } from "../Components/ThirdPartyLicense/Introduction";
import { ContactInfo } from "../Components/ThirdPartyLicense/ContactInfo";
import { ApiInfo } from "../Components/ThirdPartyLicense/ApiInfo";
import { CustomDll } from "../Components/ThirdPartyLicense/CustomDll";
import { Licenses } from "../Components/ThirdPartyLicense/Licenses";
import { ThirdPartyNavbar } from "../Components/ThirdPartyLicense/ThirdPartyNavbar";

import { Header } from "../Components/Home/Header";
import { Footer } from "../Components/Home/Footer";
import { FireParticles } from "../Components/Home/FireParticles";

export const ThirdPartyLicense = () => {
  return (
    <>
      <div className="relative w-full h-screen">
        <img
          src={`${import.meta.env.BASE_URL}assets/ds4B.jpg`}
          alt="Privacy Background"
          className="fixed inset-0 w-full h-full object-cover -z-10"
        />
        <div className="space-y-8">
          <FireParticles />
          <Header h2Valor={"Third Party License"} />
          <ThirdPartyNavbar />
          <div className="pt-5">
            <Introduction />
            <ContactInfo />
            <ApiInfo />
            <CustomDll />
            <Licenses />
          </div>
          <Footer />
        </div>
      </div>
    </>
  );
};
