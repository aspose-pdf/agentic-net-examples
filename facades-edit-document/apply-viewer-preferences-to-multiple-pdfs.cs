using System;
using System.IO;
using Aspose.Pdf.Facades;

class ApplyViewerPreferences
{
    static void Main()
    {
        // List of PDF file paths to process
        string[] pdfFiles = new string[]
        {
            @"C:\Docs\sample1.pdf",
            @"C:\Docs\sample2.pdf",
            @"C:\Docs\sample3.pdf"
        };

        // Desired viewer preferences (combine as needed)
        int[] preferences = new int[]
        {
            ViewerPreference.HideMenubar,
            ViewerPreference.HideToolbar,
            ViewerPreference.PageModeUseNone
        };

        foreach (string inputPath in pdfFiles)
        {
            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine($"File not found: {inputPath}");
                continue;
            }

            // Build output file name (original name with suffix)
            string outputPath = Path.Combine(
                Path.GetDirectoryName(inputPath) ?? string.Empty,
                Path.GetFileNameWithoutExtension(inputPath) + "_pref.pdf");

            // Use PdfContentEditor facade to modify viewer preferences
            using (PdfContentEditor editor = new PdfContentEditor())
            {
                // Bind the source PDF
                editor.BindPdf(inputPath);

                // Apply each viewer preference
                foreach (int pref in preferences)
                {
                    editor.ChangeViewerPreference(pref);
                }

                // Save the modified PDF
                editor.Save(outputPath);
            }

            Console.WriteLine($"Processed '{inputPath}' → '{outputPath}'");
        }
    }
}