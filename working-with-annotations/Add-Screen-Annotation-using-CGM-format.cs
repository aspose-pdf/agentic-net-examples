using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // source PDF
        const string outputPdf = "output_with_screen.pdf"; // destination PDF
        const string cgmFile   = "media.cgm";          // CGM file to be used as media

        // Verify that required files exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(cgmFile))
        {
            Console.Error.WriteLine($"CGM media file not found: {cgmFile}");
            return;
        }

        // Load the existing PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPdf))
        {
            // Select the page where the annotation will be placed (1‑based indexing)
            Page page = doc.Pages[1];

            // Define the annotation rectangle (coordinates in points)
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 700);

            // Create a ScreenAnnotation that references the CGM file
            ScreenAnnotation screen = new ScreenAnnotation(page, rect, cgmFile)
            {
                Title    = "CGM Media",                     // optional title
                Contents = "Click to view the CGM content." // optional description
            };

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(screen);

            // Save the modified PDF (PDF format, no special SaveOptions needed)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Screen annotation added. Saved to '{outputPdf}'.");
    }
}