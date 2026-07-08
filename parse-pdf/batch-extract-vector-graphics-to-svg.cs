using System;
using System.IO;
using Aspose.Pdf;               // Core PDF API
using Aspose.Pdf.Vector;        // SvgExtractor and related vector graphics API

class BatchVectorGraphicsExtractor
{
    static void Main()
    {
        // Folder containing PDF files to process
        const string inputFolder = @"C:\InputPdfs";

        // Folder where all extracted SVG files will be saved
        const string outputFolder = @"C:\ExtractedSvgs";

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Process each PDF file in the input folder
        foreach (string pdfPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            // Use a using block to guarantee deterministic disposal of the Document
            using (Document pdfDoc = new Document(pdfPath))
            {
                // Iterate pages using 1‑based indexing (Aspose.Pdf requirement)
                for (int pageIndex = 1; pageIndex <= pdfDoc.Pages.Count; pageIndex++)
                {
                    Page page = pdfDoc.Pages[pageIndex];

                    // Check if the page contains vector graphics
                    if (page.HasVectorGraphics())
                    {
                        // Build a unique SVG file name: <pdfname>_page<index>.svg
                        string svgFileName = $"{Path.GetFileNameWithoutExtension(pdfPath)}_page{pageIndex}.svg";
                        string svgPath = Path.Combine(outputFolder, svgFileName);

                        // Try to save the vector graphics of the page as an SVG file.
                        // The method returns true if graphics were saved; false otherwise.
                        page.TrySaveVectorGraphics(svgPath);
                    }
                }
            }

            Console.WriteLine($"Processed: {Path.GetFileName(pdfPath)}");
        }

        Console.WriteLine("Vector graphics extraction completed.");
    }
}