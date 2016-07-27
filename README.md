# ProjectToReadme

This is a simple command-line tool, that enables C# developers to turn their project.json files into a beautifully looking Markdown, HTML or plain text file.

## Usage
Open up Windows Command Prompt (**Win+R** and enter **cmd.exe**) or Powershell. Locate the **ProjectToReadme.exe**'s folder on your computer (the executable file's name is due to change in future releases).
### Example
Typing`-s project.json -f Markdown -t File` as the command-line arguments will generate a README.md file for a project.json file which is located in the **same** directory as the executable file.
### Command-line arguments
So far, there are 3 supported and (all) required options to be supplied for the program to work properly.
* -s or -S -> This option specifies the location of the source project.json file. Possible values: **any valid file location**.
* -f or -F -> This option specifies, whether the output will be a README in Markdown, HTML or plain text. Possible values: **Markdown**, **Html** or **Text**.
* -t or -T -> This option specifies, whether the output will be placed in a README file (with the appropriate extension) or outputted into the console. Possible values: **Text** or **File**.

## Authors
[Daniel Zikmund](http://zikmund.me/)
##License
License is included in the [LICENSE.txt](https://github.com/DZetko/ProjectToReadme/blob/master/LICENSE.txt) file of this project.
