using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "FormH.pdf";
        const string xfdfPath = "FormH_form.xfdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {pdfPath}");
            return;
        }

        // Form implements IDisposable, so wrap it in a using block for deterministic cleanup.
        using (Form form = new Form())
        {
            // Bind the PDF that contains the form fields.
            form.BindPdf(pdfPath);

            // Export the form data to XFDF format using a FileStream (ExportXfdf expects a Stream).
            using (FileStream xfdfStream = new FileStream(xfdfPath, FileMode.Create, FileAccess.Write))
            {
                form.ExportXfdf(xfdfStream);
            }
        }

        Console.WriteLine($"XFDF file created: {xfdfPath}");
    }
}
