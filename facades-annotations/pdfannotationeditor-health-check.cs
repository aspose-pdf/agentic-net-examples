using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string samplePath = "sample.pdf";

        // Create a simple PDF with one blank page and save it
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();
            doc.Save(samplePath);
        }

        // Verify that PdfAnnotationEditor can bind to the created PDF without errors
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            editor.BindPdf(samplePath);
            // Close releases the bound document; Dispose will be called automatically by using
            editor.Close();
        }

        Console.WriteLine("PdfAnnotationEditor bound successfully to the sample PDF.");
    }
}