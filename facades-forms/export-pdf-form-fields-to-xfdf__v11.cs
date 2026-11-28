using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "XFDFSample.pdf";
        const string xfdfExportPath = "DataExport.xfdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {pdfPath}");
            return;
        }

        // Form implements IDisposable, so wrap it in a using block.
        using (Form form = new Form())
        {
            // Bind the PDF document that contains the form fields.
            form.BindPdf(pdfPath);

            // Export the current form field values to an XFDF file.
            // ExportXfdf expects a Stream, not a file path.
            using (FileStream xfdfStream = File.Create(xfdfExportPath))
            {
                form.ExportXfdf(xfdfStream);
            }

            // Close the bound PDF file (optional when using 'using', but follows API contract).
            form.Close();
        }

        Console.WriteLine($"Form fields exported to '{xfdfExportPath}'.");
    }
}
