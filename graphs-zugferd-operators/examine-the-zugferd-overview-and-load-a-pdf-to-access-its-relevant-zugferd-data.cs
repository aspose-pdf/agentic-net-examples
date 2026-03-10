using System;
using System.IO;
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

        // Load the PDF document (no special load options required)
        using (Document doc = new Document(pdfPath))
        {
            // Attempt to retrieve ZUGFeRD data from the PDF catalog.
            // The ZUGFeRD XML is usually stored under the "ZUGFeRD" key in the catalog dictionary.
            object zugFerdValue = doc.GetCatalogValue("ZUGFeRD");

            if (zugFerdValue != null)
            {
                Console.WriteLine("ZUGFeRD data found in the PDF catalog:");
                Console.WriteLine(zugFerdValue.ToString());
            }
            else
            {
                Console.WriteLine("No ZUGFeRD data found in the PDF catalog.");
            }

            // Additionally, ZUGFeRD XML may be embedded as an associated file.
            // Iterate over embedded files (if any) and look for a .xml file.
            if (doc.EmbeddedFiles != null && doc.EmbeddedFiles.Count > 0)
            {
                // In recent Aspose.Pdf versions the collection returns FileSpecification objects,
                // not EmbeddedFile objects.
                foreach (FileSpecification fileSpec in doc.EmbeddedFiles)
                {
                    if (fileSpec.Name != null && fileSpec.Name.EndsWith(".xml", StringComparison.OrdinalIgnoreCase))
                    {
                        Console.WriteLine($"Found embedded XML file: {fileSpec.Name}");
                        using (MemoryStream ms = new MemoryStream())
                        {
                            // The XML content is stored in the Contents stream of the FileSpecification.
                            fileSpec.Contents.CopyTo(ms);
                            ms.Position = 0;
                            using (StreamReader reader = new StreamReader(ms))
                            {
                                string xmlContent = reader.ReadToEnd();
                                Console.WriteLine("Embedded ZUGFeRD XML content:");
                                Console.WriteLine(xmlContent);
                            }
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("No embedded files present in the PDF.");
            }
        }
    }
}
