using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPcl = "input.pcl";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPcl))
        {
            Console.Error.WriteLine($"File not found: {inputPcl}");
            return;
        }

        // Initialize load options. HP‑GL/2 vectors are loaded automatically, so no extra property is required.
        PclLoadOptions loadOptions = new PclLoadOptions();

        using (Document pdfDoc = new Document(inputPcl, loadOptions))
        {
            pdfDoc.Save(outputPdf);
        }

        Console.WriteLine($"PCL converted to PDF: {outputPdf}");
    }
}
