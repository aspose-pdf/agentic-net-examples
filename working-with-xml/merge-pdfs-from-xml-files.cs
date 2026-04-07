using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input XML files that will be converted to PDFs and then merged
        string[] xmlFiles = new string[]
        {
            "invoice1.xml",
            "invoice2.xml",
            "invoice3.xml"
        };

        // Output merged PDF file
        const string outputPdf = "merged_output.pdf";

        // Validate input files
        foreach (string xmlPath in xmlFiles)
        {
            if (!File.Exists(xmlPath))
            {
                Console.Error.WriteLine($"Input file not found: {xmlPath}");
                return;
            }
        }

        // Load each XML into a separate Document instance
        List<Document> sourceDocs = new List<Document>();
        try
        {
            foreach (string xmlPath in xmlFiles)
            {
                // Load XML using XmlLoadOptions (required for XML input)
                XmlLoadOptions loadOptions = new XmlLoadOptions();
                Document doc = new Document(xmlPath, loadOptions);
                sourceDocs.Add(doc);
            }

            // Create an empty target document that will hold the merged result
            using (Document target = new Document())
            {
                // Merge all source documents into the target
                target.Merge(sourceDocs.ToArray());

                // Save the merged PDF
                target.Save(outputPdf);
            }

            Console.WriteLine($"Merged PDF saved to '{outputPdf}'.");
        }
        finally
        {
            // Ensure all source documents are properly disposed
            foreach (Document doc in sourceDocs)
            {
                doc.Dispose();
            }
        }
    }
}