using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "FormD.pdf";
        const string outputXfdf = "FormD_form.xfdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        try
        {
            // Form implements IDisposable, so wrap it in a using block
            using (Form form = new Form())
            {
                // Bind the PDF containing the form fields
                form.BindPdf(inputPdf);

                // Export the form data to XFDF format – ExportXfdf expects a Stream, not a file path
                using (FileStream xfdfStream = File.Create(outputXfdf))
                {
                    form.ExportXfdf(xfdfStream);
                }
            }

            Console.WriteLine($"XFDF exported to '{outputXfdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
