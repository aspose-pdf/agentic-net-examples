using System;
using System.IO;
using Aspose.Pdf;               // All SaveOptions classes are in this namespace

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputSvgPath = "first_page.svg";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Load the source PDF
            using (Document srcDoc = new Document(inputPdfPath))
            {
                // Create a new PDF containing only the first page
                using (Document singlePageDoc = new Document())
                {
                    // Aspose.Pdf uses 1‑based page indexing
                    singlePageDoc.Pages.Add(srcDoc.Pages[1]);

                    // Save the single‑page PDF as SVG
                    SvgSaveOptions svgOptions = new SvgSaveOptions();
                    singlePageDoc.Save(outputSvgPath, svgOptions);
                }
            }

            Console.WriteLine($"First page saved as SVG to '{outputSvgPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}