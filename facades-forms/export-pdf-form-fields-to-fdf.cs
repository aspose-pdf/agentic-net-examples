using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputFdf = "output.fdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        try
        {
            // Initialize the Form facade with the source PDF.
            using (Form form = new Form(inputPdf))
            {
                // Create a file stream for the FDF output.
                using (FileStream fdfStream = new FileStream(outputFdf, FileMode.Create, FileAccess.Write))
                {
                    // Export all form fields to the FDF stream.
                    form.ExportFdf(fdfStream);
                }
            }

            Console.WriteLine($"Form fields exported to FDF: {outputFdf}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}