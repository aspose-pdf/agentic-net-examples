using System;
using System.IO;
using Aspose.Pdf;               // Core Aspose.Pdf namespace (contains XmlSaveOptions)

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputXmlPath = "output.xml";

        // Verify the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Configure XML save options (explicitly required to get XML output)
                XmlSaveOptions xmlOptions = new XmlSaveOptions();

                // Save the PDF as XML representation
                pdfDoc.Save(outputXmlPath, xmlOptions);
            }

            Console.WriteLine($"XML representation saved to '{outputXmlPath}'.");
        }
        catch (Exception ex)
        {
            // Generic error handling – report any issues during processing
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}