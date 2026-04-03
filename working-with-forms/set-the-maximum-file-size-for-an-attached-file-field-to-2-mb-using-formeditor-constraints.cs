using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Set the global file size limit for loading files into memory (value in megabytes)
            Document.FileSizeLimitToMemoryLoading = 2; // 2 MB

            // Optional: use FormEditor if further form manipulation is required
            using (FormEditor editor = new FormEditor())
            {
                editor.BindPdf(doc);
                // No additional constraints needed for the attached file field in this scenario
                editor.Save(outputPath);
            }

            // If FormEditor is not used for saving, ensure the document is saved
            // doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with file size limit set to 2 MB: {outputPath}");
    }
}