using System;
using System.IO;
using Aspose.Pdf; // Core Aspose.Pdf namespace

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";   // source PDF file
        const string outputFilePath = "output_from_memory.pdf"; // destination file

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Export the PDF to a memory stream
            using (MemoryStream ms = new MemoryStream())
            {
                pdfDoc.Save(ms);               // Save document into the stream (PDF format)
                ms.Position = 0;               // Reset stream position before reading

                // Write the memory stream contents to a physical file
                using (FileStream fileOut = new FileStream(outputFilePath, FileMode.Create, FileAccess.Write))
                {
                    ms.CopyTo(fileOut);
                }
            }
        }

        Console.WriteLine($"PDF exported to memory and saved as '{outputFilePath}'.");
    }
}