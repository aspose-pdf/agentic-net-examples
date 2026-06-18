using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string title      = "My PDF Title";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Step 1: Load the PDF, set its document title, and save to a temporary file.
        // The temporary file is needed because PdfContentEditor works on a file path.
        string tempPath = Path.GetTempFileName();
        using (Document doc = new Document(inputPath))
        {
            doc.SetTitle(title);               // Set the PDF title.
            doc.Save(tempPath);                // Persist the title change.
        }

        // Step 2: Use PdfContentEditor (Aspose.Pdf.Facades) to set the DisplayDocTitle viewer preference.
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(tempPath);                              // Load the temporary PDF.
        editor.ChangeViewerPreference(ViewerPreference.DisplayDocTitle); // Enable title in window caption.
        editor.Save(outputPath);                               // Save the final PDF.
        editor.Close();                                        // Release facade resources.

        // Clean up the temporary file.
        File.Delete(tempPath);

        Console.WriteLine($"PDF saved with DisplayDocTitle enabled: {outputPath}");
    }
}