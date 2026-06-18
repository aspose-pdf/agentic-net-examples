using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths to the XML files that will be converted to PDF.
        string[] xmlFiles = {
            "invoice1.xml",
            "invoice2.xml",
            "invoice3.xml"
        };

        // Output merged PDF file.
        const string outputPdf = "merged_output.pdf";

        // Create an empty PDF document that will receive the merged pages.
        using (Document mergedDoc = new Document())
        {
            // Iterate over each XML source, convert it to a PDF document,
            // and merge it into the target document.
            foreach (string xmlPath in xmlFiles)
            {
                if (!File.Exists(xmlPath))
                {
                    Console.Error.WriteLine($"File not found: {xmlPath}");
                    continue;
                }

                // Load the XML file using XmlLoadOptions (required for XML input).
                XmlLoadOptions loadOptions = new XmlLoadOptions();

                // Load the XML as a PDF document.
                using (Document srcDoc = new Document(xmlPath, loadOptions))
                {
                    // Merge the source document into the target.
                    mergedDoc.Merge(srcDoc);
                }
            }

            // Save the merged PDF.
            mergedDoc.Save(outputPdf);
        }

        Console.WriteLine($"Merged PDF saved to '{outputPdf}'.");
    }
}