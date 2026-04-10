using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string samplePdf = "sample.pdf";
        const string outputPdf = "health_check_output.pdf";

        if (!File.Exists(samplePdf))
        {
            Console.Error.WriteLine($"Sample PDF not found: {samplePdf}");
            return;
        }

        try
        {
            // Create the annotation editor and bind it to the sample PDF
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
            {
                editor.BindPdf(samplePdf); // Initialize the facade with the PDF file

                // Simple verification: access the underlying document and read page count
                int pageCount = editor.Document.Pages.Count;
                Console.WriteLine($"Successfully bound. Page count: {pageCount}");

                // Save the document to confirm that Save works without errors
                editor.Save(outputPdf);
                Console.WriteLine($"Health‑check PDF saved to '{outputPdf}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Health check failed: {ex.Message}");
        }
    }
}