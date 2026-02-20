using System;
using System.IO;
using Aspose.Pdf.Facades;

class ImportFdfToPdf
{
    static void Main(string[] args)
    {
        // Expected arguments: input PDF path, input FDF path, output PDF path
        if (args.Length < 3)
        {
            Console.WriteLine("Usage: ImportFdfToPdf <inputPdf> <inputFdf> <outputPdf>");
            return;
        }

        string pdfPath = args[0];
        string fdfPath = args[1];
        string outputPath = args[2];

        // Verify that the source files exist
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found – {pdfPath}");
            return;
        }

        if (!File.Exists(fdfPath))
        {
            Console.Error.WriteLine($"Error: FDF file not found – {fdfPath}");
            return;
        }

        try
        {
            // Create the Form facade and bind it to the PDF document
            using (Form form = new Form())
            {
                form.BindPdf(pdfPath); // load PDF

                // Import the FDF data into the PDF
                using (FileStream fdfStream = new FileStream(fdfPath, FileMode.Open, FileAccess.Read))
                {
                    form.ImportFdf(fdfStream);
                }

                // Save the modified PDF to the specified output path
                form.Save(outputPath);
            }

            Console.WriteLine($"Successfully imported FDF data. Output saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}