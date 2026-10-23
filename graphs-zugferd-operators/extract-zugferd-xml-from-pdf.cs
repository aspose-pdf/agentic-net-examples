using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string outputXmlPath = "ZUGFeRD.xml";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document doc = new Document(pdfPath))
            {
                bool extracted = false;

                // Iterate over all embedded files in the PDF using FileSpecification
                foreach (FileSpecification fileSpec in doc.EmbeddedFiles)
                {
                    // ZUGFeRD XML files are typically named with .xml extension and contain "ZUGFeRD"
                    if (!string.IsNullOrEmpty(fileSpec.Name) &&
                        fileSpec.Name.EndsWith(".xml", StringComparison.OrdinalIgnoreCase) &&
                        fileSpec.Name.IndexOf("ZUGFeRD", StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        // Save the embedded XML to the specified path via its Contents stream
                        using (FileStream outStream = File.Create(outputXmlPath))
                        using (Stream content = fileSpec.Contents)
                        {
                            content.CopyTo(outStream);
                        }

                        Console.WriteLine($"ZUGFeRD XML extracted to '{outputXmlPath}'.");
                        extracted = true;
                        break; // stop after the first match
                    }
                }

                if (!extracted)
                {
                    Console.WriteLine("No ZUGFeRD XML embedded file found in the PDF.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
