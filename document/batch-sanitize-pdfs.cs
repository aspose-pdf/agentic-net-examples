using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Input folder containing PDFs to be sanitized
        const string inputFolder = "InputPdfs";
        // Output folder for cleaned PDFs
        const string outputFolder = "SanitizedPdfs";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Get all PDF files in the input folder (non‑recursive)
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf", SearchOption.TopDirectoryOnly);

        foreach (string inputPath in pdfFiles)
        {
            string fileName   = Path.GetFileNameWithoutExtension(inputPath);
            string outputPath = Path.Combine(outputFolder, $"{fileName}_sanitized.pdf");

            try
            {
                // Load each PDF inside a using block for deterministic disposal
                using (Document doc = new Document(inputPath))
                {
                    // ==== Remove document metadata ====
                    doc.Info.Title        = string.Empty;
                    doc.Info.Author       = string.Empty;
                    doc.Info.Subject      = string.Empty;
                    doc.Info.Keywords     = string.Empty;
                    doc.Info.Creator      = string.Empty;
                    doc.Info.Producer     = string.Empty;
                    doc.Info.ModDate      = DateTime.MinValue;
                    doc.Info.CreationDate = DateTime.MinValue;

                    // ==== Remove document‑level JavaScript (if any) ====
                    // The JavaScript property is read‑only; clear the OpenAction instead.
                    doc.OpenAction = null;

                    // ==== Remove embedded files (attachments) ====
                    if (doc.EmbeddedFiles != null && doc.EmbeddedFiles.Count > 0)
                    {
                        // EmbeddedFileCollection uses 1‑based indexing; delete by name.
                        for (int i = doc.EmbeddedFiles.Count; i >= 1; i--)
                        {
                            var fileSpec = doc.EmbeddedFiles[i];
                            if (fileSpec != null && !string.IsNullOrEmpty(fileSpec.Name))
                            {
                                doc.EmbeddedFiles.Delete(fileSpec.Name);
                            }
                        }
                    }

                    // ==== Remove page annotations (e.g., comments, links) ====
                    // Pages are 1‑based; use the global rule for correct indexing
                    for (int i = 1; i <= doc.Pages.Count; i++)
                    {
                        Page page = doc.Pages[i];
                        if (page.Annotations != null && page.Annotations.Count > 0)
                        {
                            page.Annotations.Clear();
                        }
                    }

                    // Save the sanitized PDF; default format is PDF
                    doc.Save(outputPath);
                }

                Console.WriteLine($"Sanitized: {outputPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to process '{inputPath}': {ex.Message}");
            }
        }
    }
}
