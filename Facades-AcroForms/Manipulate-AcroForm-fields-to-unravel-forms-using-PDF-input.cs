using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Input and output PDF file paths
        string inputPdf = "input.pdf";
        string outputPdf = "output_unraveled.pdf";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Error: Input file not found – {inputPdf}");
            return;
        }

        try
        {
            // Initialize the Form facade
            using (Form form = new Form())
            {
                // Bind the existing PDF document
                form.BindPdf(inputPdf);

                // Flatten (unravel) all AcroForm fields
                form.FlattenAllFields();

                // Save the modified PDF
                form.Save(outputPdf);
            }

            Console.WriteLine($"Successfully unraveled form fields. Output saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}