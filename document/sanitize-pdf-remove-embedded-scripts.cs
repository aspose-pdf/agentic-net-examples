using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "sanitized.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Remove embedded JavaScript actions (if any) – this mimics the
                // "DeleteEmbeddedScripts" option of HiddenDataSanitizerOptions.
                if (doc.OpenAction is JavascriptAction)
                {
                    doc.OpenAction = null;
                }

                // Clear all annotations on each page. Annotations may also contain
                // JavaScript (e.g., JavaScript actions attached to link annotations).
                foreach (Page page in doc.Pages)
                {
                    page.Annotations.Clear();
                }

                // Save the sanitized PDF. Document.Save without SaveOptions writes a PDF.
                doc.Save(outputPath);
            }

            Console.WriteLine($"Sanitized PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
