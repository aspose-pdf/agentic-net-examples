using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Base directory of the application (works for both console and other hosts)
        string baseDir = AppDomain.CurrentDomain.BaseDirectory;

        // Resolve input and output folders – fall back to the current working directory if the expected folders are missing.
        string inputFolder = Path.Combine(baseDir, "folderF");
        if (!Directory.Exists(inputFolder))
        {
            Console.WriteLine($"[Warning] Input folder '{inputFolder}' not found. Falling back to current directory.");
            inputFolder = Directory.GetCurrentDirectory();
        }

        string outputFolder = Path.Combine(baseDir, "folderF_PDFX");
        Directory.CreateDirectory(outputFolder); // Guarantees the folder exists.

        // Process each PDF file in the resolved input folder.
        foreach (string pdfPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            if (!File.Exists(pdfPath))
            {
                Console.WriteLine($"[Warning] File not found (skipped): {pdfPath}");
                continue;
            }

            string fileNameWithoutExt = Path.GetFileNameWithoutExtension(pdfPath);
            string outputPath = Path.Combine(outputFolder, fileNameWithoutExt + "_pdfx.pdf");
            string logPath = Path.Combine(outputFolder, fileNameWithoutExt + "_conversion.log");

            try
            {
                // Load the source PDF. The using statement ensures deterministic disposal.
                using (Document doc = new Document(pdfPath))
                {
                    // Convert to PDF/X‑1a:2003. No external ICC profile is supplied – the API uses the default profile.
                    doc.Convert(logPath, PdfFormat.PDF_X_1A, ConvertErrorAction.Delete);

                    // Save the converted document.
                    doc.Save(outputPath);
                }

                Console.WriteLine($"Converted '{pdfPath}' → '{outputPath}'");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[Error] Failed to convert '{pdfPath}'. Reason: {ex.Message}");
            }
        }
    }
}
