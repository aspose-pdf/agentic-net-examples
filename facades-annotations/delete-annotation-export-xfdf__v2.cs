using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string pdfPath = "contract_update.pdf";
        const string xfdfPath = "remaining4.xfdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        try
        {
            // PdfAnnotationEditor implements IDisposable, so wrap it in a using block
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
            {
                // Load the PDF document
                editor.BindPdf(pdfPath);

                // Remove the annotation named "Stamp4"
                editor.DeleteAnnotation("Stamp4");

                // Export the remaining annotations from pages 1‑3 to XFDF.
                // ExportAnnotationsXfdf is the correct method; an empty string array means "all types".
                using (FileStream fs = File.Create(xfdfPath))
                {
                    editor.ExportAnnotationsXfdf(fs, 1, 3, new string[0]);
                }
            }

            Console.WriteLine($"Remaining annotations exported to '{xfdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
