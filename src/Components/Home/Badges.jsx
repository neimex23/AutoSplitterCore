import React from "react";
import { useNavigate } from "react-router-dom";

const badgesConst = [
  {
    href: "https://github.com/neimex23/AutoSplitterCore/wiki",
    imgSrc:
      "https://img.shields.io/badge/-Setup%20Guide%20%2F%20Wiki-blue?longCache=true&style=for-the-badge",
    alt: "Setup Guide / Wiki",
  },
  {
    href: "https://github.com/neimex23/AutoSplitterCore/releases/latest",
    imgSrc:
      "https://img.shields.io/github/release/neimex23/HitCounterManager.svg?label=Latest%20release:&longCache=true&style=for-the-badge&colorB=0088FF",
    alt: "Latest Release",
  },
  {
    href: "https://github.com/neimex23/AutoSplitterCore/releases",
    imgSrc:
      "https://img.shields.io/github/downloads/neimex23/HitCounterManager/total.svg?label=Downloads:&longCache=true&style=for-the-badge&colorB=0088FF",
    alt: "Downloads",
  },
  {
    href: "LICENSE",
    imgSrc:
      "https://img.shields.io/github/license/neimex23/HitCounterManager.svg?label=License:&longCache=true&style=for-the-badge&colorB=0088FF",
    alt: "License",
  },
  {
    route: "/ThirdPartyLicense",
    imgSrc:
      "https://img.shields.io/badge/-Third%20Party%20Licenses-darkblue?longCache=true&style=for-the-badge",
    alt: "Third Party Licenses",
    isFooter: true,
  },
  {
    route: "/PrivacyPolicy",
    imgSrc:
      "https://img.shields.io/badge/-Privacy%20Policy-red?longCache=true&style=for-the-badge",
    alt: "Privacy Policy",
    isFooter: true,
  },
  {
    route: "/TermsAndConditions",
    imgSrc:
      "https://img.shields.io/badge/-Terms%20and%20Conditions-darkred?longCache=true&style=for-the-badge",
    alt: "Terms and Conditions",
    isFooter: true,
  },
];

export const Badges = () => {
  const navigate = useNavigate();

  return (
    <nav className="w-full p-2 shadow-md">
      {/* Sección de badges externos */}
      <div className="container mx-auto flex flex-wrap justify-center gap-2">
        {badgesConst
          .filter((badge) => !badge.isFooter)
          .map((badge, index) => (
            <a
              key={index}
              href={badge.href}
              target="_blank"
              rel="noopener noreferrer">
              <img
                className="h-8 transition-transform transform hover:scale-105"
                src={badge.imgSrc}
                alt={badge.alt}
                aria-label={badge.alt}
              />
            </a>
          ))}
      </div>

      {/* Sección de navegación interna */}
      <div className="container mx-auto flex flex-wrap justify-center gap-2 p-3">
        {badgesConst
          .filter((badge) => badge.isFooter)
          .map((badge, index) => (
            <button
              key={index}
              type="button"
              onClick={() => navigate(badge.route)}
              className="border-none bg-transparent cursor-pointer">
              <img
                className="h-8 transition-transform transform hover:scale-105"
                src={badge.imgSrc}
                alt={badge.alt}
                aria-label={badge.alt}
              />
            </button>
          ))}
      </div>
    </nav>
  );
};
