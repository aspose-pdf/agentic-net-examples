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

        // Load the PDF document; the using block ensures proper disposal.
        using (Document pdfDoc = new Document(inputPath))
        {
            // Create a memory stream that will receive the PDF data.
            using (MemoryStream outputStream = new MemoryStream())
            {
                // Save the document as PDF into the stream.
                pdfDoc.Save(outputStream);

                // Reset the stream position if it will be read afterwards.
                outputStream.Position = 0;

                // Example output: display the size of the generated PDF.
                Console.WriteLine($"PDF saved to stream, length = {outputStream.Length} bytes");
                // The stream can now be returned, sent over a network, or written to a file.
            }
        }
    }
}