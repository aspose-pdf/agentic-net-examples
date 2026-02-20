using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Input PDF file path
        string inputPdfPath = args.Length > 0 ? args[0] : "input.pdf";
        // Output XFDF file path
        string outputXfdfPath = args.Length > 1 ? args[1] : "output.xfdf";

        // Validate input file existence
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: Input PDF file not found – '{inputPdfPath}'.");
            return;
        }

        try
        {
            // Initialize the Form facade and bind the PDF document
            Form form = new Form();
            form.BindPdf(inputPdfPath);

            // Export form field data to XFDF
            using (FileStream xfdfStream = new FileStream(outputXfdfPath, FileMode.Create, FileAccess.Write))
            {
                form.ExportXfdf(xfdfStream);
            }

            // Release resources
            form.Close();

            Console.WriteLine($"XFDF export completed successfully. File saved to '{outputXfdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred during XFDF export: {ex.Message}");
        }
    }
}