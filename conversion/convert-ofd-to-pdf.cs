using System;
using System.IO;
using Aspose.Pdf; // Core Aspose.Pdf namespace provides Document and load option classes

class Program
{
    static void Main()
    {
        const string inputOfdPath = "input.ofd";
        const string outputPdfPath = "output.pdf";

        if (!File.Exists(inputOfdPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputOfdPath}");
            return;
        }

        // Load the OFD file using OfdLoadOptions (default settings) and convert to PDF.
        using (Document doc = new Document(inputOfdPath, new OfdLoadOptions()))
        {
            // Save the resulting PDF. No additional SaveOptions are required for default settings.
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"OFD file converted to PDF successfully: {outputPdfPath}");
    }
}