using System;
using System.IO;
using Aspose.Pdf.Facades;

class ExportFormFieldsToFdf
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputFdf = "output.fdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        // Load the PDF form using the Form facade (handles PDF opening internally)
        using (Form form = new Form(inputPdf))
        {
            // Create a writable stream for the FDF output
            using (FileStream fdfStream = new FileStream(outputFdf, FileMode.Create, FileAccess.Write))
            {
                // Export all form fields to the FDF stream
                form.ExportFdf(fdfStream);
            }
        }

        Console.WriteLine($"Form fields exported to '{outputFdf}'.");
    }
}