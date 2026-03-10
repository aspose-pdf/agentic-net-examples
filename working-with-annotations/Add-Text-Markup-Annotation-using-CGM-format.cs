using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string cgmPath   = "input.cgm";   // CGM input (input‑only format)
        const string outputPdf = "annotated_output.pdf";

        if (!File.Exists(cgmPath))
        {
            Console.Error.WriteLine($"File not found: {cgmPath}");
            return;
        }

        // Load the CGM file using the input‑only CgmLoadOptions
        using (Document doc = new Document(cgmPath, new CgmLoadOptions()))
        {
            // Ensure the document has at least one page
            if (doc.Pages.Count == 0)
            {
                Console.Error.WriteLine("The loaded document contains no pages.");
                return;
            }

            // Create a StrikeOut (text markup) annotation on the first page
            Page page = doc.Pages[1];

            // Define the annotation rectangle (coordinates are in points)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 520);

            // Instantiate the annotation and set its properties
            StrikeOutAnnotation strikeOut = new StrikeOutAnnotation(page, rect)
            {
                Color    = Aspose.Pdf.Color.Red,   // markup color
                Contents = "Strikeout annotation added via CGM load"
            };

            // Add the annotation to the page
            page.Annotations.Add(strikeOut);

            // Save the modified document as PDF (output format is PDF)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Annotated PDF saved to '{outputPdf}'.");
    }
}