using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string psInputPath = "input.ps";
        const string pdfOutputPath = "output.pdf";

        if (!File.Exists(psInputPath))
        {
            Console.Error.WriteLine($"File not found: {psInputPath}");
            return;
        }

        // Open the PostScript file as a stream and load it using PsLoadOptions.
        using (FileStream psStream = File.OpenRead(psInputPath))
        {
            PsLoadOptions loadOptions = new PsLoadOptions();

            // Document constructor that accepts a stream and load options.
            using (Document doc = new Document(psStream, loadOptions))
            {
                // Save the loaded document as PDF.
                doc.Save(pdfOutputPath);
                Console.WriteLine($"Converted PS to PDF: {pdfOutputPath}");
            }
        }
    }
}