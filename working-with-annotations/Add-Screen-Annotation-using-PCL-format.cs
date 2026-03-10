using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output_with_screen.pdf";
        const string mediaFile = "sample.pcl"; // Path to the PCL media file

        // Verify input files exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(mediaFile))
        {
            Console.Error.WriteLine($"Media file not found: {mediaFile}");
            return;
        }

        // Load the existing PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Access the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Define the rectangle where the annotation will appear
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 600);

            // Create a ScreenAnnotation that references the PCL file
            ScreenAnnotation screen = new ScreenAnnotation(page, rect, mediaFile)
            {
                Title = "PCL Media",
                Contents = "Click to view PCL content."
            };

            // Add the annotation to the page
            page.Annotations.Add(screen);

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved with ScreenAnnotation: {outputPdf}");
    }
}