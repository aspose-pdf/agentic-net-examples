using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // CMYK components (range 0.0 – 1.0) for the desired background color
        double cyan = 0.0;
        double magenta = 0.5;
        double yellow = 0.5;
        double black = 0.0;

        // If the source PDF does not exist, create a simple one with a single page
        if (!File.Exists(inputPath))
        {
            using (Document tempDoc = new Document())
            {
                tempDoc.Pages.Add();
                tempDoc.Save(inputPath);
            }
        }

        // Load the PDF, set the background color on each page, and save the result
        using (Document doc = new Document(inputPath))
        {
            foreach (Page page in doc.Pages)
            {
                page.Background = Aspose.Pdf.Color.FromCmyk(cyan, magenta, yellow, black);
            }

            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with CMYK background to '{outputPath}'.");
    }
}