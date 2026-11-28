using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "FormF.pdf";
        const string xfdfPath = "FormF_form.xfdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {pdfPath}");
            return;
        }

        try
        {
            // Form implements IDisposable, so wrap it in a using block.
            using (Form form = new Form())
            {
                // Bind the PDF that contains the form fields.
                form.BindPdf(pdfPath);

                // Export the form data to XFDF using a FileStream (ExportXfdf expects a Stream).
                using (FileStream xfdfStream = new FileStream(xfdfPath, FileMode.Create, FileAccess.Write))
                {
                    form.ExportXfdf(xfdfStream);
                }
            }

            Console.WriteLine($"XFDF exported successfully to '{xfdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during XFDF export: {ex.Message}");
        }
    }
}
