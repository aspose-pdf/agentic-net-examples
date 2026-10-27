using System;
using System.IO;
using Aspose.Pdf;               // Core Aspose.Pdf namespace
using Aspose.Pdf;               // SvgSaveOptions resides here

class Program
{
    static void Main()
    {
        // Folder containing the source PDF files
        const string inputFolder = @"C:\PdfInput";

        // Folder where extracted SVG files will be saved
        const string outputFolder = @"C:\SvgOutput";

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Process each PDF file in the input folder
        foreach (string pdfPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            // Load the PDF document
            using (Document srcDoc = new Document(pdfPath))
            {
                // Iterate through all pages (Aspose.Pdf uses 1‑based indexing)
                for (int pageIndex = 1; pageIndex <= srcDoc.Pages.Count; pageIndex++)
                {
                    // Create a temporary document that contains only the current page
                    using (Document singlePageDoc = new Document())
                    {
                        // Add the page to the new document (this creates a copy of the page)
                        singlePageDoc.Pages.Add(srcDoc.Pages[pageIndex]);

                        // Prepare SVG save options (no additional configuration required for vector graphics)
                        SvgSaveOptions svgOptions = new SvgSaveOptions();

                        // Build the output SVG file name: original name + page number
                        string svgFileName = $"{Path.GetFileNameWithoutExtension(pdfPath)}_page{pageIndex}.svg";
                        string svgPath = Path.Combine(outputFolder, svgFileName);

                        // Save the single‑page document as SVG
                        singlePageDoc.Save(svgPath, svgOptions);
                    }
                }
            }

            Console.WriteLine($"Extracted SVGs from '{Path.GetFileName(pdfPath)}' to '{outputFolder}'.");
        }

        Console.WriteLine("Batch processing completed.");
    }
}