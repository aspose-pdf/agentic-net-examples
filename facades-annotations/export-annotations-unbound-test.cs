using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Create a PdfAnnotationEditor without binding it to any PDF document
        PdfAnnotationEditor editor = new PdfAnnotationEditor();
        using (MemoryStream outputStream = new MemoryStream())
        {
            try
            {
                // This call should throw because the editor is unbound
                editor.ExportAnnotationsToXfdf(outputStream);
                Console.WriteLine("No exception was thrown – test failed.");
            }
            catch (Exception exception)
            {
                Console.WriteLine("Expected exception caught: " + exception.Message);
            }
        }
    }
}