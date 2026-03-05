using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Path to the source PDF file (input PDF)
        const string inputPath = "input.pdf";

        // Ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document using a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // -------------------------------------------------
            // 1. Save the document to a new PDF file on disk
            // -------------------------------------------------
            const string outputFilePath = "output_file.pdf";
            doc.Save(outputFilePath); // Save(string) – saves as PDF by default

            // -------------------------------------------------
            // 2. Save the document to a FileStream (stream output)
            // -------------------------------------------------
            const string outputStreamPath = "output_stream.pdf";
            using (FileStream fs = new FileStream(outputStreamPath, FileMode.Create, FileAccess.Write))
            {
                doc.Save(fs); // Save(Stream) – writes PDF bytes to the stream
            }

            // -------------------------------------------------
            // 3. Save the document to a memory buffer (MemoryStream)
            // -------------------------------------------------
            using (MemoryStream memory = new MemoryStream())
            {
                doc.Save(memory); // Save(Stream) – writes PDF into the memory buffer

                // Optionally, write the memory buffer to a file to verify the result
                const string outputMemoryPath = "output_memory.pdf";
                File.WriteAllBytes(outputMemoryPath, memory.ToArray());
            }
        }

        Console.WriteLine("Document saved to file, stream, and memory buffer successfully.");
    }
}