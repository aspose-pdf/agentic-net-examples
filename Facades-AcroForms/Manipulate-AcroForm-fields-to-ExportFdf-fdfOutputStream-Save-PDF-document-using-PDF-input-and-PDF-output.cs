using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Expected arguments: input PDF path, output PDF path, output FDF path
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: <inputPdf> <outputPdf> <outputFdf>");
            return;
        }

        string inputPdfPath = args[0];
        string outputPdfPath = args[1];
        string outputFdfPath = args[2];

        // Verify that the input PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Initialize the Form facade and bind the PDF document
            using (Form form = new Form())
            {
                form.BindPdf(inputPdfPath);

                // Export AcroForm fields to an FDF stream
                using (FileStream fdfStream = new FileStream(outputFdfPath, FileMode.Create, FileAccess.Write))
                {
                    form.ExportFdf(fdfStream);
                }

                // Save (unchanged) PDF to the specified output path
                form.Save(outputPdfPath);
            }

            Console.WriteLine("PDF saved and form fields exported to FDF successfully.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}