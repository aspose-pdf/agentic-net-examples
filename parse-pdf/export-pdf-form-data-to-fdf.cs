using System;
using System.IO;
using Aspose.Pdf; // Core namespace only; do NOT add Aspose.Pdf.Facades using

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputFdfPath = "output.fdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Use the Form facade via its fully qualified name (no using directive for Aspose.Pdf.Facades)
        using (var form = new Aspose.Pdf.Facades.Form(inputPdfPath))
        {
            // Open a writable FileStream for the FDF output
            using (FileStream fdfStream = new FileStream(outputFdfPath, FileMode.Create, FileAccess.Write))
            {
                // Export all form field data to the FDF stream
                form.ExportFdf(fdfStream);
            }

            // The PDF itself is unchanged; if you need to save it explicitly you can call:
            // form.Save(); // saves the original PDF (optional)
        }

        Console.WriteLine($"Form data successfully exported to '{outputFdfPath}'.");
    }
}