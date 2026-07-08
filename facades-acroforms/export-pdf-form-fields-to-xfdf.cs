using System;
using System.IO;
using Aspose.Pdf.Facades;

class ExportFormToXfdf
{
    static void Main()
    {
        const string pdfPath = "input_form.pdf";
        const string xfdfPath = "output.xfdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Initialize the Form facade with the source PDF
        using (Form form = new Form(pdfPath))
        {
            // Create the output stream for the XFDF file
            using (FileStream xfdfStream = new FileStream(xfdfPath, FileMode.Create, FileAccess.Write))
            {
                // Export all form field values to XFDF
                form.ExportXfdf(xfdfStream);
            }
        }

        Console.WriteLine($"Form data exported to '{xfdfPath}'.");
    }
}