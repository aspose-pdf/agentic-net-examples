using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input_form.pdf";
        const string fdfPath = "data.fdf";
        const string outputPath = "filled_form.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        if (!File.Exists(fdfPath))
        {
            Console.Error.WriteLine($"FDF file not found: {fdfPath}");
            return;
        }

        try
        {
            // Initialize the Form facade with the source PDF
            using (Form form = new Form(pdfPath))
            {
                // Open the FDF file as a stream and import its data into the PDF form
                using (FileStream fdfStream = new FileStream(fdfPath, FileMode.Open, FileAccess.Read))
                {
                    form.ImportFdf(fdfStream);
                }

                // Save the updated PDF with imported form data
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