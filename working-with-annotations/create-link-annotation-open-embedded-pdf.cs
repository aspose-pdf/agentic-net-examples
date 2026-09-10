using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Paths for the output PDF and the PDF file to embed as an attachment
        const string outputPath = "output.pdf";
        const string attachmentPath = "attachment.pdf";

        // Ensure the attachment file exists
        if (!File.Exists(attachmentPath))
        {
            Console.Error.WriteLine($"Attachment not found: {attachmentPath}");
            return;
        }

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page (required for placing the annotation)
            Page page = doc.Pages.Add();

            // Embed the external PDF file into the document
            // Use FileSpecification constructor as required by Aspose.Pdf API
            var fileSpec = new FileSpecification(attachmentPath, "Embedded PDF Attachment");
            doc.EmbeddedFiles.Add(fileSpec);

            // Define the rectangle area for the link annotation (coordinates are in points)
            // Use Aspose.Pdf.Rectangle (float values) – no System.Drawing.Rectangle
            Aspose.Pdf.Rectangle linkRect = new Aspose.Pdf.Rectangle(100f, 500f, 300f, 550f);

            // Create the link annotation on the page
            LinkAnnotation link = new LinkAnnotation(page, linkRect);
            link.Color = Color.Blue;
            link.Border = new Border(link) { Width = 1 };

            // Set the action to open the embedded PDF attachment.
            // GoToRemoteAction references the embedded file by its name.
            link.Action = new GoToRemoteAction("attachment.pdf", 1); // open page 1 of the attached PDF

            // Add the annotation to the page
            page.Annotations.Add(link);

            // Save the resulting PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with link annotation saved to '{outputPath}'.");
    }
}
