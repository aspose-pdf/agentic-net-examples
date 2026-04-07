using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputFilePath = "output_from_memory.pdf";

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
                pdfDoc.Save(ms);               // Save to stream (PDF format by default)
                ms.Position = 0;               // Reset stream position for reading

                // Write the memory stream contents to a file on disk
                using (FileStream file = new FileStream(outputFilePath, FileMode.Create, FileAccess.Write))
                {
                    ms.CopyTo(file);
                }
            }
        }

        Console.WriteLine($"PDF exported to memory and saved as '{outputFilePath}'.");
    }
}