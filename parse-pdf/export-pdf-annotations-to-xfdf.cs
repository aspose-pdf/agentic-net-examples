using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string xfdfPath = "output.xfdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF document and export its form/annotation data to XFDF.
        // Both the Document and the FileStream are wrapped in using statements so they are disposed (closed) automatically.
        using (Document doc = new Document(inputPdf))
        using (FileStream fs = new FileStream(xfdfPath, FileMode.Create, FileAccess.Write))
        {
            // The core API provides ExportAnnotationsToXfdf for writing XFDF data to a stream.
            doc.ExportAnnotationsToXfdf(fs);
        }

        Console.WriteLine($"XFDF data exported to '{xfdfPath}'.");
    }
}
