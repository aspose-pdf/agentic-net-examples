using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string fdfPath = "output.fdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document doc = new Document(pdfPath))
            // Create the output FDF file stream
            using (FileStream fdfStream = new FileStream(fdfPath, FileMode.Create, FileAccess.Write))
            // Facade for form operations
            using (Form formFacade = new Form())
            {
                // Bind the PDF to the Form facade
                formFacade.BindPdf(doc);
                // Export all form fields to the FDF stream
                formFacade.ExportFdf(fdfStream);
            }

            Console.WriteLine($"Form fields exported to FDF file: {fdfPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}