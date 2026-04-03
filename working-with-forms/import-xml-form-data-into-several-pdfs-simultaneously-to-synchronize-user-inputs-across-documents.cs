using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input XML file containing form data (XFDF or XFA compatible XML)
        const string xmlDataPath = "formData.xml";

        // List of PDF files to which the XML data will be applied
        var pdfFiles = new List<string>
        {
            "document1.pdf",
            "document2.pdf",
            "document3.pdf"
        };

        // Directory where the updated PDFs will be saved
        const string outputDir = "SyncedPdfs";

        // Validate inputs
        if (!File.Exists(xmlDataPath))
        {
            Console.Error.WriteLine($"XML data file not found: {xmlDataPath}");
            return;
        }

        foreach (var pdfPath in pdfFiles)
        {
            if (!File.Exists(pdfPath))
            {
                Console.Error.WriteLine($"PDF file not found: {pdfPath}");
                continue;
            }
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // Process each PDF: bind XML data and save the result
        foreach (var pdfPath in pdfFiles)
        {
            // Skip missing files (already reported)
            if (!File.Exists(pdfPath)) continue;

            // Use a using block for deterministic disposal (document-disposal-with-using rule)
            using (Document doc = new Document(pdfPath))
            {
                // Import XML form data into the document (BindXml rule)
                doc.BindXml(xmlDataPath);

                // Construct output file path
                string outputPath = Path.Combine(outputDir, Path.GetFileName(pdfPath));

                // Save the updated PDF (standard Save method)
                doc.Save(outputPath);
                Console.WriteLine($"Synchronized PDF saved: {outputPath}");
            }
        }
    }
}