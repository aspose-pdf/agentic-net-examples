using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Use the executable's folder as the base so the sample works out‑of‑the‑box
        string baseDir = AppDomain.CurrentDomain.BaseDirectory;

        // Input / output folders – they will be created if they do not exist
        string pdfFolder   = Path.Combine(baseDir, "pdfs");
        string xfdfFolder  = Path.Combine(baseDir, "xfdfs");
        string outputFolder = Path.Combine(baseDir, "output");

        Directory.CreateDirectory(pdfFolder);
        Directory.CreateDirectory(xfdfFolder);
        Directory.CreateDirectory(outputFolder);

        // Get all PDF files in the input folder
        string[] pdfFiles = Directory.GetFiles(pdfFolder, "*.pdf");
        if (pdfFiles.Length == 0)
        {
            Console.WriteLine($"No PDF files found in '{pdfFolder}'. Place PDFs there and rerun.");
            return;
        }

        foreach (string pdfPath in pdfFiles)
        {
            string baseName = Path.GetFileNameWithoutExtension(pdfPath);
            string xfdfPath = Path.Combine(xfdfFolder, baseName + ".xfdf");

            if (!File.Exists(xfdfPath))
            {
                Console.WriteLine($"No matching XFDF for '{pdfPath}'. Skipping.");
                continue;
            }

            string outputPath = Path.Combine(outputFolder, baseName + "_annotated.pdf");

            try
            {
                // Load the PDF, import annotations from the XFDF file and save the result
                using (Document doc = new Document(pdfPath))
                {
                    doc.ImportAnnotationsFromXfdf(xfdfPath);
                    doc.Save(outputPath);
                }

                Console.WriteLine($"Annotated PDF saved: {outputPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        }
    }
}
