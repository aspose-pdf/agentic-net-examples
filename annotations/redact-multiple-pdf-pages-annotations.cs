using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "redacted_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // ---- Add a redaction on page 1 ----
            // Pages are 1‑based in Aspose.Pdf
            Page page1 = doc.Pages[1];
            // Define the area to be redacted (coordinates in points)
            Aspose.Pdf.Rectangle rect1 = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
            // Create the annotation, set fill color (optional) and add to the page
            RedactionAnnotation redaction1 = new RedactionAnnotation(page1, rect1)
            {
                FillColor = Aspose.Pdf.Color.Red
            };
            page1.Annotations.Add(redaction1);

            // ---- Add a redaction on page 2 (if it exists) ----
            if (doc.Pages.Count >= 2)
            {
                Page page2 = doc.Pages[2];
                Aspose.Pdf.Rectangle rect2 = new Aspose.Pdf.Rectangle(50, 400, 250, 450);
                RedactionAnnotation redaction2 = new RedactionAnnotation(page2, rect2)
                {
                    FillColor = Aspose.Pdf.Color.Black
                };
                page2.Annotations.Add(redaction2);
            }

            // Apply all redaction annotations manually (Document.Redact() is not available in this version)
            foreach (Page page in doc.Pages)
            {
                foreach (Annotation annot in page.Annotations)
                {
                    if (annot is RedactionAnnotation redAnnot)
                    {
                        redAnnot.Redact();
                    }
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Redacted PDF saved to '{outputPath}'.");
    }
}
