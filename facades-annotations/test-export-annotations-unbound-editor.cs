using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Create a PdfAnnotationEditor without binding it to any PDF document.
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            // Use a memory stream as the destination for the XFDF export.
            using (MemoryStream output = new MemoryStream())
            {
                try
                {
                    // Attempt to export annotations. This should fail because the editor is unbound.
                    editor.ExportAnnotationsToXfdf(output);
                    Console.WriteLine("FAIL: ExportAnnotationsToXfdf did not throw an exception as expected.");
                }
                catch (Exception ex)
                {
                    // Expected outcome: an exception is thrown.
                    Console.WriteLine($"PASS: Caught expected exception: {ex.GetType().Name} - {ex.Message}");
                }
            }
        }
    }
}