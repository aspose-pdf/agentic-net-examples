using System;
using System.IO;
using Aspose.Pdf; // Core Aspose.Pdf namespace

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";   // source PDF file
        const string outputFilePath = "output_from_stream.pdf"; // destination file

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Export the PDF to a memory stream
            using (MemoryStream ms = new MemoryStream())
            {
                // Save the document into the memory stream (PDF format is default)
                pdfDoc.Save(ms);

                // Ensure the stream position is at the beginning before writing out
                ms.Position = 0;

                // Write the memory stream contents to a file on disk
                using (FileStream fileOut = new FileStream(outputFilePath, FileMode.Create, FileAccess.Write))
                {
                    ms.CopyTo(fileOut);
                }
            }
        }

        Console.WriteLine($"PDF exported to memory stream and saved as '{outputFilePath}'.");
    }
}