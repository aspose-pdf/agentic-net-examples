using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades; // keep if other facades are needed elsewhere, not used for annotations here

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "annotated_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Work with the first page (pages are 1‑based in Aspose.Pdf)
            Page page = doc.Pages[1];

            // -----------------------------------------------------------------
            // 1. Add a simple text (sticky‑note) annotation on page 1
            // -----------------------------------------------------------------
            // Rectangle coordinates: lower‑left X, lower‑Y, upper‑right X, upper‑Y
            Aspose.Pdf.Rectangle textRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
            TextAnnotation textAnn = new TextAnnotation(page, textRect)
            {
                Contents = "This is a sample text annotation.",
                Title = "Sample Note",
                Open = true,               // initially open
                Color = Color.Yellow,
                Name = "Note1"
            };
            page.Annotations.Add(textAnn);

            // -----------------------------------------------------------------
            // 2. Add a web‑link annotation on page 1
            // -----------------------------------------------------------------
            Aspose.Pdf.Rectangle linkRect = new Aspose.Pdf.Rectangle(350, 500, 550, 550);
            LinkAnnotation linkAnn = new LinkAnnotation(page, linkRect)
            {
                Action = new GoToURIAction("https://www.example.com"),
                Color = Color.Blue // optional visual cue
            };
            page.Annotations.Add(linkAnn);

            // -----------------------------------------------------------------
            // 3. Add a file‑attachment annotation on page 1
            // -----------------------------------------------------------------
            const string attachmentPath = "sample_attachment.txt";
            if (File.Exists(attachmentPath))
            {
                Aspose.Pdf.Rectangle attachRect = new Aspose.Pdf.Rectangle(600, 500, 800, 550);
                // Create a FileSpecification that points to the external file
                FileSpecification fileSpec = new FileSpecification(attachmentPath);
                FileAttachmentAnnotation fileAnn = new FileAttachmentAnnotation(page, attachRect, fileSpec)
                {
                    Title = "sample_attachment.txt",
                    Contents = "Attached sample file"
                };
                page.Annotations.Add(fileAnn);
            }

            // Save the modified document directly (no need for PdfAnnotationEditor)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Annotations added and saved to '{outputPath}'.");
    }
}
