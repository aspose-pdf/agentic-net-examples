using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Determine the directory containing PDF files.
        string targetDirectory = args.Length > 0 ? args[0] : Directory.GetCurrentDirectory();
        if (!Directory.Exists(targetDirectory))
        {
            Console.Error.WriteLine($"Directory not found: {targetDirectory}");
            return;
        }

        // Switch the working directory so that Save uses a simple filename.
        Directory.SetCurrentDirectory(targetDirectory);

        // Get all PDF files in the directory.
        string[] pdfFiles = Directory.GetFiles(".", "*.pdf");
        if (pdfFiles.Length == 0)
        {
            Console.WriteLine("No PDF files found in the specified directory.");
            return;
        }

        foreach (string filePath in pdfFiles)
        {
            string fileName = Path.GetFileName(filePath);
            try
            {
                using (PdfContentEditor editor = new PdfContentEditor())
                {
                    // Load the PDF.
                    editor.BindPdf(fileName);
                    // Set viewer preference to full‑screen mode.
                    editor.ChangeViewerPreference(ViewerPreference.PageModeFullScreen);
                    // Overwrite the original file (output path is a simple filename).
                    editor.Save(fileName);
                }
                Console.WriteLine($"Updated viewer preference: {fileName}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing {fileName}: {ex.Message}");
            }
        }
    }
}