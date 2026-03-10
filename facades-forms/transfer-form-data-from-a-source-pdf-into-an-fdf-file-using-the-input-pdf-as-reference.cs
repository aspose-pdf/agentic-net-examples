using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths for the source PDF and the destination FDF file
        const string sourcePdfPath = "source.pdf";
        const string destFdfPath   = "output.fdf";

        // Verify that the source PDF exists
        if (!File.Exists(sourcePdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {sourcePdfPath}");
            return;
        }

        // Open the PDF document inside a using block to ensure proper disposal
        using (Document pdfDoc = new Document(sourcePdfPath))
        {
            // Create a Form facade bound to the opened document
            Form pdfForm = new Form(pdfDoc);

            // Export the form fields to an FDF stream
            using (FileStream fdfStream = new FileStream(destFdfPath, FileMode.Create, FileAccess.Write))
            {
                pdfForm.ExportFdf(fdfStream);
            }

            // No explicit Save is required for the Form when only exporting data
        }

        Console.WriteLine($"Form data exported to FDF: {destFdfPath}");
    }
}