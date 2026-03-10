using System;
using System.IO;
using Aspose.Pdf;
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

        // Load the PDF document (deterministic disposal)
        using (Document doc = new Document(inputPdf))
        {
            // Initialize the Form facade on the loaded document
            using (Form form = new Form(doc))
            {
                // Export AcroForm field data to an FDF file
                using (FileStream fdfStream = new FileStream(outputFdf, FileMode.Create, FileAccess.Write))
                {
                    form.ExportFdf(fdfStream);
                }
            }
        }

        Console.WriteLine($"Form data exported to FDF: {outputFdf}");
    }
}