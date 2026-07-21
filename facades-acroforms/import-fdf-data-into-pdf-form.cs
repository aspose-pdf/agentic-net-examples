using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath   = "form.pdf";      // source PDF with form fields
        const string fdfPath   = "data.fdf";      // FDF file containing field values
        const string outputPath = "form_filled.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }
        if (!File.Exists(fdfPath))
        {
            Console.Error.WriteLine($"FDF not found: {fdfPath}");
            return;
        }

        try
        {
            // Initialize the Form facade with the source PDF
            using (Form form = new Form(pdfPath))
            {
                // Open the FDF stream and import the data into the PDF form
                using (FileStream fdfStream = new FileStream(fdfPath, FileMode.Open, FileAccess.Read))
                {
                    form.ImportFdf(fdfStream);
                }

                // Save the updated PDF to a new file
                form.Save(outputPath);
            }

            Console.WriteLine($"Form data imported successfully. Output saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}