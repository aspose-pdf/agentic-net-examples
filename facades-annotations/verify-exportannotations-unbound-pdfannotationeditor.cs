using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Instantiate PdfAnnotationEditor without binding to any PDF document
        PdfAnnotationEditor editor = new PdfAnnotationEditor();

        // Attempt to export annotations; this should throw because the editor is unbound
        try
        {
            using (MemoryStream stream = new MemoryStream())
            {
                editor.ExportAnnotationsToXfdf(stream);
                // If no exception is thrown, the test has failed
                Console.WriteLine("Export succeeded unexpectedly.");
            }
        }
        catch (Exception ex)
        {
            // Expected path: an exception is thrown indicating the editor is not bound
            Console.WriteLine($"Expected exception caught: {ex.GetType().Name} - {ex.Message}");
        }
        finally
        {
            // Clean up the editor instance
            editor.Dispose();
        }
    }
}