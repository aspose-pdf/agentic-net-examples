using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string attachmentPath = "attachment.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPath}");
            return;
        }
        if (!File.Exists(attachmentPath))
        {
            Console.Error.WriteLine($"Attachment file not found: {attachmentPath}");
            return;
        }

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // Embed the external PDF file into the document
            using (FileStream attStream = File.OpenRead(attachmentPath))
            {
                // Create a FileSpecification for the attachment and assign its content stream
                var fileSpec = new FileSpecification(attachmentPath, "attachment.pdf");
                fileSpec.Contents = attStream;

                // Add the specification to the EmbeddedFiles collection
                doc.EmbeddedFiles.Add(fileSpec);
            }

            // Choose the page where the link annotation will be placed (first page)
            Page page = doc.Pages[1];

            // Define the rectangle area for the link annotation (float values)
            Aspose.Pdf.Rectangle linkRect = new Aspose.Pdf.Rectangle(100f, 500f, 300f, 550f);

            // Create the link annotation
            LinkAnnotation link = new LinkAnnotation(page, linkRect);
            link.Color = Aspose.Pdf.Color.Blue;
            link.Border = new Border(link) { Width = 1 };

            // Set the hyperlink to the embedded file (use the name stored in the FileSpecification)
            link.Hyperlink = new FileHyperlink("attachment.pdf");

            // Add the annotation to the page
            page.Annotations.Add(link);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with embedded attachment link saved to '{outputPath}'.");
    }
}
