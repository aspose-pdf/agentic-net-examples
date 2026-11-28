using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "FormG.pdf";
        const string xfdfPath = "FormG_form.xfdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {pdfPath}");
            return;
        }

        // Form implements IDisposable, so wrap it in a using block for deterministic cleanup.
        using (Form form = new Form())
        {
            // Bind the source PDF that contains the form fields.
            form.BindPdf(pdfPath);

            // Export the form data to XFDF format. ExportXfdf expects a Stream, not a file path.
            using (FileStream xfdfStream = File.Create(xfdfPath))
            {
                form.ExportXfdf(xfdfStream);
            }
        }

        Console.WriteLine($"XFDF file created: {xfdfPath}");
    }
}
