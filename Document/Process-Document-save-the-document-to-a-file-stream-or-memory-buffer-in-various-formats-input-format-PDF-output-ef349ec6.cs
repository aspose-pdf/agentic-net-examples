using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputFilePath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // 1. Load PDF and save to another file (PDF format)
        using (Document doc = new Document(inputPath))
        {
            doc.Save(outputFilePath); // Saves as PDF regardless of extension
        }

        // 2. Load PDF and save to a FileStream (PDF format)
        using (Document doc = new Document(inputPath))
        using (FileStream fileStream = new FileStream("output_stream.pdf", FileMode.Create, FileAccess.Write))
        {
            doc.Save(fileStream); // Writes PDF into the stream
        }

        // 3. Load PDF and save to a MemoryStream (PDF format)
        using (Document doc = new Document(inputPath))
        using (MemoryStream memoryStream = new MemoryStream())
        {
            doc.Save(memoryStream); // Writes PDF into memory
            byte[] pdfBytes = memoryStream.ToArray(); // PDF as byte array

            // Example: persist the byte array to a file
            File.WriteAllBytes("output_memory.pdf", pdfBytes);
        }

        Console.WriteLine("Document processing completed.");
    }
}