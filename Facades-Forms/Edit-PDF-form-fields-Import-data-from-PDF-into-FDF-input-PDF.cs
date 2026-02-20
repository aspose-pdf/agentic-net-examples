using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Expect input PDF path and output FDF path as arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <input.pdf> <output.fdf>");
            return;
        }

        string pdfPath = args[0];
        string fdfPath = args[1];

        // Verify that the input PDF exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found: {pdfPath}");
            return;
        }

        try
        {
            // Initialize the Form facade with the PDF document
            Form form = new Form(pdfPath);

            // Export the form fields to an FDF file
            using (FileStream fdfStream = new FileStream(fdfPath, FileMode.Create, FileAccess.Write))
            {
                form.ExportFdf(fdfStream);
            }

            Console.WriteLine($"Form data successfully exported to: {fdfPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred during export: {ex.Message}");
        }
    }
}