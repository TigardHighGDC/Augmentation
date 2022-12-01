# Copyright (c) TigardHighGDC
# SPDX-License SPDX-License-Identifier: Apache-2.0

"""
Run this script to validate that all c# files in the Assets/Scripts folder are formatted correctly,
as per the .clang-format file. As a consequence of running this script all files will be formatted.

Requires clang-format to be installed and in PATH. NOTE: No error checking is done to verify this.
This script is intended to be run automatically by the CI system.

This script is run by GitHub Actions on every push and pull request.
"""

import os
import sys

def main():
    # Verify the path is the root of the project
    if not os.path.exists(os.path.join(os.getcwd(), ".clang-format")): # Use .clang-format as a marker
        while True: 
            os.chdir("../")
            if os.path.exists(os.path.join(os.getcwd(), ".clang-format")):
                print(f"Found root of project at {os.getcwd()}")
                break

    # Get all files in the Assets/Scripts folder
    files = []
    for root, dirs, file_names in os.walk("Assets/Scripts"):
        for file_name in file_names:
            if file_name.endswith(".cs"):
                files.append(os.path.join(root, file_name))

    # Test each file
    for file in files:
        previous_file_state = open(file, "r").read()
        os.system(f"clang-format -i {file}")
        new_file_state = open(file, "r").read()

        if previous_file_state != new_file_state:
            print(f"File {file} is not formatted correctly!")
            sys.exit(1)

    print("All files formatted correctly")


if __name__ == "__main__":
    main()
