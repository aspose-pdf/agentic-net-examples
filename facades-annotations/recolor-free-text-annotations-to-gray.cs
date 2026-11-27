using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_gray_free_text.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document using the high‑level Document API.
        Document pdfDoc = new Document(inputPath);

        // Iterate through all pages and their annotations.
        foreach (Page page in pdfDoc.Pages)
        {
            if (page.Annotations == null) continue;

            foreach (Annotation annot in page.Annotations)
            {
                // Process only Free Text annotations.
                if (annot is FreeTextAnnotation freeText)
                {
                    // Set the annotation's color to gray.
                    freeText.Color = Color.Gray;
                }
            }
        }

        // Save the modified PDF.
        pdfDoc.Save(outputPath);

        Console.WriteLine($"Free Text annotations recolored to gray and saved to '{outputPath}'.");
    }
}
