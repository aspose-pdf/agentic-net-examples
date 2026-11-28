using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "FormA.pdf";
        const string xfdfPath = "FormA_form.xfdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {pdfPath}");
            return;
        }

        // Form implements IDisposable; using ensures proper resource cleanup
        using (Form form = new Form())
        {
            // Load the PDF that contains the interactive form
            form.BindPdf(pdfPath);

            // Export the form fields and values to an XFDF file.
            // ExportXfdf expects a Stream, so we open a FileStream for writing.
            using (FileStream xfdfStream = new FileStream(xfdfPath, FileMode.Create, FileAccess.Write))
            {
                form.ExportXfdf(xfdfStream);
            }
        }

        Console.WriteLine($"XFDF file created at '{xfdfPath}'.");
    }
}
