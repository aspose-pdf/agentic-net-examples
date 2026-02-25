using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // Included as requested

class Program
{
    static void Main()
    {
        const string psPath = "input.ps";
        const string outputPdf = "output.pdf";

        if (!File.Exists(psPath))
        {
            Console.Error.WriteLine($"File not found: {psPath}");
            return;
        }

        // Open the PostScript file as a stream
        using (FileStream psStream = File.OpenRead(psPath))
        {
            // Use the concrete load options for PostScript files
            PsLoadOptions loadOptions = new PsLoadOptions();

            // Load the stream into a PDF Document
            using (Document pdfDoc = new Document(psStream, loadOptions))
            {
                // Save the resulting PDF
                pdfDoc.Save(outputPdf);
                Console.WriteLine($"Converted PS to PDF: {outputPdf}");
            }
        }
    }
}