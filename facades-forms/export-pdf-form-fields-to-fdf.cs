using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputFdfPath = "output.fdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Initialize the Form facade with the source PDF
            using (Form form = new Form(inputPdfPath))
            {
                // Create the output FDF file stream
                using (FileStream fdfStream = new FileStream(outputFdfPath, FileMode.Create, FileAccess.Write))
                {
                    // Export all form fields to the FDF stream
                    form.ExportFdf(fdfStream);
                }
            }

            Console.WriteLine($"Form fields exported successfully to '{outputFdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during export: {ex.Message}");
        }
    }
}