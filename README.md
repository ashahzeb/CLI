# NFT.CLI
## Command Line Tool

## Features

- Add, Remove, Transfer tokens
- Accepts inline json and json files
- Retains state in between runs

## Tech/Packages

Dillinger uses a number of open source projects to work properly:

- [.Net Core] - 7.0
- [Xunit] - for unit tests
- [Moq] - for mockiing data in tests
- [Autofixture] - for creating objects in tests
- [MediatR] is used for CQRS

And of course Dillinger itself is open source with a [public repository][dill]
 on GitHub.

## Installation

Unzip the NFT-CLI.zip to a folder

Change directory

```sh
cd NFT-CLI
```

Install cli tool using below command

```sh
dotnet tool install --global --add-source nft.cli
```

Now you can use the cli globally in your machine

## Usage

CLi support following commands

### Read-Inline

```sh
program --read-inline '{ "Type": "Burn", "TokenId":             01:07:00 PM
"0xD000000000000000000000000000000000000000", "Address":
"0x1000000000000000000000000000000000000000" }'
```

### Read-File

```sh
program --read-file input.json
```

### NFT

```sh
program --nft 0xC000000000000000000000000000000000000000
```
### Wallet
```sh
program --wallet 0x3000000000000000000000000000000000000000
```
### Reset
```sh
 program --reset
```
## Notes

- I have placed a sample input.json file in the root folder. You can utilize the same or use your own. If you plan to use file from other directory traverse to that directory and run the same command with your json file name.
- System is built in accordance to the provided requirement document.

## Improvements
 For a production application a few of the improvements are listed below
 - exception handling would be improved
 - Currently system caters commands with the first provided option value, e.g instead of ```sh program --reset ``` if you provide command ```sh program --reset 123```, it won't consider 123. Handling such cases would be added to the application
 - there is no help switch available for now, that would be added
 - Project structure would have projects created with limited scope, e.g Tools folder from CLI project would be a separate project, domain folder from application project would be a separate project (and so on).
 - Currently to retain state, json file is used, instead of that a database with EntityFramework code first approach would be used
 - Support for relative path for readfile command would be added (not tested yet)
 - MediatR will be replaced by a queuing system (e.g kafka, rabbitmq etc)
