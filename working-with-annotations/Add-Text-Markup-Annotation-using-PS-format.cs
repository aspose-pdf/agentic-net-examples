using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "annotated.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle: create & load)
        using (Document doc = new Document(inputPath))
        {
            // Access the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Define the annotation rectangle (left, bottom, right, top)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 520);

            // Create a TextMarkupAnnotation – here a StrikeOutAnnotation as an example
            StrikeOutAnnotation strike = new StrikeOutAnnotation(page, rect)
            {
                Color    = Aspose.Pdf.Color.Red,          // annotation color
                Contents = "Strikeout annotation example" // popup text
            };

            // Add the annotation to the page
            page.Annotations.Add(strike);

            // NOTE: PostScript (PS) format is input‑only in Aspose.Pdf and cannot be used for saving.
            // Therefore we save the modified document as PDF.
            doc.Save(outputPath); // lifecycle: save
        }

        Console.WriteLine($"Annotated PDF saved to '{outputPath}'.");
    }
}