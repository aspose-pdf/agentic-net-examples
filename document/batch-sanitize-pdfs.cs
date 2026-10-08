using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class PdfSanitizer
{
    static void Main()
    {
        // Input and output directories
        const string inputFolder  = @"C:\InputPdfs";
        const string outputFolder = @"C:\SanitizedPdfs";

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Process each PDF file in the input folder
        foreach (string inputPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            try
            {
                // Determine output file path
                string fileName   = Path.GetFileName(inputPath);
                string outputPath = Path.Combine(outputFolder, fileName);

                // Load the PDF inside a using block for deterministic disposal
                using (Document doc = new Document(inputPath))
                {
                    // ---------- Sanitize metadata ----------
                    doc.Info.Title        = string.Empty;
                    doc.Info.Author       = string.Empty;
                    doc.Info.Subject      = string.Empty;
                    doc.Info.Keywords     = string.Empty;
                    doc.Info.Creator      = string.Empty;
                    doc.Info.Producer     = string.Empty;
                    doc.Info.ModDate      = DateTime.Now;
                    doc.Info.CreationDate = DateTime.Now;

                    // ---------- Remove all annotations ----------
                    // Pages are 1‑based in Aspose.Pdf
                    for (int i = 1; i <= doc.Pages.Count; i++)
                    {
                        Page page = doc.Pages[i];
                        // Clear the annotations collection if it contains any
                        if (page.Annotations != null && page.Annotations.Count > 0)
                        {
                            page.Annotations.Clear();
                        }
                    }

                    // Save the sanitized PDF to the output folder
                    doc.Save(outputPath);
                }

                Console.WriteLine($"Sanitized: {fileName}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{inputPath}': {ex.Message}");
            }
        }

        Console.WriteLine("Batch sanitization completed.");
    }
}