using System;
using System.IO;
using Aspose.Pdf.Facades;   // PdfContentEditor, ViewerPreference

class Program
{
    static void Main()
    {
        // List of PDF file paths to process
        string[] pdfFiles = {
            @"C:\Docs\file1.pdf",
            @"C:\Docs\file2.pdf",
            @"C:\Docs\file3.pdf"
        };

        // Define the viewer preferences you want to apply to all PDFs.
        // Example: hide the menubar and toolbar, and disable the page mode.
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

            // Create a PdfContentEditor facade
            using (PdfContentEditor editor = new PdfContentEditor())
            {
                // Load the PDF document
                editor.BindPdf(inputPath);

                // Apply the viewer preferences
                editor.ChangeViewerPreference(preferences);

                // Save the modified PDF.
                // Here we overwrite the original file; change the path if you need a separate output.
                editor.Save(inputPath);
            }

            Console.WriteLine($"Applied viewer preferences to: {inputPath}");
        }
    }
}