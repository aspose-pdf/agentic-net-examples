using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";          // source PDF
        const string outputPdfPath = "output.pdf";        // result PDF
        const string attachmentFilePath = "attachment.pdf"; // PDF to embed

        // Verify required files exist
        if (!File.Exists(inputPdfPath) || !File.Exists(attachmentFilePath))
        {
            Console.Error.WriteLine("Input PDF or attachment file not found.");
            return;
        }

        // Load the source document (create‑load‑save lifecycle)
        using (Document doc = new Document(inputPdfPath))
        {
            // Embed the attachment into the PDF using EmbeddedFiles collection
            var fileSpec = new FileSpecification(attachmentFilePath, Path.GetFileName(attachmentFilePath));
            fileSpec.Contents = new MemoryStream(File.ReadAllBytes(attachmentFilePath));
            doc.EmbeddedFiles.Add(fileSpec);

            // Create a link annotation on the first page
            Page page = doc.Pages[1]; // 1‑based indexing
            // Rectangle: lower‑left X/Y, upper‑right X/Y
            Aspose.Pdf.Rectangle linkRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
            LinkAnnotation link = new LinkAnnotation(page, linkRect);

            // Hyperlink that points to the embedded file (by its name)
            link.Hyperlink = new FileHyperlink(Path.GetFileName(attachmentFilePath));

            // Optional visual styling
            link.Color = Aspose.Pdf.Color.Blue;
            // Border requires the parent annotation in its constructor
            link.Border = new Border(link) { Width = 1 };

            // Add the annotation to the page
            page.Annotations.Add(link);

            // Save the modified document
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Document saved with link annotation: '{outputPdfPath}'.");
    }
}
