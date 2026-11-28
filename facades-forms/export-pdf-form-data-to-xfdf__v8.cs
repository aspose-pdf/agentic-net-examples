using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath  = "input.pdf";
        const string xfdfPath = "output.xfdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        try
        {
            // Form implements IDisposable – ensure deterministic disposal
            using (Form form = new Form())
            {
                // Bind the PDF document to the Form object
                form.BindPdf(pdfPath);

                // Export the form fields data to an XFDF file using a stream overload
                using (FileStream xfdfStream = File.Create(xfdfPath))
                {
                    form.ExportXfdf(xfdfStream);
                }
            }

            Console.WriteLine($"Form data successfully exported to '{xfdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
