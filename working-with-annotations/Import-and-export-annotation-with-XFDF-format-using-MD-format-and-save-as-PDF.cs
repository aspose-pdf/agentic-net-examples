using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string xfdfPath = "annotations.xfdf";
        const string outputPdfPath = "output.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdfPath))
        {
            // Export all existing annotations to an XFDF file
            doc.ExportAnnotationsToXfdf(xfdfPath);
            Console.WriteLine($"Annotations exported to '{xfdfPath}'.");

            // (Optional) modify the XFDF file here if needed
            // For demonstration we simply re-import the same XFDF

            // Import annotations from the XFDF file back into the document
            doc.ImportAnnotationsFromXfdf(xfdfPath);
            Console.WriteLine("Annotations imported from XFDF.");

            // Save the resulting PDF
            doc.Save(outputPdfPath);
            Console.WriteLine($"Resulting PDF saved to '{outputPdfPath}'.");
        }
    }
}