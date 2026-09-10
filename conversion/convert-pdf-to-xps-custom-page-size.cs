using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.xps";

        // Custom page size (width x height) in points (1 point = 1/72 inch)
        // Example: 842 x 595 points = A4 landscape
        double customWidth = 842; // points
        double customHeight = 595; // points

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block (ensures disposal)
        using (Document pdfDoc = new Document(inputPath))
        {
            // Apply custom size and orientation to each page
            foreach (Page page in pdfDoc.Pages)
            {
                page.SetPageSize(customWidth, customHeight);
                // Set orientation to landscape via PageInfo
                page.PageInfo.IsLandscape = true;
            }

            // Initialize XPS save options (options live in the Aspose.Pdf namespace)
            var xpsOptions = new XpsSaveOptions();

            // Save the document as XPS using the save options
            pdfDoc.Save(outputPath, xpsOptions);
        }

        Console.WriteLine($"PDF successfully converted to XPS: {outputPath}");
    }
}