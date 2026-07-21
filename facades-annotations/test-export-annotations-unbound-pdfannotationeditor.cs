using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Create a PdfAnnotationEditor instance without binding it to any PDF.
        PdfAnnotationEditor editor = new PdfAnnotationEditor();

        // Attempt to export annotations; this should throw an exception because the editor is unbound.
        try
        {
            using (MemoryStream stream = new MemoryStream())
            {
                editor.ExportAnnotationsToXfdf(stream);
                // If no exception is thrown, the test has failed.
                Console.Error.WriteLine("Test failed: ExportAnnotationsToXfdf did not throw an exception on an unbound editor.");
            }
        }
        catch (Exception ex)
        {
            // Expected outcome: an exception is thrown.
            Console.WriteLine($"Expected exception caught: {ex.GetType().Name} - {ex.Message}");
        }
        finally
        {
            // Ensure resources are released.
            editor.Dispose();
        }
    }
}