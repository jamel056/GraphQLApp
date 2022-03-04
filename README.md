# GraphQLApp

## .net5 with GraphQL.Net

- Used: Playground package
-  Used Bodies for Testing:

- Url: http://localhost:5000/command

- ********** Query Command ****************
```sh
{
  first: command (id: 2){
    ...commandFields
  }
  second: commands{
   ...commandFields
  }
}
fragment commandFields on Command
{
  id
  howTo
  platformId
  commandLine
}
 
{
  platforms{
    id
    name
    typeEnum
    commands{
      id
      howTo
    }
  }
}

query Command($showHowTo: Boolean!)
{
  command(id: 5)
  {
    howTo @include(if: $showHowTo)
    commandLine
  }
}
```
- ********** Mutation Command ****************
```sh
{
  "command": {
    "howTo": "testfsdfdsfdsf",
    "commandLine": "test testdsfdsfdsfdsf",
    "platformId": 9
  }
}

mutation($command: CommandInput!)
{
  createCommand(command: $command)
  {
    id
    howTo
    commandLine
    platformId
  }
}

{
  "id": 5,
  "command": {
    "howTo": "testttt",
    "commandLine": "ttttttttttttttttttt",
    "platformId": 9
  }
}

mutation($id: ID!, $command: CommandInput!)
{
  updateCommand(id:$id,command:$command)
  {
    id
    howTo
    platformId
    commandLine
  }
}

{
  "commandId": 12
}

mutation($commandId: ID!)
{
  deleteCommand(id: $commandId)
}
```

- Url:http://localhost:5000/platform

- ********** Query Command ****************
```sh
{
  platforms{
    id
    name
    typeEnum
    commands
    {
      id
      howTo
    }
  }
}

{
  platform(id:1)
  {
    id
    name
    typeEnum
    commands
    {
      id
      howTo
    }
  }
}
```
- ********** Mutation Platform ****************
```sh
{
  "platform": {
    "name":"Android",
    "typeEnum": "cmd"
  }
}

mutation($platform: PlatformInput!)
{
  createPlatform(platform: $platform)
  {
    id
    name
    typeEnum
  }
}

{
  "id": 5,
  "platform": {
    "name": "RedHat",
    "typeEnum": "linux"
  }
}

mutation($id: ID!, $platform: PlatformInput!)
{
  updatePlatform(id:$id,platform:$platform)
  {
    id
    name
    typeEnum
  }
}

{
  "platformId": 9
}

mutation($platformId: ID!)
{
  deletePlatform(id: $platformId)
}
```
