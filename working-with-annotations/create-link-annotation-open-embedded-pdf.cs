using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string sourcePdfPath   = "source.pdf";      // existing PDF to modify
        const string attachmentPath  = "attachment.pdf"; // PDF to embed as attachment
        const string outputPdfPath   = "output.pdf";

        // Ensure source and attachment files exist
        if (!File.Exists(sourcePdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {sourcePdfPath}");
            return;
        }
        if (!File.Exists(attachmentPath))
        {
            Console.Error.WriteLine($"Attachment PDF not found: {attachmentPath}");
            return;
        }

        // Open the source PDF
        using (Document doc = new Document(sourcePdfPath))
        {
            // Embed the attachment PDF into the document using the correct FileSpecification constructor
            var fileSpec = new FileSpecification(attachmentPath, "Embedded PDF attachment");
            // The MimeType property does not exist in recent Aspose.PDF versions, so it is omitted.
            doc.EmbeddedFiles.Add(fileSpec);

            // Choose the page where the link annotation will be placed (first page)
            Page page = doc.Pages[1];

            // Define the rectangle area for the link annotation (float values)
            Aspose.Pdf.Rectangle linkRect = new Aspose.Pdf.Rectangle(100f, 500f, 300f, 550f);

            // Create the link annotation
            LinkAnnotation link = new LinkAnnotation(page, linkRect)
            {
                Color = Aspose.Pdf.Color.Blue,
                Contents = "Open embedded PDF attachment"
            };

            // Set the action to launch the embedded file. Use the Name property of the FileSpecification.
            link.Action = new LaunchAction(fileSpec.Name);

            // Add the annotation to the page
            page.Annotations.Add(link);

            // Save the modified PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF with embedded attachment and link saved to '{outputPdfPath}'.");
    }
}
