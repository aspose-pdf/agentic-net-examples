using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string mhtPath = "input.mht";
        const string pdfPath = "output.pdf";

        if (!File.Exists(mhtPath))
        {
            Console.Error.WriteLine($"File not found: {mhtPath}");
            return;
        }

        // Load options for MHT format
        MhtLoadOptions loadOptions = new MhtLoadOptions();

        // Open the MHT file and convert it to a PDF document
        using (Document pdfDocument = new Document(mhtPath, loadOptions))
        {
            // Save the resulting PDF
            pdfDocument.Save(pdfPath);
        }

        Console.WriteLine($"MHT file has been converted and saved as PDF: '{pdfPath}'");
    }
}