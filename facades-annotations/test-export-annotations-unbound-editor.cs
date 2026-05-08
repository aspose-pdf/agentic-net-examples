using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Create a PdfAnnotationEditor without binding any PDF document.
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            // Prepare a stream to receive XFDF output (won't be used because an exception is expected).
            using (MemoryStream xfdfStream = new MemoryStream())
            {
                try
                {
                    // Attempt to export annotations. Since the editor is unbound, this should throw.
                    editor.ExportAnnotationsXfdf(xfdfStream, 1, 1, new string[] { "Text" });

                    // If no exception occurs, the test has failed.
                    Console.WriteLine("Test failed: ExportAnnotationsXfdf did not throw an exception on an unbound editor.");
                }
                catch (Exception ex)
                {
                    // Expected outcome: an exception is thrown.
                    Console.WriteLine($"Test passed: Caught expected exception -> {ex.GetType().Name}: {ex.Message}");
                }
            }
        }
    }
}