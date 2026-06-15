using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputFdfPath = "output.fdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Initialize the Form facade (fully qualified, no using Aspose.Pdf.Facades)
            var formFacade = new Aspose.Pdf.Facades.Form(pdfDocument);

            // Export form fields to an FDF file via a FileStream
            using (FileStream fdfStream = new FileStream(outputFdfPath, FileMode.Create, FileAccess.Write))
            {
                formFacade.ExportFdf(fdfStream);
                // The using block ensures the stream is closed
            }
        }

        Console.WriteLine($"Form data exported to '{outputFdfPath}'.");
    }
}