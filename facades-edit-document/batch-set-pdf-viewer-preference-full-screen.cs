using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Directory containing PDF files
        const string inputDirectory = @"C:\PdfFiles";

        if (!Directory.Exists(inputDirectory))
        {
            Console.Error.WriteLine($"Directory not found: {inputDirectory}");
            return;
        }

        // Process each PDF file in the directory
        foreach (string pdfPath in Directory.GetFiles(inputDirectory, "*.pdf"))
        {
            try
            {
                // Initialize the facade for editing PDF viewer preferences
                PdfContentEditor editor = new PdfContentEditor();

                // Bind the existing PDF file
                editor.BindPdf(pdfPath);

                // Set the viewer preference to full‑screen mode
                editor.ChangeViewerPreference(ViewerPreference.PageModeFullScreen);

                // Save changes (overwrites the original file)
                editor.Save(pdfPath);

                Console.WriteLine($"Updated viewer preference for: {Path.GetFileName(pdfPath)}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        }
    }
}