using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Expected arguments: input PDF path, input FDF path, output PDF path
        if (args.Length < 3)
        {
            Console.WriteLine("Usage: <program> <inputPdf> <inputFdf> <outputPdf>");
            return;
        }

        string inputPdfPath = args[0];
        string inputFdfPath = args[1];
        string outputPdfPath = args[2];

        // Verify that the input files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.WriteLine($"Error: PDF file not found – {inputPdfPath}");
            return;
        }

        if (!File.Exists(inputFdfPath))
        {
            Console.WriteLine($"Error: FDF file not found – {inputFdfPath}");
            return;
        }

        try
        {
            // Open the FDF stream
            using (FileStream fdfStream = File.OpenRead(inputFdfPath))
            {
                // Initialize the Form facade and bind the PDF document
                using (Form form = new Form())
                {
                    form.BindPdf(inputPdfPath);

                    // Import the FDF data into the PDF form
                    form.ImportFdf(fdfStream);

                    // Save the updated PDF document (uses the document-save rule)
                    form.Save(outputPdfPath);
                }
            }

            Console.WriteLine($"Successfully saved updated PDF to: {outputPdfPath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}