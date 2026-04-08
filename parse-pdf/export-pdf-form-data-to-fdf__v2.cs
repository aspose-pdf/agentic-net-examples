using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputFdfPath = "output.fdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Create a FileStream for the FDF output
            using (FileStream fdfStream = new FileStream(outputFdfPath, FileMode.Create, FileAccess.Write))
            {
                // Use the Form facade to export form data to FDF
                using (Form formFacade = new Form())
                {
                    formFacade.BindPdf(pdfDoc);
                    formFacade.ExportFdf(fdfStream);
                }
            }
        }

        Console.WriteLine($"Form data exported to FDF file: {outputFdfPath}");
    }
}
