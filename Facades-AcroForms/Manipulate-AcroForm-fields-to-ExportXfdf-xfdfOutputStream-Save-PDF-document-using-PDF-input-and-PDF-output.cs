using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths for input PDF, output PDF and XFDF export
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string xfdfPath = "output.xfdf";

        // Verify that the input PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Create the Form facade and bind it to the source PDF
            using (Form form = new Form())
            {
                form.BindPdf(inputPdfPath);

                // Export AcroForm fields to XFDF using a file stream
                using (FileStream xfdfStream = new FileStream(xfdfPath, FileMode.Create, FileAccess.Write))
                {
                    form.ExportXfdf(xfdfStream);
                }

                // Save (optionally unchanged) PDF to the output location
                form.Save(outputPdfPath);
            }

            Console.WriteLine("XFDF export and PDF save completed successfully.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}