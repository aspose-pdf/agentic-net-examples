using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main(string[] args)
    {
        // Create a simple PDF document
        using (Document doc = new Document())
        {
            doc.Pages.Add();
            doc.Save("sample.pdf");
        }

        // Measure size before adding attachment
        FileInfo infoBefore = new FileInfo("sample.pdf");
        long sizeBefore = infoBefore.Length;

        // Create a dummy file to attach
        string attachmentPath = "attachment.txt";
        File.WriteAllText(attachmentPath, "This is an attachment file used for testing.");

        // Open the PDF and add a file attachment annotation
        using (Document doc = new Document("sample.pdf"))
        {
            Page page = doc.Pages[1];
            Rectangle rect = new Rectangle(100, 600, 200, 700);
            FileSpecification fileSpec = new FileSpecification(attachmentPath);
            FileAttachmentAnnotation fileAttachment = new FileAttachmentAnnotation(page, rect, fileSpec);
            // Correct enum usage for the icon
            fileAttachment.Icon = FileIcon.PushPin;
            fileAttachment.Contents = "Test attachment";
            page.Annotations.Add(fileAttachment);
            doc.Save("sample_with_attachment.pdf");
        }

        // Measure size after adding attachment
        FileInfo infoAfter = new FileInfo("sample_with_attachment.pdf");
        long sizeAfter = infoAfter.Length;

        // Simple verification
        if (sizeAfter > sizeBefore)
        {
            Console.WriteLine($"Test passed: PDF size increased from {sizeBefore} bytes to {sizeAfter} bytes.");
        }
        else
        {
            Console.WriteLine($"Test failed: PDF size did not increase. Before = {sizeBefore} bytes, After = {sizeAfter} bytes.");
        }
    }
}
