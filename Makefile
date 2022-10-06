name := ProjectCuteAndFunny.CunnyApi
tarball := $(name).tar


all: api

clean:
	rm -rf build

api: clean
	dotnet publish -o build/$(name) -r linux-x64 -c RELEASE --self-contained

package: api clean
	cd build && tar c $(name) >$(tarball)
	cd build && gzip -f $(tarball)