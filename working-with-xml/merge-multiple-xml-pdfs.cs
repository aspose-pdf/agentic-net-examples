using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths to the XML files that will be converted to PDF
        string[] xmlFiles = { "input1.xml", "input2.xml", "input3.xml" };
        const string outputPdf = "merged_output.pdf";

        // Verify that all XML source files exist
        foreach (var xmlPath in xmlFiles)
        {
            if (!File.Exists(xmlPath))
            {
                Console.Error.WriteLine($"Error: XML file not found – {xmlPath}");
                return;
            }
        }

        // Load the first XML file as the base document
        using (Document mergedDoc = new Document(xmlFiles[0], new XmlLoadOptions()))
        {
            // Load each subsequent XML file and merge it into the base document
            for (int i = 1; i < xmlFiles.Length; i++)
            {
                using (Document doc = new Document(xmlFiles[i], new XmlLoadOptions()))
                {
                    mergedDoc.Merge(doc); // Instance merge adds pages from doc to mergedDoc
                }
            }

            // Save the combined PDF
            mergedDoc.Save(outputPdf);
        }

        Console.WriteLine($"Merged PDF created successfully at '{outputPdf}'.");
    }
}