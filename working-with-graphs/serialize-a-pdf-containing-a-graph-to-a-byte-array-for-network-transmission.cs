using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Path to the source PDF that already contains a graph.
        const string inputPath = "graph.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Byte array that will hold the serialized PDF.
        byte[] pdfBytes;

        // Load the PDF inside a using block for deterministic disposal.
        using (Document doc = new Document(inputPath))
        {
            // Save the document to a memory stream (PDF format is implicit).
            using (MemoryStream ms = new MemoryStream())
            {
                doc.Save(ms);
                pdfBytes = ms.ToArray(); // Extract the byte array for transmission.
            }
        }

        // Example usage: display the size of the serialized PDF.
        Console.WriteLine($"Serialized PDF size: {pdfBytes.Length} bytes");
        // At this point, pdfBytes can be sent over a network socket, HTTP response, etc.
    }
}