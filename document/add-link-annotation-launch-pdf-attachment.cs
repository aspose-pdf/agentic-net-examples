using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string attachmentPath = "attachment.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath) || !File.Exists(attachmentPath))
        {
            Console.Error.WriteLine("Input PDF or attachment file not found.");
            return;
        }

        // Load the source PDF
        using (Document doc = new Document(inputPath))
        {
            // Choose the page where the annotations will be placed
            Page page = doc.Pages[1];

            // Optional: embed the PDF as a file attachment annotation
            Aspose.Pdf.Rectangle attachRect = new Aspose.Pdf.Rectangle(100, 500, 120, 520);
            FileSpecification fileSpec = new FileSpecification(attachmentPath);
            FileAttachmentAnnotation fileAnn = new FileAttachmentAnnotation(page, attachRect, fileSpec)
            {
                // Icon property removed because the IconEnum is not available in the current Aspose.PDF version.
                // The default icon will be used (Paperclip).
                Color = Aspose.Pdf.Color.Gray
            };
            page.Annotations.Add(fileAnn);

            // Create a link annotation that launches the attached PDF when clicked
            Aspose.Pdf.Rectangle linkRect = new Aspose.Pdf.Rectangle(150, 500, 300, 520);
            LinkAnnotation link = new LinkAnnotation(page, linkRect)
            {
                Color = Aspose.Pdf.Color.Blue
            };
            // LaunchAction opens the external PDF file
            link.Action = new LaunchAction(attachmentPath);
            // Add a visible border to the link annotation
            link.Border = new Border(link) { Width = 1 };

            page.Annotations.Add(link);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with link annotation saved to '{outputPath}'.");
    }
}
