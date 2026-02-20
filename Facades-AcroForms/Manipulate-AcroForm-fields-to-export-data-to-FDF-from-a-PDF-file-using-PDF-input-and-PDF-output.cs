using System;
using System.IO;
using Aspose.Pdf.Facades;

class ExportAcroFormToFdf
{
    static void Main(string[] args)
    {
        // Input PDF file containing AcroForm fields
        const string inputPdfPath = "input.pdf";
        // Output FDF file where the form data will be exported
        const string outputFdfPath = "output.fdf";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found – {inputPdfPath}");
            return;
        }

        try
        {
            // Initialize the Form facade and bind it to the PDF document
            using (Form form = new Form())
            {
                form.BindPdf(inputPdfPath);

                // Export the form fields to an FDF stream and write it to a file
                using (FileStream fdfStream = new FileStream(outputFdfPath, FileMode.Create, FileAccess.Write))
                {
                    form.ExportFdf(fdfStream);
                }
            }

            Console.WriteLine($"Form data successfully exported to FDF: {outputFdfPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}