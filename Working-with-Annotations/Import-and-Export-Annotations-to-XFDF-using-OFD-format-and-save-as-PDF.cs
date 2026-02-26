using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string ofdPath   = "input.ofd";      // source OFD file
        const string xfdfPath  = "annotations.xfdf"; // intermediate XFDF file
        const string outputPdf = "output.pdf";      // final PDF file

        if (!File.Exists(ofdPath))
        {
            Console.Error.WriteLine($"File not found: {ofdPath}");
            return;
        }

        // Load the OFD document.  OfdLoadOptions is the appropriate load options class.
        var ofdLoadOptions = new OfdLoadOptions();

        // Ensure deterministic disposal of the Document.
        using (Document doc = new Document(ofdPath, ofdLoadOptions))
        {
            // Export any existing annotations to an XFDF file.
            doc.ExportAnnotationsToXfdf(xfdfPath);

            // (Optional) you could modify or clear annotations here.

            // Import the annotations back from the XFDF file.
            doc.ImportAnnotationsFromXfdf(xfdfPath);

            // Save the result as a PDF.  Document.Save(string) always writes PDF.
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Annotations exported to '{xfdfPath}' and PDF saved to '{outputPdf}'.");
    }
}