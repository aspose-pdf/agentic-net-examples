using System;
using System.IO;
using Aspose.Pdf;   // Core Aspose.Pdf namespace

class SavePdfExamples
{
    static void Main()
    {
        // Input PDF file path (must exist)
        const string inputPdfPath = "input.pdf";

        // Verify the input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document (using a using block for deterministic disposal)
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // -------------------------------------------------
            // 1. Save the document to a physical file (PDF format)
            // -------------------------------------------------
            const string outputFilePath = "output_file.pdf";
            pdfDoc.Save(outputFilePath);   // Save(string) writes PDF regardless of extension
            Console.WriteLine($"Document saved to file: {outputFilePath}");

            // -------------------------------------------------
            // 2. Save the document to a FileStream (PDF format)
            // -------------------------------------------------
            const string outputStreamPath = "output_stream.pdf";
            using (FileStream fileStream = new FileStream(outputStreamPath, FileMode.Create, FileAccess.Write))
            {
                pdfDoc.Save(fileStream);   // Save(Stream) writes PDF to the provided stream
            }
            Console.WriteLine($"Document saved to stream (file): {outputStreamPath}");

            // -------------------------------------------------
            // 3. Save the document to a memory buffer (MemoryStream)
            // -------------------------------------------------
            using (MemoryStream memoryStream = new MemoryStream())
            {
                pdfDoc.Save(memoryStream); // Save(Stream) writes PDF into the memory buffer

                // Optionally retrieve the raw PDF bytes from the memory buffer
                byte[] pdfBytes = memoryStream.ToArray();

                // Example: write the bytes to another file to verify the in‑memory save
                const string outputMemoryPath = "output_memory.pdf";
                File.WriteAllBytes(outputMemoryPath, pdfBytes);
                Console.WriteLine($"Document saved to memory buffer and written to file: {outputMemoryPath}");
            }
        }

        // All resources (Document, streams) are disposed automatically by the using statements.
        Console.WriteLine("All save operations completed successfully.");
    }
}