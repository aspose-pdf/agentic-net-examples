using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // (Optional) Perform any modifications to the document here
            // Example: add a blank page
            // doc.Pages.Add();

            // Save the document into a memory stream
            using (MemoryStream ms = new MemoryStream())
            {
                doc.Save(ms);               // Uses Document.Save(Stream)
                byte[] pdfBytes = ms.ToArray(); // Byte array ready for transmission

                Console.WriteLine($"PDF saved to byte array, size = {pdfBytes.Length} bytes");
                // pdfBytes can now be sent over a network, stored, etc.
            }
        }
    }
}