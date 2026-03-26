using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

public class Program
{
    public static void Main()
    {
        const string inputPath = "input.pdf";
        const string attachmentPath = "attachment.pdf";
        const string outputPath = "output.pdf";

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

        using (Document doc = new Document(inputPath))
        {
            // Embed the attachment file into the PDF
            FileSpecification fileSpec = new FileSpecification(attachmentPath);
            doc.EmbeddedFiles.Add(fileSpec);

            // Define the rectangle area for the link annotation (coordinates are in points)
            Aspose.Pdf.Rectangle linkRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a link annotation on the first page
            LinkAnnotation link = new LinkAnnotation(doc.Pages[1], linkRect);

            // Set the hyperlink to the embedded file (opens the attachment when clicked)
            link.Hyperlink = new FileHyperlink(attachmentPath);

            // Optional visual styling for the annotation
            link.Color = Aspose.Pdf.Color.Blue;
            link.Border = new Border(link) { Width = 1 };

            // Add the annotation to the page's annotation collection
            doc.Pages[1].Annotations.Add(link);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with link annotation saved to '{outputPath}'.");
    }
}