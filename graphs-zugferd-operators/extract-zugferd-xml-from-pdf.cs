using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath = "invoice.pdf";          // Input PDF containing ZUGFeRD
        const string outputXmlPath = "ZUGFeRD.xml";    // Destination for extracted XML

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        try
        {
            // Load PDF inside a using block for deterministic disposal
            using (Document doc = new Document(pdfPath))
            {
                // Locate the embedded ZUGFeRD XML file (name usually contains "ZUGFeRD" and ends with .xml)
                FileSpecification zugFileSpec = null;
                foreach (FileSpecification fs in doc.EmbeddedFiles)
                {
                    if (!string.IsNullOrEmpty(fs.Name) &&
                        fs.Name.IndexOf("ZUGFeRD", StringComparison.OrdinalIgnoreCase) >= 0 &&
                        fs.Name.EndsWith(".xml", StringComparison.OrdinalIgnoreCase))
                    {
                        zugFileSpec = fs;
                        break;
                    }
                }

                if (zugFileSpec == null)
                {
                    Console.Error.WriteLine("No ZUGFeRD XML attachment found in the PDF.");
                    return;
                }

                // Save the embedded XML to the specified file path using the Contents stream
                using (FileStream outStream = File.Create(outputXmlPath))
                using (Stream content = zugFileSpec.Contents)
                {
                    content.CopyTo(outStream);
                }

                Console.WriteLine($"ZUGFeRD XML extracted to '{outputXmlPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
