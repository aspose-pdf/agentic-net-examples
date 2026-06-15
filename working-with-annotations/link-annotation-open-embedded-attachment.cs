using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";      // existing PDF or create a new one
        const string attachment = "sample.docx";    // file to embed
        const string outputPdf  = "output.pdf";

        // Ensure the attachment file exists
        if (!File.Exists(attachment))
        {
            Console.Error.WriteLine($"Attachment not found: {attachment}");
            return;
        }

        // Load (or create) the PDF document
        using (Document doc = File.Exists(inputPdf) ? new Document(inputPdf) : new Document())
        {
            // If the document is empty, add a blank page
            if (doc.Pages.Count == 0)
                doc.Pages.Add();

            // ------------------------------------------------------------
            // Embed the external file into the PDF
            // ------------------------------------------------------------
            var fileSpec = new FileSpecification(attachment, "Embedded attachment");
            using (FileStream fs = File.OpenRead(attachment))
            {
                fileSpec.Contents = fs;               // attach the file stream
                doc.EmbeddedFiles.Add(fileSpec);      // add to the document's EmbeddedFiles collection
            }

            // ------------------------------------------------------------
            // Create a link annotation that opens the embedded file
            // ------------------------------------------------------------
            Page page = doc.Pages[1]; // first page (1‑based indexing)

            // Define the clickable rectangle (coordinates: llx, lly, urx, ury)
            Aspose.Pdf.Rectangle linkRect = new Aspose.Pdf.Rectangle(100, 500, 250, 550);

            // Create the link annotation on the specified page
            LinkAnnotation link = new LinkAnnotation(page, linkRect)
            {
                Color = Aspose.Pdf.Color.Blue,                     // visual border color
                Contents = "Open attached file",                    // tooltip text
                // Use a FileHyperlink to reference the embedded file by its name
                Hyperlink = new FileHyperlink(attachment)
            };

            // Optional: set a visible border around the link area
            link.Border = new Border(link) { Width = 1 };

            // Add the annotation to the page
            page.Annotations.Add(link);

            // ------------------------------------------------------------
            // Save the modified PDF
            // ------------------------------------------------------------
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF with embedded attachment and link saved to '{outputPdf}'.");
    }
}