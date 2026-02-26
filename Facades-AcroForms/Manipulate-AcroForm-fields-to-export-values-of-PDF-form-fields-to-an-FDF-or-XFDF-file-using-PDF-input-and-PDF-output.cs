using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";   // source PDF with AcroForm fields
        const string outputFdf = "output.fdf";  // destination FDF file
        const string outputXfdf = "output.xfdf"; // destination XFDF file

        // Verify that the input PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        // Initialize the Form facade on the PDF document.
        // Form inherits from SaveableFacade which implements IDisposable,
        // so we wrap it in a using block for deterministic cleanup.
        using (Form form = new Form(inputPdf))
        {
            // Export the current values of all form fields to an FDF stream.
            using (FileStream fdfStream = new FileStream(outputFdf, FileMode.Create, FileAccess.Write))
            {
                form.ExportFdf(fdfStream);
            }

            // Export the same data to an XFDF (XML) stream.
            using (FileStream xfdfStream = new FileStream(outputXfdf, FileMode.Create, FileAccess.Write))
            {
                form.ExportXfdf(xfdfStream);
            }

            // No additional Save() call is required; ExportFdf/ExportXfdf write directly to the streams.
        }

        Console.WriteLine($"Form field values exported to '{outputFdf}' and '{outputXfdf}'.");
    }
}