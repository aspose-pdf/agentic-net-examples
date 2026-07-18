using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdfPath   = "input.pdf";
        const string attachmentPath = "embedded.pdf";
        const string outputPdfPath  = "output.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(attachmentPath))
        {
            Console.Error.WriteLine($"Attachment PDF not found: {attachmentPath}");
            return;
        }

        using (Document doc = new Document(inputPdfPath))
        {
            // Embed the PDF as an embedded file (Attachments property does not exist)
            FileSpecification fileSpec = new FileSpecification(attachmentPath);
            doc.EmbeddedFiles.Add(fileSpec);

            // Choose the page where the link annotation will be placed (first page)
            Page page = doc.Pages[1];

            // Define the rectangle area for the link annotation
            Aspose.Pdf.Rectangle linkRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create the link annotation (initialize first, then set Border to avoid CS0165)
            LinkAnnotation link = new LinkAnnotation(page, linkRect)
            {
                Color = Aspose.Pdf.Color.Blue,
                Contents = "Open embedded PDF"
            };

            // Set a visible border (optional)
            link.Border = new Border(link) { Width = 1 };

            // Assign a FileHyperlink that points to the embedded file by its name
            link.Hyperlink = new FileHyperlink(fileSpec.Name);

            // Add the annotation to the page
            page.Annotations.Add(link);

            // Save the modified PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF with embedded attachment and link saved to '{outputPdfPath}'.");
    }
}
