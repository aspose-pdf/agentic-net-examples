using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Path to the source PDF file (for demonstration purposes)
        const string sourcePdfPath = "input.pdf";
        // Path where the resulting PDF will be saved
        const string outputPdfPath = "output_v1_4.pdf";

        // Ensure the source file exists
        if (!File.Exists(sourcePdfPath))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePdfPath}");
            return;
        }

        // Load the PDF into a memory stream
        using (MemoryStream ms = new MemoryStream(File.ReadAllBytes(sourcePdfPath)))
        {
            // Create a Document instance from the memory stream
            using (Document doc = new Document(ms))
            {
                // Convert the document to PDF version 1.4
                // A temporary log file is required by the Convert method
                string logPath = Path.GetTempFileName();
                doc.Convert(logPath, PdfFormat.v_1_4, ConvertErrorAction.Delete);

                // Save the converted document to the file system
                doc.Save(outputPdfPath);
            }
        }

        Console.WriteLine($"PDF saved with version 1.4 to '{outputPdfPath}'.");
    }
}