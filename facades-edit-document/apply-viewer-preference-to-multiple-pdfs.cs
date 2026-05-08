using System;
using System.IO;
using Aspose.Pdf.Facades; // Provides PdfContentEditor and ViewerPreference

class Program
{
    static void Main()
    {
        // Array of source PDF file paths
        string[] pdfFiles = { "doc1.pdf", "doc2.pdf", "doc3.pdf" };

        // Directory where modified PDFs will be saved
        string outputDir = "Processed";
        Directory.CreateDirectory(outputDir);

        // Apply the same viewer preference to each PDF
        foreach (string inputPath in pdfFiles)
        {
            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine($"File not found: {inputPath}");
                continue;
            }

            // Build output file name (e.g., doc1_pref.pdf)
            string outputPath = Path.Combine(outputDir,
                Path.GetFileNameWithoutExtension(inputPath) + "_pref.pdf");

            // Use PdfContentEditor facade to modify viewer preferences
            using (PdfContentEditor editor = new PdfContentEditor())
            {
                // Load the source PDF
                editor.BindPdf(inputPath);

                // Change viewer preference – hide the menu bar (example)
                editor.ChangeViewerPreference(ViewerPreference.HideMenubar);

                // Save the updated PDF
                editor.Save(outputPath);
            }

            Console.WriteLine($"Processed: {outputPath}");
        }
    }
}