using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputMht = "input.mht";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputMht))
        {
            Console.Error.WriteLine($"Input file not found: {inputMht}");
            return;
        }

        // Load the MHTML file with default options
        var loadOptions = new MhtLoadOptions();
        using var doc = new Document(inputMht, loadOptions);

        // Configure page size and margins after the document is loaded
        doc.PageInfo.Width = 595f;   // A4 width in points (≈210 mm)
        doc.PageInfo.Height = 842f;  // A4 height in points (≈297 mm)
        doc.PageInfo.Margin = new MarginInfo(10, 10, 10, 10);

        // Save as PDF
        doc.Save(outputPdf);

        Console.WriteLine($"MHTML converted to PDF: {outputPdf}");
    }
}
