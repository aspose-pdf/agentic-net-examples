using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "PdfForm.pdf";
        const string outputFdf = "export.fdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        // Initialize the Form facade for the PDF document
        Form pdfForm = new Form(inputPdf);

        // Export form fields to an FDF file using a file stream
        using (FileStream fdfStream = new FileStream(outputFdf, FileMode.Create, FileAccess.Write))
        {
            pdfForm.ExportFdf(fdfStream);
        }

        // Release resources held by the Form facade
        pdfForm.Close();

        Console.WriteLine($"Form data exported to '{outputFdf}'.");
    }
}