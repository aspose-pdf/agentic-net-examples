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

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        Document pdfDocument = new Document(inputPath);

        // Ensure the document has at least three pages
        if (pdfDocument.Pages.Count < 3)
        {
            Console.Error.WriteLine("The PDF does not contain a third page.");
            return;
        }

        // Use PdfPageEditor to set a Split transition (type = 5) with a duration of 2 seconds on page 3
        using (PdfPageEditor editor = new PdfPageEditor(pdfDocument))
        {
            // Specify the page(s) to which the transition will be applied (1‑based index)
            editor.ProcessPages = new int[] { 3 };

            // TransitionType values are defined by the library; Split corresponds to integer value 5
            editor.TransitionType = 5; // Split transition
            editor.TransitionDuration = 2; // 2 seconds

            // Apply the changes to the document
            editor.ApplyChanges();
        }

        // Save the modified PDF
        pdfDocument.Save(outputPath);

        Console.WriteLine($"Page 3 transition set to Split (2 seconds) and saved to '{outputPath}'.");
    }
}
