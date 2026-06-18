using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Define source and output folders
        string sourceFolder = "source";
        string outputFolder = "output";

        // Ensure the folders exist
        Directory.CreateDirectory(sourceFolder);
        Directory.CreateDirectory(outputFolder);

        // Create a few sample PDFs (evaluation mode limits collections to 4 items)
        for (int i = 1; i <= 3; i++)
        {
            string samplePath = Path.Combine(sourceFolder, "sample" + i + ".pdf");
            using (Document doc = new Document())
            {
                // Add four pages to each sample PDF (evaluation mode limit)
                for (int p = 0; p < 4; p++)
                {
                    doc.Pages.Add();
                }
                doc.Save(samplePath);
            }
        }

        // Get all PDF files from the source folder
        string[] pdfFiles = Directory.GetFiles(sourceFolder, "*.pdf");
        foreach (string inputFile in pdfFiles)
        {
            // Extract only the file name for the output
            string outputFileName = Path.GetFileName(inputFile);
            string outputPath = Path.Combine(outputFolder, outputFileName);

            // Delete pages 3 and 4 using PdfFileEditor
            PdfFileEditor pdfEditor = new PdfFileEditor();
            pdfEditor.Delete(inputFile, new int[] { 3, 4 }, outputPath);
        }
    }
}
