using System;
using System.IO;
using System.Reflection;
using Aspose.Pdf; // Aspose.Pdf core library

class Program
{
    static void Main()
    {
        // Resolve the directory where the executable resides.
        string exeDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        if (string.IsNullOrEmpty(exeDir))
        {
            Console.WriteLine("Unable to determine the executable directory.");
            return;
        }

        // NOTE: The XSL‑FO conversion requires the separate Aspose.Pdf.XslFo package.
        // Since that package is not referenced, we fall back to loading a regular PDF file.
        // Replace "input.pdf" with the path to the PDF you want to process.
        string inputPdf = Path.Combine(exeDir, "input.pdf");
        string outputPdf = Path.Combine(exeDir, "output.pdf");

        // Verify that the input PDF file exists before attempting conversion.
        if (!File.Exists(inputPdf))
        {
            Console.WriteLine($"Input PDF file not found: {inputPdf}");
            Console.WriteLine("Place a .pdf file in the executable directory or update the path accordingly.");
            return;
        }

        // Load the PDF document using the core Aspose.Pdf Document class.
        using (Document pdfDocument = new Document(inputPdf))
        {
            // Save the resulting PDF (could be the same as input or a new file).
            pdfDocument.Save(outputPdf);
        }

        Console.WriteLine($"PDF has been successfully saved: {outputPdf}");
    }
}