using System;
using System.IO;
using Aspose.Pdf; // Core Aspose.Pdf namespace

class Program
{
    static void Main()
    {
        const string inputOfdPath  = "input.ofd";
        const string outputPdfPath = "output.pdf";

        if (!File.Exists(inputOfdPath))
        {
            Console.Error.WriteLine($"File not found: {inputOfdPath}");
            return;
        }

        // Load the OFD file using the appropriate load options and convert to PDF
        using (Document doc = new Document(inputOfdPath, new OfdLoadOptions()))
        {
            // Save the resulting PDF with default settings
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"OFD converted to PDF: '{outputPdfPath}'");
    }
}