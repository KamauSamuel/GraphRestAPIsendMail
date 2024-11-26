# My Project
A Web API using Microsoft Graph SDK to Create and Send Emails with File Attachments
# Pre-requisites
You would need to pre-configure the Application on the Azure portal and grant it consent to send emails (Mail.Send). The settings from the azure portal then are updated on the appsetting.json file

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

### Authentication
[AuthenticationProvider Class](MSGraph.SendEmail/Authentication/AuthenticationProvider.cs) caters for authentication on the API. The [AppRegistration](MSGraph.SendEmail/Authentication/AppRegistration.cs) gets the authentication details from the app settings json file 
and later in the controller the method getGraphClient is called and it returns a GraphServiceClient instance. 

### FormFileUpload
In addition to the [DTO](MSGraph.SendEmail/Model/RequestBody.cs) collecting the subject, recipient and email body, users can also upload files as email attachments. 
The [FormFileUpload class] (MSGraph.SendEmail/File Attachment/FormFileUpload.cs) contains the method UploadAttachment() which takes a List of type IForm file as an argument and returns a list of file attachments. The method gets each file from the iform file, checks for null then retreives the following:
 - Name (Required) : The name representing the text that is displayed below the icon representing the embedded attachment
 - ContentByte (Required): The base64-encoded contents of the file
 - Adds : "@odata.type": "#microsoft.graph.fileAttachment"
 - contentType : The content type of the attachment.

  This class returns a list of type FileAttachment which is later added as an email attachment to the email body on the Controller

### Combining Everything on the Controller
The [SendMailController](MSGraph.SendEmail/Controllers/SendMailController.cs) combines everything together on a POST endpoint with the method SendEmail(). The Message resource type is created and the properties are updated from the DTO (subject, toRecipients, body and Attachments).
The [sendMail method] (https://learn.microsoft.com/en-us/graph/api/user-sendmail?view=graph-rest-1.0&tabs=csharp#example-3-create-a-message-with-a-file-attachment-and-send-the-message) is called and the message and savetoSentItems are added as [parameters] (https://learn.microsoft.com/en-us/graph/api/user-sendmail?view=graph-rest-1.0&tabs=csharp#request-body).

