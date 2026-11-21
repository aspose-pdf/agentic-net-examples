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

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // PdfPageEditor is used to modify page‑level presentation settings
            using (PdfPageEditor editor = new PdfPageEditor())
            {
                editor.BindPdf(doc);

                // Target page 1 (1‑based indexing)
                editor.ProcessPages = new int[] { 1 };

                // Fade transition – the property expects an integer value.
                // In the Aspose.Pdf versions that expose this API, 2 corresponds to Fade.
                editor.TransitionType = 2;          // Fade
                editor.TransitionDuration = 2;      // duration in seconds

                // Persist the changes
                editor.Save(outputPath);
            }
        }

        Console.WriteLine($"Transition applied and saved to '{outputPath}'.");
    }
}
