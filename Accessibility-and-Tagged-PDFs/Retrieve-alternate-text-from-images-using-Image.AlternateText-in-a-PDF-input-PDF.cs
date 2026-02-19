using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main(string[] args)
    {
        // Input and output PDF paths
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify that the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found – {inputPath}");
            return;
        }

        // Load the PDF document
        Document pdfDocument = new Document(inputPath);

        // Iterate through each page in the document
        foreach (Page page in pdfDocument.Pages)
        {
            // Each page may contain images in its Resources.Images collection
            foreach (XImage image in page.Resources.Images)
            {
                // Retrieve all alternate text entries for the image on this page
                var altTexts = image.GetAlternativeText(page);

                // Output the alternate text (if any) to the console
                foreach (string alt in altTexts)
                {
                    Console.WriteLine($"Page {page.Number}, Image \"{image.Name}\": Alt Text = \"{alt}\"");
                }
            }
        }

        // Save the (unchanged) document using the prescribed save rule
        pdfDocument.Save(outputPath);
    }
}