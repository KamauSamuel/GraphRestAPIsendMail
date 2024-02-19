using Microsoft.Graph;

namespace MSGraph.SendEmail.Model
{
    public class MessageCollection
    {
        public MessageAttachmentsCollectionPage AddAttachment(List<FileAttachment> fileitems)
        {
            MessageAttachmentsCollectionPage attachmentcollections = new MessageAttachmentsCollectionPage();
            foreach (var attachment in fileitems)
            {
                attachmentcollections.Add(attachment);
            }
            return attachmentcollections;
        }
    }
}
