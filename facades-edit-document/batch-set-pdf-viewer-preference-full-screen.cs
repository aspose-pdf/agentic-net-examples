using System;
using System.IO;
using Aspose.Pdf.Facades;   // Facade classes (PdfContentEditor)
using Aspose.Pdf;          // ViewerPreference constants

class Program
{
    static void Main()
    {
        // Directory containing the PDF files to process
        const string inputDirectory = "pdfs";

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
                // Initialize the content editor facade
                PdfContentEditor editor = new PdfContentEditor();

                // Bind the existing PDF file
                editor.BindPdf(pdfPath);

                // Set the viewer preference to full‑screen mode
                editor.ChangeViewerPreference(ViewerPreference.PageModeFullScreen);

                // Save the changes back to the same file
                editor.Save(pdfPath);

                // Release resources held by the facade
                editor.Close();

                Console.WriteLine($"Updated viewer preference for: {Path.GetFileName(pdfPath)}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        }
    }
}