#!/bin/bash

DIST=$(uname -a)

echo ${DIST}

if [[ "${DIST}" =~ ubuntu ]]
then
	echo "This is Ubuntu"
	apt install -y build-essential autoconf libtool pkg-config
	apt install -y cmake
	apt install -y clang libc++-dev
else
	echo "There is not installation step related to this OS."
fi
