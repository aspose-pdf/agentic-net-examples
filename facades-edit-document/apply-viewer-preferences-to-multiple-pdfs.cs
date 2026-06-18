using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf.Facades;          // PdfContentEditor, ViewerPreference
using Aspose.Pdf;                 // Document (if needed)

class ApplyViewerPreferences
{
    static void Main()
    {
        // List of PDF file paths to process
        List<string> pdfFiles = new List<string>
        {
            @"C:\Docs\file1.pdf",
            @"C:\Docs\file2.pdf",
            @"C:\Docs\file3.pdf"
        };

        // Directory where the modified PDFs will be saved
        string outputDir = @"C:\Docs\Modified";
        Directory.CreateDirectory(outputDir);

        // Define the viewer preferences to apply (combine using bitwise OR if needed)
        int preferences = ViewerPreference.HideMenubar |
                          ViewerPreference.HideToolbar |
                          ViewerPreference.PageModeUseNone;

        foreach (string inputPath in pdfFiles)
        {
            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine($"File not found: {inputPath}");
                continue;
            }

            // Build output file path (same file name in the output directory)
            string outputPath = Path.Combine(outputDir, Path.GetFileName(inputPath));

            // Use PdfContentEditor to modify viewer preferences
            using (PdfContentEditor editor = new PdfContentEditor())
            {
                // Load the source PDF
                editor.BindPdf(inputPath);

                // Apply the desired viewer preferences
                editor.ChangeViewerPreference(preferences);

                // Save the modified PDF
                editor.Save(outputPath);
            }

            Console.WriteLine($"Processed: {inputPath} -> {outputPath}");
        }
    }
}