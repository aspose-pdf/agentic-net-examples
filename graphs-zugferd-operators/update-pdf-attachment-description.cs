using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Drawing;

class UpdateAttachmentDescription
{
    static void Main()
    {
        const string outputPdf = "output.pdf";
        const string dummyAttachment = "dummy.txt";
        const string initialDescription = "Initial description";
        const string newDescription = "Updated description for the latest version";

        // ------------------------------------------------------------
        // Ensure the dummy file that will be attached exists.
        // ------------------------------------------------------------
        if (!File.Exists(dummyAttachment))
        {
            File.WriteAllText(dummyAttachment, "This is a dummy attachment used for demonstration.");
        }

        // ------------------------------------------------------------
        // Create a new PDF, embed the attachment and update its description.
        // ------------------------------------------------------------
        using (Document doc = new Document())
        {
            // Add a blank page where the visual attachment annotation will be placed.
            Page page = doc.Pages.Add();

            // Create a FileSpecification for the attachment with the *initial* description.
            FileSpecification fileSpec = new FileSpecification(dummyAttachment, initialDescription);
            // Provide the file's raw bytes – required for PDF/A compliance and for a proper attachment.
            fileSpec.Contents = new MemoryStream(File.ReadAllBytes(dummyAttachment));

            // Embed the file in the PDF (makes it part of the document's EmbeddedFiles collection).
            doc.EmbeddedFiles.Add(fileSpec);

            // Create a visual annotation so the user can see and open the attachment in a PDF viewer.
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 600, 150, 650);
            FileAttachmentAnnotation attachmentAnn = new FileAttachmentAnnotation(page, rect, fileSpec);
            page.Annotations.Add(attachmentAnn);

            // --------------------------------------------------------
            // Update the description to reflect the latest version.
            // --------------------------------------------------------
            fileSpec.Description = newDescription;

            // Save the resulting PDF.
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Attachment description updated and saved to '{outputPdf}'.");
    }
}
