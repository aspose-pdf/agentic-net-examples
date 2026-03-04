using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string inputFdfPath = "data.fdf";
        const string outputPdfPath = "output.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(inputFdfPath))
        {
            Console.Error.WriteLine($"FDF file not found: {inputFdfPath}");
            return;
        }

        try
        {
            // Initialize the Form facade with the source PDF
            using (Form form = new Form(inputPdfPath))
            {
                // Import field values from the FDF file
                using (FileStream fdfStream = new FileStream(inputFdfPath, FileMode.Open, FileAccess.Read))
                {
                    form.ImportFdf(fdfStream);
                }

                // Save the updated PDF to the desired output file
                form.Save(outputPdfPath);
            }

            Console.WriteLine($"FDF data successfully imported into PDF: {outputPdfPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}