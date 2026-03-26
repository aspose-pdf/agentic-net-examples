using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        string sourceFolder = "input-pdfs";
        string targetFolder = "cleaned-pdfs";

        if (!Directory.Exists(sourceFolder))
        {
            Console.Error.WriteLine($"Source folder not found: {sourceFolder}");
            return;
        }

        Directory.CreateDirectory(targetFolder);
        string[] pdfFiles = Directory.GetFiles(sourceFolder, "*.pdf");

        string originalDirectory = Directory.GetCurrentDirectory();
        Directory.SetCurrentDirectory(targetFolder);

        foreach (string pdfPath in pdfFiles)
        {
            string fileName = Path.GetFileNameWithoutExtension(pdfPath);
            try
            {
                using (Document doc = new Document(pdfPath))
                {
                    // Remove metadata and compliance information
                    doc.RemoveMetadata();
                    doc.RemovePdfUaCompliance();
                    doc.RemovePdfaCompliance();

                    // Flatten form fields and optimize resources
                    doc.Flatten();
                    doc.OptimizeResources();

                    // Save cleaned PDF using a simple filename (no directory in the path)
                    doc.Save(fileName + ".pdf");
                }
                Console.WriteLine($"Cleaned: {fileName}.pdf");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to process {pdfPath}: {ex.Message}");
            }
        }

        Directory.SetCurrentDirectory(originalDirectory);
    }
}