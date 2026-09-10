using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "sample.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block to ensure disposal
        using (Document doc = new Document(inputPath))
        {
            // Example operation: display page count
            Console.WriteLine($"Page count: {doc.Pages.Count}");

            // Use PdfFileInfo to retrieve file-level information
            PdfFileInfo fileInfo = new PdfFileInfo(inputPath);
            try
            {
                // Example: check if the PDF is encrypted
                Console.WriteLine($"Is encrypted: {fileInfo.IsEncrypted}");
            }
            finally
            {
                // Explicitly close PdfFileInfo (it does not implement IDisposable)
                fileInfo.Close();
            }

            // Save the document (could be the same or a different file)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Document saved to '{outputPath}'.");
    }
}