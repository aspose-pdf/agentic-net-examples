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
            Console.Error.WriteLine($"MHT file not found: {mhtPath}");
            return;
        }

        // Load options specific to MHT format
        MhtLoadOptions loadOptions = new MhtLoadOptions();

        // Open the MHT file as a PDF document
        using (Document pdfDoc = new Document(mhtPath, loadOptions))
        {
            // Save the resulting PDF
            pdfDoc.Save(pdfPath);
        }

        Console.WriteLine($"MHT successfully converted to PDF: {pdfPath}");
    }
}