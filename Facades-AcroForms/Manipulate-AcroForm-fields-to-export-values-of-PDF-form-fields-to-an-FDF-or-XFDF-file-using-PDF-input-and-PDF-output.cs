using System;
using System.IO;
using Aspose.Pdf.Facades;

class ExportFormData
{
    static void Main(string[] args)
    {
        // Input PDF with AcroForm fields
        const string inputPdfPath = "input.pdf";

        // Output files
        const string outputPdfPath = "output.pdf";   // optional copy of the PDF
        const string fdfPath = "output.fdf";
        const string xfdfPath = "output.xfdf";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: Input PDF not found at '{inputPdfPath}'.");
            return;
        }

        try
        {
            // Initialize the Form facade and bind the PDF document
            using (Form form = new Form())
            {
                form.BindPdf(inputPdfPath);

                // Export form field values to FDF
                using (FileStream fdfStream = new FileStream(fdfPath, FileMode.Create, FileAccess.Write))
                {
                    form.ExportFdf(fdfStream);
                }

                // Export form field values to XFDF (XML based)
                using (FileStream xfdfStream = new FileStream(xfdfPath, FileMode.Create, FileAccess.Write))
                {
                    form.ExportXfdf(xfdfStream);
                }

                // Optional: save the (unchanged) PDF to a new file
                form.Save(outputPdfPath);
            }

            Console.WriteLine("Form data exported successfully:");
            Console.WriteLine($"  FDF  -> {fdfPath}");
            Console.WriteLine($"  XFDF -> {xfdfPath}");
            Console.WriteLine($"  PDF  -> {outputPdfPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}