using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class HealthCheck
{
    static void Main(string[] args)
    {
        // Path to a sample PDF used for the health check
        const string samplePath = "sample.pdf";

        // Ensure the sample PDF exists – create a minimal one if necessary
        if (!File.Exists(samplePath))
        {
            // Create a simple PDF with a single blank page
            using (Document doc = new Document())
            {
                doc.Pages.Add();               // Add an empty page
                doc.Save(samplePath);          // Save as PDF
            }
        }

        // Attempt to bind PdfAnnotationEditor to the sample PDF
        try
        {
            // PdfAnnotationEditor implements IDisposable, so use a using block
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
            {
                // Bind the editor to the PDF file
                editor.BindPdf(samplePath);

                // Verify that the underlying Document was loaded successfully
                if (editor.Document != null)
                {
                    Console.WriteLine("PdfAnnotationEditor successfully bound to the PDF.");
                }
                else
                {
                    Console.WriteLine("PdfAnnotationEditor bound, but Document is null.");
                }
            }
        }
        catch (Exception ex)
        {
            // Report any binding errors
            Console.Error.WriteLine($"PdfAnnotationEditor binding failed: {ex.Message}");
        }
    }
}