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
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Export all annotations from the source PDF to an XFDF file
        using (Document sourceDoc = new Document(inputPdf))
        {
            sourceDoc.ExportAnnotationsToXfdf(xfdfFile);
        }

        // Import the previously exported XFDF annotations into a new PDF and save it
        using (Document targetDoc = new Document(inputPdf))
        {
            targetDoc.ImportAnnotationsFromXfdf(xfdfFile);
            targetDoc.Save(outputPdf);
        }

        Console.WriteLine($"Annotations exported to '{xfdfFile}' and imported into '{outputPdf}'.");
    }
}