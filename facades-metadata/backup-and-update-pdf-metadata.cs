using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Folder containing PDFs to process
        const string inputFolder = "InputPdfs";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Folder not found: {inputFolder}");
            return;
        }

        // Process each PDF file in the folder
        foreach (string pdfPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            try
            {
                // Create a backup copy before any modification
                string backupPath = Path.Combine(
                    Path.GetDirectoryName(pdfPath),
                    Path.GetFileNameWithoutExtension(pdfPath) + "_backup.pdf");

                File.Copy(pdfPath, backupPath, true);
                Console.WriteLine($"Backup created: {backupPath}");

                // Open the PDF with PdfFileInfo facade
                using (PdfFileInfo pdfInfo = new PdfFileInfo(pdfPath))
                {
                    // Modify desired metadata properties
                    pdfInfo.Title = "Updated Title";
                    pdfInfo.Author = "Updated Author";
                    pdfInfo.Subject = "Updated Subject";
                    pdfInfo.Keywords = "Aspose, PDF, Metadata";

                    // Save the updated metadata back to the original file
                    bool saved = pdfInfo.SaveNewInfo(pdfPath);
                    if (saved)
                    {
                        Console.WriteLine($"Metadata updated: {pdfPath}");
                    }
                    else
                    {
                        Console.Error.WriteLine($"Failed to save metadata for: {pdfPath}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        }
    }
}