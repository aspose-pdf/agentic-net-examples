using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class ExportFormToFdf
{
    static void Main()
    {
        // Path to the source PDF containing form fields
        const string pdfPath = "input.pdf";

        // Path where the FDF file will be written
        const string fdfPath = "output.fdf";

        // Ensure the source PDF exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Load the PDF document
        using (Document pdfDoc = new Document(pdfPath))
        {
            // Open a FileStream for the FDF output (create or overwrite)
            using (FileStream fdfStream = new FileStream(fdfPath, FileMode.Create, FileAccess.Write))
            {
                // Use the Form facade to export form data to FDF
                using (Form formFacade = new Form())
                {
                    formFacade.BindPdf(pdfDoc);
                    formFacade.ExportFdf(fdfStream);
                }
            }
        }

        Console.WriteLine($"Form data exported to FDF: {fdfPath}");
    }
}
