using System;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input and output PDF file paths
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";

        // Ensure the input file exists before processing
        if (!System.IO.File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // PdfPageEditor is a facade for editing page properties such as transitions
        using (PdfPageEditor pageEditor = new PdfPageEditor())
        {
            // Bind the source PDF file to the editor
            pageEditor.BindPdf(inputPdf);

            // Apply changes only to the first page (Aspose.Pdf uses 1‑based indexing)
            pageEditor.ProcessPages = new int[] { 1 };

            // Set the transition type to Fade.
            // In Aspose.Pdf the Fade transition is represented by the integer value 0.
            pageEditor.TransitionType = 0;

            // Set the transition duration to 2 seconds.
            pageEditor.TransitionDuration = 2;

            // Commit the changes to the document.
            pageEditor.ApplyChanges();

            // Save the modified PDF to the output path.
            pageEditor.Save(outputPdf);
        }

        Console.WriteLine($"Transition applied and saved to '{outputPdf}'.");
    }
}