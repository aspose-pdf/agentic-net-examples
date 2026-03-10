using System;
using System.IO;
using Aspose.Pdf;

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
            // Specify load options for PS format
            PsLoadOptions loadOptions = new PsLoadOptions();

            // Load the document from the stream using the PS load options
            using (Document doc = new Document(psStream, loadOptions))
            {
                // Save the loaded document as PDF (default format)
                doc.Save(outputPdf);
                Console.WriteLine($"Converted PS to PDF: {outputPdf}");
            }
        }
    }
}