using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Create a PdfAnnotationEditor without binding it to any PDF document.
        PdfAnnotationEditor editor = new PdfAnnotationEditor();

        // Prepare a stream to receive XFDF output.
        using (MemoryStream xfdfStream = new MemoryStream())
        {
            try
            {
                // This call should fail because the editor is not bound to a document.
                editor.ExportAnnotationsToXfdf(xfdfStream);
                Console.WriteLine("Test failed: no exception was thrown.");
            }
            catch (Exception ex)
            {
                // Expected path – an exception indicates the editor is correctly enforcing binding.
                Console.WriteLine($"Test passed: caught expected exception ({ex.GetType().Name}) - {ex.Message}");
            }
        }
    }
}