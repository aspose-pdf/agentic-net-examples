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
        const string title      = "Sample Document Title";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF, set its title, and adjust the viewer preference.
        using (Document doc = new Document(inputPath))
        {
            // Set the document title that will be shown in the window caption.
            doc.SetTitle(title);

            // Save the modified document to a temporary file before applying viewer preferences.
            // This follows the create‑load‑save lifecycle rule.
            string tempPath = Path.GetTempFileName();
            doc.Save(tempPath);

            // Apply the DisplayDocTitle viewer preference using the Facades API.
            using (PdfContentEditor editor = new PdfContentEditor())
            {
                editor.BindPdf(tempPath);
                editor.ChangeViewerPreference(ViewerPreference.DisplayDocTitle);
                editor.Save(outputPath);
            }

            // Clean up the temporary file.
            File.Delete(tempPath);
        }

        Console.WriteLine($"PDF saved with DisplayDocTitle enabled: {outputPath}");
    }
}