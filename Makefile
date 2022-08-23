tarball := ProjectCuteAndFunny.CunnyApi.tar


all: api

clean:
	rm -rf build

api: clean
	dotnet publish -o build -r linux-x64 -c RELEASE --self-contained

package: api clean
	tar c build >$(tarball)
	gzip $(tarball)
