using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // List of PDF file paths to process
        string[] pdfFiles = new string[]
        {
            "input1.pdf",
            "input2.pdf",
            "input3.pdf"
        };

        // Desired viewer preferences (apply the same set to each document)
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

            // Create output file name (original name with "_out" suffix)
            string outputPath = Path.Combine(
                Path.GetDirectoryName(inputPath) ?? string.Empty,
                Path.GetFileNameWithoutExtension(inputPath) + "_out.pdf");

            // Apply viewer preferences using PdfContentEditor facade
            using (PdfContentEditor editor = new PdfContentEditor())
            {
                editor.BindPdf(inputPath);

                foreach (int pref in preferences)
                {
                    editor.ChangeViewerPreference(pref);
                }

                editor.Save(outputPath);
            }

            Console.WriteLine($"Processed '{inputPath}' → '{outputPath}'");
        }
    }
}