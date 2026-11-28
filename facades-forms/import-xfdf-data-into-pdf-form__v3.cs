using System;
using System.IO;
using Aspose.Pdf.Facades; // Form class resides here

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";   // source PDF with form fields
        const string xfdfPath = "data.xfdf"; // XFDF file containing form data
        const string outputPath = "filled_output.pdf";

        // Verify that source files exist
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
            // Form implements IDisposable – wrap in using for deterministic disposal
            using (Form form = new Form())
            {
                // Bind the PDF document that contains the form fields (expects a Stream)
                using (FileStream pdfStream = File.OpenRead(pdfPath))
                {
                    form.BindPdf(pdfStream);
                }

                // Import form data from the XFDF file into the bound PDF (expects a Stream)
                using (FileStream xfdfStream = File.OpenRead(xfdfPath))
                {
                    form.ImportXfdf(xfdfStream);
                }

                // Save the resulting PDF with populated form fields
                form.Save(outputPath);
            }

            Console.WriteLine($"Form data imported and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
