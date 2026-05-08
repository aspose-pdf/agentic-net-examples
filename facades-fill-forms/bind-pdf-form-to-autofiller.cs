using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string sourcePdfPath = "form.pdf";      // Path to the PDF form
        const string outputPdfPath = "filled_form.pdf"; // Destination for the result

        // Verify that the source file exists
        if (!File.Exists(sourcePdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {sourcePdfPath}");
            return;
        }

        // Create an AutoFiller instance, bind the PDF, and save the (potentially unchanged) result
        using (AutoFiller autoFiller = new AutoFiller())
        {
            // Bind the PDF form to the AutoFiller
            autoFiller.BindPdf(sourcePdfPath);

            // Save the bound document (no data imported in this example)
            autoFiller.Save(outputPdfPath);
        }

        // Use fully‑qualified System.IO.Path to avoid ambiguity with Aspose.Pdf.Drawing.Path
        Console.WriteLine($"AutoFiller bound PDF saved to '{System.IO.Path.GetFullPath(outputPdfPath)}'.");
    }
}
