using System;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Path to the source PDF form file
        const string pdfPath = "form.pdf";

        // Verify that the file exists before attempting to bind
        if (!System.IO.File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Create an AutoFiller instance (facade for filling PDF forms)
        AutoFiller autoFiller = new AutoFiller();

        // Bind the PDF form to the AutoFiller using the file path
        autoFiller.BindPdf(pdfPath);

        // The AutoFiller is now ready for further operations such as importing data,
        // filling fields, and saving the result.
        Console.WriteLine("PDF form successfully bound to AutoFiller.");
    }
}