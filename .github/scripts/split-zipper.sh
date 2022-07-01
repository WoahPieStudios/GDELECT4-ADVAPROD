#!/bin/bash

# Splits and zips a directory and optionally outputs it to a given folder
# Author: bulletproofpancake

# build directory : $1
# volume size : $2
# output directory : $3
# output name: $4

if [ ! -z "$1" ] && [ ! -z "$2" ] && [ ! -z "$4" ]
then
    zip -s $2'm' -r $4.zip $1

    if [ ! -z "$3" ]
    then
        if [ -d "$3" ]
        then
            mv $4.z* $3'/'
        else
            mkdir $3
            mv $4.z* $3'/'
        fi
        echo "$4.zip can be found at $3"
    else
        echo "$4.zip can be found at"
        pwd
    fi

else
    echo "Invalid parameters, be sure to run the command as"
    echo "./split-zipper.sh \$buildDirectory \$volumeSize \$outputDirectory (optional)"
fi