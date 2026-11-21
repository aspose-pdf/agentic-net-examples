using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Folder containing the source PDFs
        const string inputFolder = @"C:\PdfInput";
        // Folder where extracted SVG files will be saved
        const string outputFolder = @"C:\SvgOutput";

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Process each PDF file in the input folder
        foreach (string pdfPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(pdfPath))
            {
                // Iterate pages using 1‑based indexing (Aspose.Pdf requirement)
                for (int pageNumber = 1; pageNumber <= doc.Pages.Count; pageNumber++)
                {
                    // Build a unique SVG file name for each page
                    string svgFileName = $"{Path.GetFileNameWithoutExtension(pdfPath)}_page{pageNumber}.svg";
                    string svgPath = Path.Combine(outputFolder, svgFileName);

                    // Create a temporary document that contains only the current page
                    Document singlePageDoc = new Document();
                    // Add a copy of the page to the new document
                    singlePageDoc.Pages.Add(doc.Pages[pageNumber]);

                    // Configure SVG save options (no PageIndex property exists)
                    SvgSaveOptions svgOptions = new SvgSaveOptions();
                    // Optional: enable CSS style embedding by scaling to pixels
                    svgOptions.ScaleToPixels = true;

                    // Save the single‑page document as an SVG file
                    singlePageDoc.Save(svgPath, svgOptions);
                }
            }

            Console.WriteLine($"Processed: {Path.GetFileName(pdfPath)}");
        }

        Console.WriteLine("Vector graphic extraction completed.");
    }
}
