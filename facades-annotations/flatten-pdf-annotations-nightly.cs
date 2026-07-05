using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    // Network share containing PDFs (UNC path or mapped drive)
    private const string NetworkPdfFolder = @"\\SERVER\Shared\PdfFolder";

    // Entry point – schedule this executable to run nightly (e.g., via Windows Task Scheduler)
    static void Main()
    {
        // Validate the folder exists
        if (!Directory.Exists(NetworkPdfFolder))
        {
            Console.Error.WriteLine($"Folder not found: {NetworkPdfFolder}");
            return;
        }

        // Get all PDF files recursively
        string[] pdfFiles = Directory.GetFiles(NetworkPdfFolder, "*.pdf", SearchOption.AllDirectories);

        foreach (string pdfPath in pdfFiles)
        {
            try
            {
                // Use PdfAnnotationEditor to flatten annotations.
                // The class implements IDisposable, so wrap it in a using block.
                using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
                {
                    // Bind the existing PDF file.
                    editor.BindPdf(pdfPath);

                    // Flatten all annotations in the document.
                    editor.FlatteningAnnotations();

                    // Save back to the same file (overwrites the original).
                    // Save(string) writes a PDF regardless of the extension.
                    editor.Save(pdfPath);
                }

                Console.WriteLine($"Flattened annotations: {pdfPath}");
            }
            catch (Exception ex)
            {
                // Log any errors but continue processing other files.
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        }

        Console.WriteLine("Nightly annotation flattening completed.");
    }
}