# My Project
A Web API using Microsoft Graph SDK to Create and Send Emails with File Attachments
# Pre-requisites
You would need to pre-configure the app on the Azure portal and grant it consent to send emails. The settings from the azure portal then are updated on the appsetting.json file

    clientId = "YOUR_CLIENT_ID";
    tenantId = "YOUR_TENANT_ID";
    clientSecret = "YOUR_CLIENT_SECRET";
    
The above credentails are required for authentication. [The client credential flow enable the app to run without user interaction](https://learn.microsoft.com/en-us/graph/sdks/choose-authentication-providers?tabs=csharp#using-a-client-secret).
The appsettings.json file also includes a userPrincipalName field where the sender's name is filled.

## Classes and Controllers Overview

This section provides an overview of the main classes and controllers used in the project.

- **SendMailController**: Manages user-related operations.
- **FormFileUpload**: Handles file upload.
- **AuthenticationProvider**: Manages authentication-related operations.
- **MessageCollection**: Add email attachment
- **RequestBody**: A DTO acting as the request body.

## FormFileUpload
This class contains the method gets files from iform file and returns a list of file attachments
### UploadAttachment
Gets each file from the iform file, checks for null then retreives the following:
 - Name (Required) : The name representing the text that is displayed below the icon representing the embedded attachment
 - ContentByte (Required): The base64-encoded contents of the file
 - Adds : "@odata.type": "#microsoft.graph.fileAttachment"
 - contentType : The content type of the attachment.
  

