# Blackbird.io Crowdin Enterprise

Blackbird is the new automation backbone for the language technology industry. Blackbird provides enterprise-scale automation and orchestration with a simple no-code/low-code platform. Blackbird enables ambitious organizations to identify, vet and automate as many processes as possible. Not just localization workflows, but any business and IT process. This repository represents an application that is deployable on Blackbird and usable inside the workflow editor.

## Introduction

<!-- begin docs -->

Crowdin Enterprise is an advanced version of the Crowdin localization platform tailored for larger organizations and enterprises with complex localization needs. It offers additional features, customization options, security enhancements, and scalability to meet the demands of large-scale localization projects within corporate environments.

## Connecting

1. Navigate to apps and search for Crowdin Enterprise. If you cannot find Crowdin Enterprise then click _Add App_ in the top right corner, select Crowdin Enterprise and add the app to your Blackbird environment.
2. Click _Add Connection_.
3. Name your connection for future reference e.g. 'My Crowdin Enterprise connection'.
4. Click _Authorize connection_.
5. As a new window pops up, follow the instructions from Crowdin.
6. When you return to Blackbird, confirm that the connection has appeared and the status is _Connected_.

## Actions

### Comment 

- **Add string comment** Add new string comment
- **Delete string comment** Delete specific string comment
- **Get string comment** Get specific string comment
- **List comments** List string comments for a project

### Directory

- **Add directory** Add a new directory
- **Get directory by ID** Get a specific directory by ID
- **Get directory by path** Get a specific directory by path
- **List directories** List all directories

### File

- **Add file** Add new file
- **Delete file** Delete specific file
- **Download file** Download specific file
- **Get file** Get specific file info
- **List files** List project files
- **Update file** Update specific file

### Glossary

- **Import glossary** Import glossary from TBX file
- **Export glossary** Export glossary as TBX file

### Machine Translation

- **List machine translation engines** List all machine translation engines
- **Translate lines via machine translation engine** Translate multiple text lines via machine translation engine
- **Translate via machine translation engine** Translate text via machine translation engine

### Project

- **Add project** Add new project
- **Build project** Build project translation
- **Delete project** Delete specific project
- **Get project** Get specific project
- **List projects** List all projects

### Project Group

- **Add project group** Add a new project group
- **Delete project group** Delete specific project group
- **Get project group** Get specific project group
- **List project groups** List all project groups

### Reviewed File

- **Build reviewed source files** Build reviewed source files of specific project
- **Download reviewed source files as ZIP** Download reviewed source files of specific build as ZIP
- **Download reviewed source files** Download reviewed source files of specific build
- **Get reviewed source files build** Get specific reviewed source files build
- **List reviewed source files builds** List all reviewed source files builds of specific project

### Source String

- **Add source string** Add new source string
- **Delete source string** Delete specific source string
- **Get source string** Get specific source string
- **List strings** List all project source strings

### Storage

- **Add storage** Add new storage
- **Delete storage** Delete specific storage
- **Get storage** Get specific storage
- **List storages** List all storages

### Task

- **Add task** Add new task
- **Delete task** Delete specific task
- **Download task strings as XLIFF** Download specific task strings as XLIFF
- **Get task** Get specific task
- **List tasks** List all tasks

### Team

- **Add team** Add a new team
- **Delete team** Delete specific team
- **Get team** Get specific team
- **List teams** List all teams

### Translation 

- **Add translation** Add new translation
- **Apply pre-translation** Apply pre-translation to chosen files
- **Delete translation** Delete specific translation
- **Download translations as ZIP** Download project translations as ZIP
- **Download translations** Download project translations
- **Get translation** Get specific translation
- **List language translations** List project language translations
- **List string translations** List project string translations

### Translation Memory

- **Add translation memory segment** Add new segment to the translation memory
- **Add translation memory** Add new translation memory
- **Delete translation memory** Delete specific translation memory
- **Download translation memory** Download specific translation memory
- **Export translation memory** Export specific translation memory
- **Get translation memory** Get specific translation memory
- **List translation memories** List all translation memories

### User

- **Delete user** Delete specific user
- **Get user** Get specific user
- **Invite user** Add a new user
- **List users** List all users

### Workflow

- **List workflow steps** List all workflow steps of project

## Events

### File

- **On file added** On file added
- **On file approved** On file approved
- **On file deleted** On file deleted
- **On file reverted** On file reverted
- **On file translated** On file fully translated
- **On file updated** On file updated

### Group

- **On group created** On group created
- **On group deleted** On group deleted

### Project

- **On project approved** On project approved
- **On project built** On project built
- **On project created** On project created
- **On project deleted** On project deleted
- **On project translated** On project translated

### String 

- **On string added** On string added
- **On string deleted** On string deleted
- **On string updated** On string updated

### String Comment

- **On string comment created** On string comment created
- **On string comment deleted** On string comment deleted
- **On string comment restored** On string comment restored
- **On string comment updated** On string comment updated

### Suggestion

- **On suggestion added** On suggestion added
- **On suggestion approved** On suggestion approved
- **On suggestion deleted** On suggestion deleted
- **On suggestion disapproved** On suggestion disapproved
- **On suggestion updated** On suggestion updated

### Task

- **On task added** On task added
- **On task deleted** On task deleted
- **On task status changed** On task status changed

### Translation

- **On translation updated** On translation updated

## Feedback

Do you want to use this app or do you have feedback on our implementation? Reach out to us using the [established channels](https://www.blackbird.io/) or create an issue.

<!-- end docs -->
