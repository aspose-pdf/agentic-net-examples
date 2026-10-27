using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths to the XML files that will be converted to PDF
        string[] xmlFiles = new string[]
        {
            "invoice1.xml",
            "invoice2.xml",
            "invoice3.xml"
        };

        // Output merged PDF path
        const string outputPdf = "merged_output.pdf";

        // Ensure at least one input file exists
        bool anyExists = false;
        foreach (var path in xmlFiles)
        {
            if (File.Exists(path))
            {
                anyExists = true;
                break;
            }
        }

        if (!anyExists)
        {
            Console.Error.WriteLine("No XML input files were found.");
            return;
        }

        // Create the target document that will hold all pages
        using (Document target = new Document())
        {
            // Iterate over each XML file, convert it to a PDF Document,
            // and append its pages to the target document.
            foreach (string xmlPath in xmlFiles)
            {
                if (!File.Exists(xmlPath))
                {
                    Console.Error.WriteLine($"Skipping missing file: {xmlPath}");
                    continue;
                }

                // Load XML as a PDF document using XmlLoadOptions
                using (Document source = new Document(xmlPath, new XmlLoadOptions()))
                {
                    // Append all pages from the source document to the target
                    target.Pages.Add(source.Pages);
                }
            }

            // Save the merged PDF
            target.Save(outputPdf);
        }

        Console.WriteLine($"Merged PDF created at '{outputPdf}'.");
    }
}
