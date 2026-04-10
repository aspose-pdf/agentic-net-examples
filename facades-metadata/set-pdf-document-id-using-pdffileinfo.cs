using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using)
        using (Document doc = new Document(inputPath))
        {
            // Initialize PdfFileInfo facade on the loaded document
            using (PdfFileInfo fileInfo = new PdfFileInfo(doc))
            {
                // Generate a new GUID
                Guid guid = Guid.NewGuid();

                // Store the GUID as custom metadata (Document ID)
                fileInfo.SetMetaInfo("DocumentID", guid.ToString());

                // Save the updated PDF to a new file
                fileInfo.SaveNewInfo(outputPath);
            }
        }

        Console.WriteLine("PDF Document ID set successfully.");
    }
}