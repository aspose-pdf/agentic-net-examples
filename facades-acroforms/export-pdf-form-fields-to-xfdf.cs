using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputXfdfPath = "form_data.xfdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Form (Facades) implements IDisposable, so wrap it in a using block.
        using (Form pdfForm = new Form())
        {
            // Bind the PDF document that contains the form fields.
            pdfForm.BindPdf(inputPdfPath);

            // Export all form field data to XFDF (actually FDF) format.
            // The Form facade provides ExportFdf which writes the data to a stream.
            using (FileStream xfdfStream = new FileStream(outputXfdfPath, FileMode.Create, FileAccess.Write))
            {
                pdfForm.ExportFdf(xfdfStream);
            }
        }

        Console.WriteLine($"Form data exported to XFDF: {outputXfdfPath}");
    }
}
