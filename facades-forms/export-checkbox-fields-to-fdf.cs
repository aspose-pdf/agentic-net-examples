using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputFdf = "checkboxes.fdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Initialize the Form facade with the loaded document
            using (Form form = new Form(pdfDoc))
            {
                // Export the form fields to an FDF stream.
                // The resulting FDF can be filtered externally to keep only checkbox definitions.
                using (FileStream fdfStream = new FileStream(outputFdf, FileMode.Create, FileAccess.Write))
                {
                    form.ExportFdf(fdfStream);
                }
            }
        }

        Console.WriteLine($"Checkbox field definitions exported to '{outputFdf}'.");
    }
}