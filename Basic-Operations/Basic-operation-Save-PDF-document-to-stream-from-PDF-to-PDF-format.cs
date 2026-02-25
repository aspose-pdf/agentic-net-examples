using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        // Ensure the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the source PDF, save it into a memory stream, then optionally write that stream to a file
        using (FileStream inputStream = new FileStream(inputPath, FileMode.Open, FileAccess.Read))
        using (Document doc = new Document(inputStream))
        using (MemoryStream outputStream = new MemoryStream())
        {
            // Save the document to the stream in PDF format (default)
            doc.Save(outputStream);

            // Example: persist the stream to a physical file to verify the result
            File.WriteAllBytes("output_from_stream.pdf", outputStream.ToArray());

            Console.WriteLine("PDF successfully saved to stream and written to 'output_from_stream.pdf'.");
        }
    }
}