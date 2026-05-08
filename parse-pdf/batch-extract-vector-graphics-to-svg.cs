using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Folder containing source PDF files
        const string inputFolder = "InputPdfs";
        // Folder where extracted SVG files will be placed
        const string outputFolder = "ExtractedSvgs";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Get all PDF files in the input folder (non‑recursive)
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf", SearchOption.TopDirectoryOnly);

        foreach (string pdfPath in pdfFiles)
        {
            try
            {
                // Load each PDF using the standard Document constructor (lifecycle rule)
                using (Document doc = new Document(pdfPath))
                {
                    // Iterate pages using 1‑based indexing (global rule)
                    for (int i = 1; i <= doc.Pages.Count; i++)
                    {
                        Page page = doc.Pages[i];

                        // Check if the page contains vector graphics (Page.HasVectorGraphics)
                        if (page.HasVectorGraphics())
                        {
                            // Build a unique SVG file name per PDF page
                            string pdfName = Path.GetFileNameWithoutExtension(pdfPath);
                            string svgPath = Path.Combine(outputFolder, $"{pdfName}_page{i}.svg");

                            // Extract vector graphics to SVG (Page.TrySaveVectorGraphics)
                            bool saved = page.TrySaveVectorGraphics(svgPath);

                            if (saved)
                                Console.WriteLine($"Extracted SVG: {svgPath}");
                            else
                                Console.WriteLine($"No vector graphics saved for {pdfName} page {i}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        }

        Console.WriteLine("Batch SVG extraction completed.");
    }
}