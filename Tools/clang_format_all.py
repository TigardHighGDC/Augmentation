# Copyright (c) TigardHighGDC
# SPDX-License SPDX-License-Identifier: Apache-2.0

"""
Run this script to auto format all c# files in the Assets/Scripts folder.
Requires clang-format to be installed and in PATH.
"""

import os
import sys
import time
from sys import platform

FILES_DIR = "Assets/Scripts"
FILE_TARGETS = [".cs"]
FORMAT_COMMAND = "clang-format -i -style=file"

class Colors:
    GREEN = "\033[92m"
    YELLOW = "\033[93m"
    END = "\033[0m"


class Timer:
    def __init__(self) -> None:
        self.ticks = [time.perf_counter()]

    def tick(self) -> None:
        self.ticks.append(time.perf_counter())

    def get_elapsed(self) -> str:
        try:
            return str("{:.3f}".format(self.ticks[-1] - self.ticks[-2]))
        except IndexError:
            print("Timer: Elapsed is null; not enough ticks were recorded.")
            return "-1"


def main() -> None:
    # If os is not windows, exit
    if platform != "win32":
        print("This clang-format script is only for Windows 10 or Later.")
        sys.exit(0)

    # Find clang-format
    clang_format = None
    for path in os.environ["PATH"].split(os.pathsep):
        if os.path.exists(os.path.join(path, "clang-format.exe")):
            clang_format = os.path.join(path, "clang-format.exe")
            break

    if clang_format is None:
        print("clang-format is not found.")
        print("Please double check that clang-format can be found in PATH.")
        sys.exit(0)

    del clang_format # Not needed after validation

    # Verify the path is the root of the project
    if not os.path.exists(os.path.join(os.getcwd(), ".clang-format")): # Use .clang-format as a marker
        # Recursively search for the root of the project
        print(f"{Colors.YELLOW}Warning: .clang-format not found. Searching for root of project...{Colors.END}")
        while True: 
            os.chdir("../")
            if os.path.exists(os.path.join(os.getcwd(), ".clang-format")):
                print(f"{Colors.GREEN}Found root of project at {os.getcwd()}{Colors.END}")
                break

    # Get the list of files to format
    files = []
    timer = Timer()

    for root, _, filenames in os.walk(FILES_DIR):
        for filename in filenames:
            if os.path.splitext(filename)[1] in FILE_TARGETS:
                files.append(os.path.join(root, filename))

    timer.tick()
    print(f"{Colors.GREEN}Found {len(files)} files in {timer.get_elapsed()} seconds.{Colors.END}")

    # Format the files
    for file in files:
        print(f"{Colors.YELLOW}Formatting {file}...{Colors.END}")
        os.system(f"{FORMAT_COMMAND} {file}")

    timer.tick()
    print(f"{Colors.GREEN}Formatted {len(files)} files in {timer.get_elapsed()} seconds.{Colors.END}")


if __name__ == "__main__":
    main()
