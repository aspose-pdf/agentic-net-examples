using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Path to the source MHT file.
        const string mhtFilePath = "input.mht";
        // Path where the resulting PDF will be saved.
        const string pdfOutputPath = "output.pdf";

        if (!File.Exists(mhtFilePath))
        {
            Console.Error.WriteLine($"MHT file not found: {mhtFilePath}");
            return;
        }

        // Open the MHT file as a read‑only stream.
        using (FileStream mhtStream = File.OpenRead(mhtFilePath))
        {
            // Load options specific to MHT format.
            MhtLoadOptions loadOptions = new MhtLoadOptions();

            // Load the MHT content into a PDF Document.
            using (Document pdfDocument = new Document(mhtStream, loadOptions))
            {
                // Save the document as PDF.
                pdfDocument.Save(pdfOutputPath);
            }
        }

        Console.WriteLine($"MHT converted to PDF: {pdfOutputPath}");
    }
}