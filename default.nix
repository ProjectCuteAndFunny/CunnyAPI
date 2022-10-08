{ lib
, stdenv
, fetchNuGet
, buildDotnetModule
, dotnet-sdk_6
, dotnet-aspnetcore_6
, pkg-config }:

buildDotnetModule rec {
  pname = "CunnyAPI";
  version = "1.1";
  src = ./.;
  nugetDeps = ./deps.nix;
  dotnet-sdk = dotnet-sdk_6;
  dotnet-runtime = dotnet-aspnetcore_6;
  postFixup = ''
    cp $src/CunnyAPI/appsettings.json $out/bin/appsettings.json
  '';
  executables = [ "CunnyAPI" ];
  meta = with lib; {
    homepage = "https://github.com/ProjectCuteAndFunny/CunnyAPI";
    description = "The CunnyAPI";
    license = licenses.gpl3;
    platforms = [ "x86_64-linux" ];
  };
}
