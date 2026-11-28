using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "XFDFTarget.pdf";
        const string xfdfPath = "DataImport.xfdf";
        const string outputPath = "ImportedXfdf.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }
        if (!File.Exists(xfdfPath))
        {
            Console.Error.WriteLine($"XFDF file not found: {xfdfPath}");
            return;
        }

        try
        {
            // Form facade implements IDisposable, ensure deterministic disposal
            using (Form form = new Form())
            {
                // Bind the target PDF that contains the form fields
                form.BindPdf(pdfPath);

                // Import field values from the XFDF file – ImportXfdf expects a Stream
                using (FileStream xfdfStream = File.OpenRead(xfdfPath))
                {
                    form.ImportXfdf(xfdfStream);
                }

                // Save the resulting PDF with imported data
                form.Save(outputPath);
            }

            Console.WriteLine($"Imported XFDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
