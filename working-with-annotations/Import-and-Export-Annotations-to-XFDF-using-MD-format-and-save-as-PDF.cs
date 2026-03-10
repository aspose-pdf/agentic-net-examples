using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string xfdfFile = "annotations.xfdf";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        // Load the source PDF and export its annotations to an XFDF file
        using (Document srcDoc = new Document(inputPdf))
        {
            srcDoc.ExportAnnotationsToXfdf(xfdfFile);
            Console.WriteLine($"Annotations exported to '{xfdfFile}'.");
        }

        // Load the PDF again (or a fresh copy) and import the annotations from XFDF
        using (Document dstDoc = new Document(inputPdf))
        {
            dstDoc.ImportAnnotationsFromXfdf(xfdfFile);
            dstDoc.Save(outputPdf);
            Console.WriteLine($"Annotations imported and PDF saved as '{outputPdf}'.");
        }
    }
}