using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "FormC.pdf";
        const string outputXfdf = "FormC_form.xfdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Form implements IDisposable, so wrap it in a using block for deterministic cleanup.
        using (Form form = new Form())
        {
            // Bind the PDF that contains the form fields.
            form.BindPdf(inputPdf);

            // Export the form data to XFDF format. ExportXfdf expects a Stream, not a file path.
            using (FileStream xfdfStream = File.Create(outputXfdf))
            {
                form.ExportXfdf(xfdfStream);
            }
        }

        Console.WriteLine($"XFDF file created: {outputXfdf}");
    }
}
