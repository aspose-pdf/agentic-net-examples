using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "agenda_final.pdf";
        const string xfdfPath = "agendaAdd.xfdf";
        const string outputPdf = "AgendaCleanV2.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(xfdfPath))
        {
            Console.Error.WriteLine($"XFDF file not found: {xfdfPath}");
            return;
        }

        try
        {
            // PdfAnnotationEditor implements IDisposable – use a using block.
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
            {
                // Bind the source PDF.
                editor.BindPdf(inputPdf);

                // Import annotations from the XFDF file.
                // The ImportAnnotations overload expects a string[] of file paths.
                editor.ImportAnnotations(new[] { xfdfPath });

                // Delete the annotation named "RadioButton5".
                // DeleteAnnotation expects a single annotation name (string).
                editor.DeleteAnnotation("RadioButton5");

                // Save the cleaned PDF.
                editor.Save(outputPdf);
            }

            Console.WriteLine($"Clean PDF saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
