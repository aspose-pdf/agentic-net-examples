using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Get all PDF files in the current directory
        string[] pdfFiles = Directory.GetFiles(".", "*.pdf");
        foreach (string inputPath in pdfFiles)
        {
            // Prepare output file name (simple filename, no directory path)
            string fileName = Path.GetFileName(inputPath);
            string outputPath = "updated_" + fileName;

            // Load the PDF document
            using (Document doc = new Document(inputPath))
            {
                // Access and modify custom metadata
                using (PdfFileInfo pdfInfo = new PdfFileInfo(doc))
                {
                    pdfInfo.SetMetaInfo("Department", "Finance");
                    // Save the PDF with the new metadata
                    pdfInfo.SaveNewInfo(outputPath);
                }
            }
            Console.WriteLine($"Processed {fileName} -> {outputPath}");
        }
    }
}