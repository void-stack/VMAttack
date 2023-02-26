# Project Status [very WIP]

This project is heavily work in progress and is being created as a study for me and new reversers in .NET VM to explore and learn about code obfuscation techniques and how to reverse engineer them. 

The main focus currently of this project is on [.NET Reactor 6.9.0.0](https://www.eziriz.com/), which has a simple 1:1 CIL virtual machine.

<img src="assets/showcase.gif">

## Dependencies

This project is using the following dependencies:
- [Echo Framework](https://github.com/Washi1337/Echo): an experimental .NET assembly editor and manipulation framework
- [AsmResolver](https://github.com/Washi1337/AsmResolver): a library that provides a complete set of tools to read and modify .NET assemblies, including a fully-featured CIL (Common Intermediate Language) assembler and disassembler.
- [UseEveryOpCode](https://github.com/0xInception/UseEveryOpCode): a .NET assembly that contains a single method that uses every CIL opcode, which can be useful for testing and experimentation purposes.

# Introduction

Code obfuscation is a technique used to protect software code from reverse engineering. It makes the code difficult to understand, analyze, and modify by humans, making it harder for adversaries to access proprietary code or execute malicious attacks.

Virtualization is the most common form of code obfuscation. It transforms code into a virtual program that is no longer recognizable as its original source code, allowing it to be executed without the need for a human-readable form. However, this makes it difficult for security analysts to understand the behavior of virtualized programs, as the internal mechanism of commercial obfuscators is a black box.

# Point

Malware authors are increasingly using commercial obfuscators to make their code more difficult to reverse engineer. Obfuscators make code harder to read, analyze, and comprehend, which can hide the functionality of malicious code and evade detection by antivirus software.

The most popular obfuscators used by malware authors include Eazfuscator.NET, .Net Reactor, VMProtect, Agile, and ConfuserEX KoiVM. These obfuscators use techniques such as string encryption, control flow obfuscation, and code virtualization to make code more difficult to understand.

However, some of these obfuscators are not foolproof and can be defeated by skilled security researchers.Therefore, it's important for security researchers to remain vigilant and employ appropriate countermeasures to detect and identify malware that uses commercial obfuscators.

## 💵 Want to support?
- Donate BTC at `bc1q048wrqztka5x2syt9mtj68uuf73vqry60s38vf`
- Donate ETH at `0x86b2C17C94A1E6f35d498d17a37dc1f8A715139b`