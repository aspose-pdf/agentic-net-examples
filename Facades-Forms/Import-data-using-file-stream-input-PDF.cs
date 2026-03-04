using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string fdfPath = "data.fdf";

        // Verify that the required files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(fdfPath))
        {
            Console.Error.WriteLine($"FDF data file not found: {fdfPath}");
            return;
        }

        // Open the source PDF as a stream and bind it to the Form facade
        using (FileStream pdfStream = File.OpenRead(inputPdfPath))
        using (Form form = new Form())
        {
            // Bind the PDF stream for editing
            form.BindPdf(pdfStream);

            // Import the FDF data from its stream into the bound PDF
            using (FileStream fdfStream = File.OpenRead(fdfPath))
            {
                form.ImportFdf(fdfStream);
            }

            // Save the modified PDF to the desired output file
            form.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF with imported data saved to '{outputPdfPath}'.");
    }
}