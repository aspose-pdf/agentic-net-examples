using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "invoice.pdf";
        const string outputXml = "zugferd.xml";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document doc = new Document(inputPdf))
            {
                // ZUGFeRD data is stored as an embedded file in the PDF.
                // Access the collection of embedded files.
                var embeddedFiles = doc.EmbeddedFiles;
                if (embeddedFiles == null || embeddedFiles.Count == 0)
                {
                    Console.WriteLine("No embedded files found; ZUGFeRD data not present.");
                    return;
                }

                bool xmlFound = false;
                foreach (FileSpecification fileSpec in embeddedFiles)
                {
                    // The file name is available via the Name property.
                    string fileName = fileSpec.Name;
                    if (string.IsNullOrEmpty(fileName))
                        continue;

                    // Look for the XML file that contains the ZUGFeRD invoice data.
                    if (fileName.EndsWith(".xml", StringComparison.OrdinalIgnoreCase))
                    {
                        // Extract the embedded XML stream using the Contents property.
                        using (FileStream outStream = File.Create(outputXml))
                        {
                            fileSpec.Contents.CopyTo(outStream);
                        }

                        Console.WriteLine($"ZUGFeRD XML extracted to '{outputXml}'.");
                        xmlFound = true;
                        break;
                    }
                }

                if (!xmlFound)
                {
                    Console.WriteLine("Embedded files exist but no XML ZUGFeRD payload was found.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
