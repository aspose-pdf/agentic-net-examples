using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath = "invoice.pdf"; // input PDF containing ZUGFeRD data
        const string outputXmlPath = "ZUGFeRD.xml"; // where to save extracted XML

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Load the PDF document (using statement ensures proper disposal)
        using (Document doc = new Document(pdfPath))
        {
            // ZUGFeRD data is stored as an embedded file (usually named *.xml) in the PDF.
            // Aspose.Pdf exposes embedded files via the EmbeddedFiles collection.
            bool zugferdFound = false;

            // In recent Aspose.Pdf versions the collection returns FileSpecification objects.
            foreach (FileSpecification fileSpec in doc.EmbeddedFiles)
            {
                // Look for an XML file that follows the ZUGFeRD naming convention
                if (!string.IsNullOrEmpty(fileSpec.Name) &&
                    fileSpec.Name.EndsWith(".xml", StringComparison.OrdinalIgnoreCase))
                {
                    // Extract the embedded XML using the Contents stream
                    using (Stream xmlStream = fileSpec.Contents)
                    using (FileStream outStream = new FileStream(outputXmlPath, FileMode.Create, FileAccess.Write))
                    {
                        xmlStream.CopyTo(outStream);
                    }

                    Console.WriteLine($"Extracted ZUGFeRD XML to '{outputXmlPath}'.");
                    zugferdFound = true;
                    break; // assuming only one relevant XML file
                }
            }

            if (!zugferdFound)
            {
                // As a fallback, some PDFs store ZUGFeRD data under a specific catalog entry.
                // The GetCatalogValue method can retrieve raw catalog objects.
                object zugferdCatalog = doc.GetCatalogValue("ZUGFeRD");
                if (zugferdCatalog != null)
                {
                    Console.WriteLine("ZUGFeRD entry found in the PDF catalog.");
                    // Further processing would depend on the exact type returned.
                }
                else
                {
                    Console.WriteLine("No ZUGFeRD data found in the PDF.");
                }
            }
        }
    }
}
