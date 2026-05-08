using System;
using System.IO;
using Aspose.Pdf; // Core API for Document

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

        // Load the PDF document using the core Document class
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Bind the document to the Facade Form (fully qualified, no using directive)
            using (Aspose.Pdf.Facades.Form form = new Aspose.Pdf.Facades.Form(pdfDoc))
            {
                // Export the form fields to an FDF file via a FileStream
                using (FileStream fdfStream = new FileStream(outputFdfPath, FileMode.Create, FileAccess.Write))
                {
                    form.ExportFdf(fdfStream);
                }
                // The PDF itself is unchanged; no additional Save call is required
            }
        }

        Console.WriteLine($"Form data successfully exported to '{outputFdfPath}'.");
    }
}