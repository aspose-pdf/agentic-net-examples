using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string title = "My Document Title";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF, set its title, and save to a temporary file.
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has a title that can be shown in the window caption.
            doc.SetTitle(title);

            // Save to a temporary file because PdfContentEditor works on file paths.
            string tempPath = Path.GetTempFileName();
            doc.Save(tempPath);

            // Change the viewer preference to display the document title in the window bar.
            PdfContentEditor editor = new PdfContentEditor();
            editor.BindPdf(tempPath);
            editor.ChangeViewerPreference(ViewerPreference.DisplayDocTitle);
            editor.Save(outputPath);
            editor.Close();

            // Remove the temporary file.
            File.Delete(tempPath);
        }

        Console.WriteLine($"PDF saved with DisplayDocTitle enabled: '{outputPath}'.");
    }
}