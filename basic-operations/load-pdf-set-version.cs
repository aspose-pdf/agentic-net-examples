using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string sourcePath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(sourcePath))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePath}");
            return;
        }

        // Load the PDF bytes into a MemoryStream
        byte[] pdfBytes = File.ReadAllBytes(sourcePath);
        using (MemoryStream ms = new MemoryStream(pdfBytes))
        {
            // Load the document from the stream
            using (Document doc = new Document(ms))
            {
                // Convert the document to PDF version 1.4 using the Convert method
                string logPath = Path.Combine(Path.GetTempPath(), "convert_log.xml");
                doc.Convert(logPath, PdfFormat.v_1_4, ConvertErrorAction.Delete);

                // Save the converted document to the file system
                doc.Save(outputPath);
            }
        }

        Console.WriteLine($"PDF saved with version 1.4 to '{outputPath}'.");
    }
}
