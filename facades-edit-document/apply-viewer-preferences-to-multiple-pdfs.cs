using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // List of PDF file paths to process
        string[] pdfFiles = { "file1.pdf", "file2.pdf", "file3.pdf" };

        // Viewer preferences to apply to each PDF
        int[] preferences = {
            ViewerPreference.HideMenubar,
            ViewerPreference.HideToolbar,
            ViewerPreference.HideWindowUI,
            ViewerPreference.FitWindow,
            ViewerPreference.CenterWindow
        };

        foreach (string inputPath in pdfFiles)
        {
            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine($"File not found: {inputPath}");
                continue;
            }

            // Create output file name (e.g., file1_out.pdf)
            string outputPath = Path.Combine(
                Path.GetDirectoryName(inputPath) ?? string.Empty,
                Path.GetFileNameWithoutExtension(inputPath) + "_out.pdf");

            // Apply viewer preferences using PdfContentEditor
            using (PdfContentEditor editor = new PdfContentEditor())
            {
                editor.BindPdf(inputPath);

                foreach (int pref in preferences)
                {
                    editor.ChangeViewerPreference(pref);
                }

                editor.Save(outputPath);
            }

            Console.WriteLine($"Processed: {outputPath}");
        }
    }
}