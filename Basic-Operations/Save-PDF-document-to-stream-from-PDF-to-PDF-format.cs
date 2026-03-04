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

        // Load the PDF document. The using block ensures deterministic disposal.
        using (Document pdfDoc = new Document(inputPath))
        {
            // Create a memory stream that will receive the PDF data.
            using (MemoryStream outputStream = new MemoryStream())
            {
                // Save the document as PDF into the stream.
                pdfDoc.Save(outputStream);

                // Reset the stream position before reading from it.
                outputStream.Position = 0;

                // Example: write the stream contents to a file to verify the result.
                const string outputPath = "output_from_stream.pdf";
                using (FileStream file = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                {
                    outputStream.CopyTo(file);
                }

                Console.WriteLine($"PDF saved to stream and written to '{outputPath}'.");
            }
        }
    }
}