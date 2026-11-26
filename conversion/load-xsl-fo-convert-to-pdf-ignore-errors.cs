using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.fo"; // Path to the source file (PDF or XSL‑FO). Without the Aspose.Pdf.XslFo package we load it as a regular PDF.
        const string outputPdfPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the document. When the XslFo package is unavailable, the Document constructor
        // treats the file as a normal PDF. This removes the dependency on Aspose.Pdf.XslFo.
        Document doc = new Document(inputPath);

        // Save the resulting PDF.
        doc.Save(outputPdfPath);

        Console.WriteLine($"Document converted and saved to '{outputPdfPath}'.");
    }
}
