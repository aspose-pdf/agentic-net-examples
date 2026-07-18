using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input XML files that will be converted to PDFs
        string[] xmlFiles = {
            "input1.xml",
            "input2.xml",
            "input3.xml"
        };

        // Output merged PDF file
        const string outputPdf = "merged_output.pdf";

        // Validate input files exist
        foreach (string xmlPath in xmlFiles)
        {
            if (!File.Exists(xmlPath))
            {
                Console.Error.WriteLine($"File not found: {xmlPath}");
                return;
            }
        }

        // Load each XML into a separate Document using XmlLoadOptions
        var documents = new List<Document>();
        foreach (string xmlPath in xmlFiles)
        {
            // XmlLoadOptions is required for XML to PDF conversion
            XmlLoadOptions loadOptions = new XmlLoadOptions();
            Document doc = new Document(xmlPath, loadOptions);
            documents.Add(doc);
        }

        // Create an empty target document and merge all loaded documents into it
        using (Document target = new Document())
        {
            // Merge the array of documents into the target
            target.Merge(documents.ToArray());

            // Save the merged PDF
            target.Save(outputPdf);
        }

        Console.WriteLine($"Merged PDF saved to '{outputPdf}'.");
    }
}