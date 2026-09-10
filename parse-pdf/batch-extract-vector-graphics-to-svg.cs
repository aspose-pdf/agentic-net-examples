using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Folder containing PDF files to process
        const string inputFolder = @"C:\InputPdfs";
        // Folder where extracted SVG files will be saved
        const string outputFolder = @"C:\ExtractedSvgs";

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Get all PDF files in the input folder (non‑recursive)
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf", SearchOption.TopDirectoryOnly);

        foreach (string pdfPath in pdfFiles)
        {
            // Use a using block for deterministic disposal of the Document
            using (Document doc = new Document(pdfPath))
            {
                // Iterate pages using 1‑based indexing (Aspose.Pdf requirement)
                for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
                {
                    Page page = doc.Pages[pageIndex];

                    // Check if the page contains vector graphics
                    if (page.HasVectorGraphics())
                    {
                        // Build a unique SVG file name: <pdfname>_page<index>.svg
                        string svgFileName = $"{Path.GetFileNameWithoutExtension(pdfPath)}_page{pageIndex}.svg";
                        string svgPath = Path.Combine(outputFolder, svgFileName);

                        // Extract the vector graphics to the SVG file
                        // TrySaveVectorGraphics returns true if graphics were saved; we ignore the return value here
                        page.TrySaveVectorGraphics(svgPath);
                    }
                }
            }

            Console.WriteLine($"Processed: {Path.GetFileName(pdfPath)}");
        }

        Console.WriteLine("Vector graphic extraction completed.");
    }
}