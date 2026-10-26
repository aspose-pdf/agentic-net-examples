using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main(string[] args)
    {
        // Input folder containing PDF files (default: "InputPdfs")
        string inputFolder = args.Length > 0 ? args[0] : "InputPdfs";
        // Output base folder where each PDF gets its own sub‑folder (default: "ExtractedVectors")
        string outputBaseFolder = args.Length > 1 ? args[1] : "ExtractedVectors";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        Directory.CreateDirectory(outputBaseFolder);

        // Process every PDF file in the input folder
        foreach (string pdfPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string pdfFileName = Path.GetFileNameWithoutExtension(pdfPath);
            string pdfOutputFolder = Path.Combine(outputBaseFolder, pdfFileName);
            Directory.CreateDirectory(pdfOutputFolder);

            try
            {
                // Load the PDF document inside a using block (deterministic disposal)
                using (Document doc = new Document(pdfPath))
                {
                    // Pages are 1‑based in Aspose.Pdf
                    for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
                    {
                        // Create a temporary document that contains only the current page
                        using (Document singlePageDoc = new Document())
                        {
                            singlePageDoc.Pages.Add(doc.Pages[pageIndex]);

                            // Build the SVG file name for this page
                            string svgPath = Path.Combine(pdfOutputFolder, $"Page_{pageIndex}.svg");

                            // Save the single‑page document as SVG (vector graphics)
                            // Explicitly pass SvgSaveOptions – otherwise a PDF would be written
                            try
                            {
                                singlePageDoc.Save(svgPath, new SvgSaveOptions());
                                Console.WriteLine($"Extracted SVG: {svgPath}");
                            }
                            catch (TypeInitializationException)
                            {
                                // SVG conversion relies on GDI+ and is Windows‑only
                                Console.WriteLine("SVG conversion requires Windows (GDI+). Skipping remaining pages.");
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        }

        Console.WriteLine("Vector graphic extraction completed.");
    }
}