using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF file path (used to obtain the PDF bytes for the example)
        const string inputPath = "input.pdf";
        // Output PDF file path where the version‑1.4 PDF will be saved
        const string outputPath = "output.pdf";
        // Temporary log file for the conversion process (required by Document.Convert)
        string conversionLogPath = Path.GetTempFileName();

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF file into a memory stream
        using (FileStream fileStream = new FileStream(inputPath, FileMode.Open, FileAccess.Read))
        using (MemoryStream memoryStream = new MemoryStream())
        {
            fileStream.CopyTo(memoryStream);
            memoryStream.Position = 0; // reset stream position for reading

            // Open the PDF document from the memory stream
            using (Document doc = new Document(memoryStream))
            {
                // Change the PDF version to 1.4 using Document.Convert
                doc.Convert(conversionLogPath, PdfFormat.v_1_4, ConvertErrorAction.Delete);

                // Save the document to the file system
                doc.Save(outputPath);
            }
        }

        Console.WriteLine($"PDF saved with version 1.4 to '{outputPath}'.");
    }
}
