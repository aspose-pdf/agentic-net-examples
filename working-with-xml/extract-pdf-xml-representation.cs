using System;
using System.IO;
using Aspose.Pdf; // Aspose.Pdf namespace contains Document and related types

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";   // Path to the source PDF
        const string outputXmlPath = "output.xml";  // Desired XML output file

        // Verify that the input file exists before proceeding
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found at '{inputPdfPath}'.");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Save the internal XML representation of the PDF
                pdfDoc.SaveXml(outputXmlPath);
            }

            Console.WriteLine($"XML representation saved to '{outputXmlPath}'.");
        }
        catch (Exception ex)
        {
            // Catch any unexpected errors (e.g., corrupted PDF, I/O issues)
            Console.Error.WriteLine($"Error during XML extraction: {ex.Message}");
        }
    }
}