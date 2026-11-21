using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Instantiate PdfAnnotationEditor without binding it to any PDF document.
        PdfAnnotationEditor editor = new PdfAnnotationEditor();

        try
        {
            // ExportAnnotationsXfdf should throw because the editor is unbound.
            // Use a dummy file stream; the method signature requires a stream, page range and annotation types.
            using (FileStream fs = File.Create("unbound_annotations.xfdf"))
            {
                int startPage = 1;
                int endPage = 1; // dummy range
                string[] annotationTypes = null; // null means all types
                editor.ExportAnnotationsXfdf(fs, startPage, endPage, annotationTypes);
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
