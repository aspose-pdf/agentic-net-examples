using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input XML files that will be converted to PDF
        string[] xmlFiles = {
            "input1.xml",
            "input2.xml",
            "input3.xml"
        };

        // Directory for intermediate PDF files
        string tempDir = Path.Combine(Path.GetTempPath(), "XmlToPdfTemp");
        Directory.CreateDirectory(tempDir);

        // List to hold paths of the generated PDFs
        List<string> pdfFiles = new List<string>();

        // Convert each XML to a PDF and store the PDF path
        foreach (string xmlPath in xmlFiles)
        {
            if (!File.Exists(xmlPath))
            {
                Console.Error.WriteLine($"XML file not found: {xmlPath}");
                continue;
            }

            string pdfPath = Path.Combine(
                tempDir,
                Path.GetFileNameWithoutExtension(xmlPath) + ".pdf");

            // Load XML with XmlLoadOptions and save as PDF
            using (Document doc = new Document(xmlPath, new XmlLoadOptions()))
            {
                doc.Save(pdfPath);
            }

            pdfFiles.Add(pdfPath);
        }

        // Ensure we have PDFs to merge
        if (pdfFiles.Count == 0)
        {
            Console.Error.WriteLine("No PDF files were generated. Exiting.");
            return;
        }

        // Output merged PDF path
        const string outputPath = "merged_output.pdf";

        // Merge the generated PDFs into a single document
        using (Document mergedDoc = new Document())
        {
            // Remove the default blank page created by the empty Document
            mergedDoc.Pages.Clear();

            // Merge all PDF files
            mergedDoc.Merge(pdfFiles.ToArray());

            // Save the merged PDF
            mergedDoc.Save(outputPath);
        }

        // Clean up temporary PDF files
        foreach (string pdfPath in pdfFiles)
        {
            try { File.Delete(pdfPath); } catch { /* ignore cleanup errors */ }
        }
        try { Directory.Delete(tempDir, true); } catch { /* ignore cleanup errors */ }

        Console.WriteLine($"Merged PDF created at: {outputPath}");
    }
}