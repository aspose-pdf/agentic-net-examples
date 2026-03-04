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

        // Initialize load options for MHT format
        MhtLoadOptions loadOptions = new MhtLoadOptions();

        // Load the MHT file as a PDF document using the constructor that accepts LoadOptions
        using (Document doc = new Document(mhtPath, loadOptions))
        {
            // Save the converted PDF
            doc.Save(pdfPath);
        }

        Console.WriteLine($"MHT file successfully converted to PDF: {pdfPath}");
    }
}