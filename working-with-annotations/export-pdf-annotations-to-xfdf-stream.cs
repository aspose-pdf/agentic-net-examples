using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";

        // Ensure the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdfPath))
        {
            // Create a memory stream to hold the XFDF data
            using (MemoryStream xfdfStream = new MemoryStream())
            {
                // Export all annotations from the document into the stream
                doc.ExportAnnotationsToXfdf(xfdfStream);

                // Reset stream position to read the content
                xfdfStream.Position = 0;
                using (StreamReader reader = new StreamReader(xfdfStream))
                {
                    string xfdfContent = reader.ReadToEnd();
                    // Output the XFDF data (or process as needed)
                    Console.WriteLine("XFDF Export:");
                    Console.WriteLine(xfdfContent);
                }
            }
        }
    }
}