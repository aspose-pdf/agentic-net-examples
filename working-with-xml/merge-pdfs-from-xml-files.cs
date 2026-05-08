using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf; // Load option classes (e.g., XmlLoadOptions) are in this namespace

class Program
{
    static void Main()
    {
        // Paths to the source XML files (each will be converted to a PDF)
        string[] xmlFiles = {
            "invoice1.xml",
            "invoice2.xml",
            "invoice3.xml"
        };

        // Path for the final merged PDF
        const string mergedPdfPath = "merged_output.pdf";

        // Create an empty PDF document that will hold the merged result
        using (Document mergedDocument = new Document())
        {
            // List to keep the intermediate PDF documents alive until merging is done
            List<Document> intermediateDocs = new List<Document>();

            // Convert each XML file to a PDF document in memory
            foreach (string xmlPath in xmlFiles)
            {
                if (!File.Exists(xmlPath))
                {
                    Console.Error.WriteLine($"XML file not found: {xmlPath}");
                    continue;
                }

                // Load the XML using XmlLoadOptions (required for XML → PDF conversion)
                XmlLoadOptions loadOptions = new XmlLoadOptions();
                Document xmlPdf = new Document(xmlPath, loadOptions);

                // Keep the document for merging
                intermediateDocs.Add(xmlPdf);
            }

            // Merge all intermediate PDF documents into the empty target document
            // The Merge method accepts a params array of Document objects
            mergedDocument.Merge(intermediateDocs.ToArray());

            // Save the merged PDF to disk
            mergedDocument.Save(mergedPdfPath);
            Console.WriteLine($"Merged PDF saved to '{mergedPdfPath}'.");

            // Dispose the intermediate documents explicitly (they are not in using blocks)
            foreach (Document doc in intermediateDocs)
                doc.Dispose();
        }
    }
}
