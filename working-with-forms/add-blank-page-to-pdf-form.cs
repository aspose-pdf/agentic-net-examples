using System;
using System.IO;
using Aspose.Pdf;               // Core Aspose.Pdf namespace
using Aspose.Pdf.Forms;        // For form-related types (if needed)

class Program
{
    static void Main()
    {
        const string inputPath  = "input_form.pdf";
        const string outputPath = "output_with_blank_page.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the existing PDF (which may contain form fields) and preserve them
        using (Document doc = new Document(inputPath))
        {
            // Add a new blank page at the end of the document.
            // The existing form fields remain intact.
            doc.Pages.Add();

            // Save the modified document. No SaveOptions needed for PDF output.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Blank page added successfully. Saved to '{outputPath}'.");
    }
}