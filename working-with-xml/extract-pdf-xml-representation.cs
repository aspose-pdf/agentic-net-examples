using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputXml = "output.xml";

        // Verify the source PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the PDF document (lifecycle: load)
            using (Document doc = new Document(inputPdf))
            {
                // Export the internal XML representation (lifecycle: save)
                doc.SaveXml(outputXml);
            }

            Console.WriteLine($"XML representation saved to '{outputXml}'.");
        }
        catch (Exception ex)
        {
            // Handle any errors that occur during processing
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}