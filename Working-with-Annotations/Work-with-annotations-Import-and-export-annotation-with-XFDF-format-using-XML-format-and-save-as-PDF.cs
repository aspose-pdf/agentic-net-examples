using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // needed for XfdfReader if you prefer stream API

class Program
{
    static void Main()
    {
        const string inputPdfPath   = "input.pdf";
        const string outputPdfPath  = "output.pdf";
        const string xfdfPath       = "annotations.xfdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Load the source PDF, export its annotations to XFDF, then import them back and save.
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // -------------------------------------------------
            // Export existing annotations to an XFDF file.
            // -------------------------------------------------
            pdfDoc.ExportAnnotationsToXfdf(xfdfPath);
            Console.WriteLine($"Annotations exported to '{xfdfPath}'.");

            // OPTIONAL: If you want to work with streams instead of file paths,
            // you could use the stream overloads:
            // using (FileStream xfdfStream = File.Create(xfdfPath))
            // {
            //     pdfDoc.ExportAnnotationsToXfdf(xfdfStream);
            // }

            // -------------------------------------------------
            // (Re)Import annotations from the XFDF file.
            // This demonstrates that the XFDF can be read back.
            // -------------------------------------------------
            pdfDoc.ImportAnnotationsFromXfdf(xfdfPath);
            Console.WriteLine($"Annotations imported from '{xfdfPath}'.");

            // Alternatively, using the static XfdfReader:
            // using (FileStream xfdfStream = File.OpenRead(xfdfPath))
            // {
            //     XfdfReader.ReadAnnotations(xfdfStream, pdfDoc);
            // }

            // Save the PDF with the (re)imported annotations.
            pdfDoc.Save(outputPdfPath);
            Console.WriteLine($"Resulting PDF saved to '{outputPdfPath}'.");
        }
    }
}