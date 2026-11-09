using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Microsoft.CSharp.RuntimeBinder;

class Program
{
    static void Main()
    {
        // List of PDF file paths to process
        List<string> pdfPaths = new List<string>
        {
            "input1.pdf",
            "input2.pdf",
            "input3.pdf"
        };

        // Directory where the modified PDFs will be saved
        string outputDir = "ProcessedPdfs";
        Directory.CreateDirectory(outputDir);

        // Define the viewer preferences that will be applied to every PDF
        bool hideToolbar   = true;
        bool hideMenubar   = true;
        bool centerWindow  = true;
        bool displayDocTitle = true;

        foreach (string inputPath in pdfPaths)
        {
            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine($"File not found: {inputPath}");
                continue;
            }

            // Determine output file path (same name, different folder)
            string outputPath = Path.Combine(outputDir, Path.GetFileName(inputPath));

            // Load the PDF document
            Document pdfDocument = new Document(inputPath);

            // Apply viewer preferences – use dynamic access to stay compatible with older Aspose.Pdf versions
            dynamic doc = pdfDocument;
            try
            {
                doc.ViewerPreferences.HideToolbar   = hideToolbar;
                doc.ViewerPreferences.HideMenubar   = hideMenubar;
                doc.ViewerPreferences.CenterWindow  = centerWindow;
                doc.ViewerPreferences.DisplayDocTitle = displayDocTitle;
            }
            catch (RuntimeBinderException)
            {
                Console.WriteLine($"ViewerPreferences property is not supported by the current Aspose.Pdf library version for file '{inputPath}'. Skipping preference changes.");
            }

            // Save the modified PDF
            pdfDocument.Save(outputPath);
            Console.WriteLine($"Processed '{inputPath}' → '{outputPath}'");
        }
    }
}
