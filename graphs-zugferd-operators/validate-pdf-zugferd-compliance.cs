using System;
using System.IO;
using System.Text;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath = "invoice.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(pdfPath))
        {
            // Locate an embedded ZUGFeRD XML file (standard name contains .xml)
            FileSpecification zugferdFile = null;
            foreach (FileSpecification fileSpec in doc.EmbeddedFiles)
            {
                // The correct property for the original file name is "Name"
                if (!string.IsNullOrEmpty(fileSpec.Name) &&
                    fileSpec.Name.EndsWith(".xml", StringComparison.OrdinalIgnoreCase))
                {
                    zugferdFile = fileSpec;
                    break;
                }
            }

            if (zugferdFile == null)
            {
                Console.WriteLine("The PDF does not contain ZUGFeRD data.");
                return;
            }

            // Simple validation: read the XML and look for a known root element.
            // A full ZUGFeRD validation would require a schema validator, which is
            // beyond the scope of this example and would need additional libraries.
            bool isValid = false;
            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    // The Contents property holds the raw bytes of the embedded file.
                    // Use the Contents stream directly – the older FileData property does not exist.
                    if (zugferdFile.Contents != null)
                    {
                        zugferdFile.Contents.CopyTo(ms);
                    }
                    else
                    {
                        Console.WriteLine("Unable to read embedded ZUGFeRD XML content.");
                        return;
                    }

                    // Ensure the stream is read from the beginning.
                    ms.Position = 0;
                    string xmlContent = Encoding.UTF8.GetString(ms.ToArray());

                    // Very basic check for the ZUGFeRD root element.
                    if (xmlContent.Contains("<rsm:CrossIndustryInvoice") || xmlContent.Contains("<Invoice"))
                    {
                        isValid = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while reading ZUGFeRD data: {ex.Message}");
                return;
            }

            if (isValid)
            {
                Console.WriteLine("ZUGFeRD validation succeeded. The PDF appears to contain a valid ZUGFeRD XML payload.");
            }
            else
            {
                Console.WriteLine("ZUGFeRD validation failed. The embedded XML does not match expected ZUGFeRD structure.");
            }
        }
    }
}
