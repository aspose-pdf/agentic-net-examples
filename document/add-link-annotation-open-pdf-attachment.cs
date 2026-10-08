using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string attachmentPath = "attachment.pdf";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf) || !File.Exists(attachmentPath))
        {
            Console.Error.WriteLine("Input PDF or attachment file not found.");
            return;
        }

        // Load the source PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Embed the PDF attachment into the document using the EmbeddedFiles collection
            var fileSpec = new FileSpecification(Path.GetFileName(attachmentPath))
            {
                Contents = new MemoryStream(File.ReadAllBytes(attachmentPath))
            };
            doc.EmbeddedFiles.Add(fileSpec);

            // Define the clickable area (lower‑left x, lower‑left y, upper‑right x, upper‑right y)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 520);

            // Create a link annotation that will launch the attachment file when clicked
            LinkAnnotation link = new LinkAnnotation(doc.Pages[1], rect);
            // LaunchAction expects a string (file path). Use the attachment path directly.
            link.Action = new LaunchAction(attachmentPath);

            // Optional: make the link invisible (no border, transparent)
            link.Border = new Border(link) { Width = 0 };
            link.Color = Aspose.Pdf.Color.Transparent;

            // Add the annotation to the first page (adjust page index as needed)
            doc.Pages[1].Annotations.Add(link);

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF with link annotation saved to '{outputPdf}'.");
    }
}
