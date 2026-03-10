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

        // Verify that the source PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // ---------- Export annotations ----------
        // Load the PDF, export all annotations to an XFDF file, then dispose the document
        using (Document doc = new Document(inputPdf))
        {
            // ExportAnnotationsToXfdf writes all annotation data to the specified XFDF file
            doc.ExportAnnotationsToXfdf(xfdfFile);
        }

        // ---------- Import annotations ----------
        // Load the PDF again (or a different PDF), import the previously saved XFDF,
        // and save the result as a new PDF file
        using (Document doc = new Document(inputPdf))
        {
            // ImportAnnotationsFromXfdf reads annotation data from the XFDF file
            doc.ImportAnnotationsFromXfdf(xfdfFile);

            // Save the document with the imported annotations; PDF format is the default
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Annotations exported to '{xfdfFile}' and re‑imported. Result saved as '{outputPdf}'.");
    }
}