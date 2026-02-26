using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";      // PDF to which the annotation will be added
        const string xfdfPath      = "annotation.xfdf"; // XFDF (XML) file containing the markup annotation
        const string outputPdfPath = "output.pdf";     // Resulting PDF with the imported annotation

        // Verify that the source files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(xfdfPath))
        {
            Console.Error.WriteLine($"XFDF not found: {xfdfPath}");
            return;
        }

        // Load the PDF, import the XFDF annotations, and save the result
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Import all annotations defined in the XFDF file into the document
            pdfDoc.ImportAnnotationsFromXfdf(xfdfPath);

            // Save the updated PDF
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Annotations imported and saved to '{outputPdfPath}'.");
    }
}