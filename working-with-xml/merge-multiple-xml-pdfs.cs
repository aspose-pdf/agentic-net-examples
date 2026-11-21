using System;
using System.IO;
using Aspose.Pdf; // Document, XmlLoadOptions, etc.

class Program
{
    static void Main()
    {
        // Folder containing the XML files that will be converted to PDF
        const string xmlFolder = "XmlInputs";
        // Output merged PDF path
        const string mergedPdfPath = "MergedDocument.pdf";

        // Validate input folder
        if (!Directory.Exists(xmlFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {xmlFolder}");
            return;
        }

        // Get all XML files (order can be adjusted as needed)
        string[] xmlFiles = Directory.GetFiles(xmlFolder, "*.xml");
        if (xmlFiles.Length == 0)
        {
            Console.Error.WriteLine("No XML files found to process.");
            return;
        }

        // Create an empty target document that will hold all pages
        using (Document target = new Document())
        {
            // Iterate over each XML file, convert to PDF, and append its pages
            foreach (string xmlPath in xmlFiles)
            {
                // Load XML as a PDF document using XmlLoadOptions
                XmlLoadOptions loadOpts = new XmlLoadOptions();
                using (Document source = new Document(xmlPath, loadOpts))
                {
                    // Append all pages from the source PDF to the target PDF
                    target.Pages.Add(source.Pages);
                }
            }

            // Save the merged PDF
            target.Save(mergedPdfPath);
        }

        Console.WriteLine($"Merged PDF created at: {mergedPdfPath}");
    }
}