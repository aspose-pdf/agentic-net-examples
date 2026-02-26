using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output_strikeout.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Choose the page where the strike‑out will be placed (first page in this example)
            Page page = doc.Pages[1];

            // Define the rectangle that covers the word(s) to be struck out.
            // Use the fully qualified Aspose.Pdf.Rectangle to avoid ambiguity with System.Drawing.
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 520);

            // Create the StrikeOutAnnotation on the selected page and rectangle
            StrikeOutAnnotation strikeOut = new StrikeOutAnnotation(page, rect)
            {
                // Optional: set the color of the strike‑out line
                Color = Aspose.Pdf.Color.Red,
                // Optional: add a comment that appears in the annotation popup
                Contents = "Struck out for review"
            };

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(strikeOut);

            // Save the modified document as PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Strike‑out annotation added. Saved to '{outputPdf}'.");
    }
}