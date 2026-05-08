using System;
using System.IO;
using Aspose.Pdf;

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

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Bind the document to the Form facade (fully qualified to avoid using Aspose.Pdf.Facades namespace)
            using (Aspose.Pdf.Facades.Form form = new Aspose.Pdf.Facades.Form(pdfDoc))
            {
                // Open a file stream for the FDF output
                using (FileStream fdfStream = new FileStream(outputFdf, FileMode.Create, FileAccess.Write))
                {
                    // Export the form fields to the FDF stream
                    form.ExportFdf(fdfStream);
                    // The using block ensures the stream is closed
                }
                // The using block ensures the Form facade is disposed
            }
            // The using block ensures the Document is disposed
        }

        Console.WriteLine($"Form data exported to FDF: {outputFdf}");
    }
}