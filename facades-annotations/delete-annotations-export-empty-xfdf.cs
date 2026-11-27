using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "schedule.pdf";
        const string xfdfPath = "empty.xfdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        try
        {
            // PdfAnnotationEditor does not implement IDisposable, so we instantiate it directly.
            PdfAnnotationEditor editor = new PdfAnnotationEditor();

            // Bind the PDF document to the editor.
            editor.BindPdf(pdfPath);

            // Delete all annotations from the bound document.
            editor.DeleteAnnotations();

            // Export the (now empty) annotation set to an XFDF file.
            using (FileStream fs = File.Create(xfdfPath))
            {
                // ExportAnnotationsToXfdf writes all annotations (none after deletion).
                editor.ExportAnnotationsToXfdf(fs);
            }

            // Optionally close the editor to release resources.
            editor.Close();

            Console.WriteLine($"Empty XFDF exported to '{xfdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
