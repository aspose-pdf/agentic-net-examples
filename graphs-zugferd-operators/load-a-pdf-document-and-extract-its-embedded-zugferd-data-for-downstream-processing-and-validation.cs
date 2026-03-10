using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath = "invoice.pdf";
        const string outputXmlPath = "zugferd.xml";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Load the PDF document (wrapped in using for deterministic disposal)
        using (Document pdfDoc = new Document(pdfPath))
        {
            // The ZUGFeRD XML is stored as an embedded file (usually with .xml extension)
            // Aspose.Pdf exposes the collection via the EmbeddedFiles property.
            var embeddedFiles = pdfDoc.EmbeddedFiles;

            if (embeddedFiles == null || embeddedFiles.Count == 0)
            {
                Console.WriteLine("No embedded files found in the PDF.");
                return;
            }

            // Search for an XML file that likely contains the ZUGFeRD data
            foreach (FileSpecification fileSpec in embeddedFiles)
            {
                // fileSpec.Name holds the original filename of the embedded file
                if (!string.IsNullOrEmpty(fileSpec.Name) &&
                    fileSpec.Name.EndsWith(".xml", StringComparison.OrdinalIgnoreCase))
                {
                    // Directly copy the embedded XML stream to the output file
                    using (FileStream outFs = new FileStream(outputXmlPath, FileMode.Create, FileAccess.Write))
                    {
                        fileSpec.Contents.CopyTo(outFs);
                    }

                    Console.WriteLine($"ZUGFeRD XML extracted to '{outputXmlPath}'.");
                    return; // Extraction complete
                }
            }

            Console.WriteLine("ZUGFeRD XML not found among embedded files.");
        }
    }
}
