using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Create a PdfAnnotationEditor without binding it to any PDF document.
        PdfAnnotationEditor editor = new PdfAnnotationEditor();

        try
        {
            // Attempt to export annotations to a stream.
            // This should fail because the editor is not bound to a document.
            using (MemoryStream ms = new MemoryStream())
            {
                editor.ExportAnnotationsToXfdf(ms);
            }

            // If no exception is thrown, the test has failed.
            Console.WriteLine("Test Failed: No exception was thrown.");
        }
        catch (Exception ex)
        {
            // Expected path: an exception is thrown.
            Console.WriteLine($"Test Passed: Caught expected exception -> {ex.GetType().Name}: {ex.Message}");
        }
    }
}