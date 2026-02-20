using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main(string[] args)
    {
        // Directory containing PDF files; can be passed as a command‑line argument
        string inputFolder = args.Length > 0 ? args[0] : "InputPdfs";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Error: Folder not found – {inputFolder}");
            return;
        }

        // Get all PDF files in the folder
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf");
        if (pdfFiles.Length == 0)
        {
            Console.WriteLine("No PDF files found.");
            return;
        }

        foreach (string pdfPath in pdfFiles)
        {
            if (!File.Exists(pdfPath))
            {
                Console.Error.WriteLine($"File not found: {pdfPath}");
                continue;
            }

            try
            {
                // Load each PDF using the Aspose.Pdf.Document class (cross‑platform)
                using (Document pdfDoc = new Document(pdfPath))
                {
                    int pageCount = pdfDoc.Pages.Count;
                    Console.WriteLine($"Loaded '{Path.GetFileName(pdfPath)}' – Pages: {pageCount}");
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to process '{pdfPath}': {ex.Message}");
            }
        }
    }
}
