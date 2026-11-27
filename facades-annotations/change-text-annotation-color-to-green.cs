using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF with the high‑level Document API (PdfContentEditor does not expose page access)
        Document pdf = new Document(inputPath);

        // Retrieve the page that contains the annotation (example: first page). Pages are 1‑based.
        Page page = pdf.Pages[1];

        // Locate the specific Text annotation and change its color to green
        foreach (Annotation annot in page.Annotations)
        {
            if (annot is TextAnnotation textAnnot)
            {
                // Use Aspose.Pdf.Color for cross‑platform compatibility
                textAnnot.Color = Aspose.Pdf.Color.Green;
                break; // stop after modifying the first matching annotation
            }
        }

        // Save the modified PDF
        pdf.Save(outputPath);

        Console.WriteLine($"Annotation color changed to green and saved to '{outputPath}'.");
    }
}
