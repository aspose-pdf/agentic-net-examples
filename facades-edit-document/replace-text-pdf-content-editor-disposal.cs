using System;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";

        // Create a minimal PDF so the example can run in an empty sandbox.
        using (Document seed = new Document())
        {
            seed.Pages.Add();
            seed.Save(inputPdf);
        }

        // Ensure the PdfContentEditor is always closed, even if an exception occurs.
        PdfContentEditor? editor = null;
        try
        {
            editor = new PdfContentEditor();
            editor.BindPdf(inputPdf);

            // Example operation: replace all occurrences of "OldText" with "NewText".
            editor.ReplaceText("OldText", "NewText");

            // Save the modified document.
            editor.Save(outputPdf);
        }
        finally
        {
            // Close releases all resources associated with the facade.
            if (editor != null)
                editor.Close();
        }

        Console.WriteLine($"Processed PDF saved to '{outputPdf}'.");
    }
}
