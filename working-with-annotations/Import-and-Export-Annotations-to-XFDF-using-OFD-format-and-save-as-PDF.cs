using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputOfd = "input.ofd";          // OFD source file
        const string xfdfPath = "annotations.xfdf";   // Temporary XFDF file
        const string outputPdf = "output.pdf";        // Resulting PDF

        if (!File.Exists(inputOfd))
        {
            Console.Error.WriteLine($"Input file not found: {inputOfd}");
            return;
        }

        // Load the OFD document using the appropriate load options.
        using (Document doc = new Document(inputOfd, new OfdLoadOptions()))
        {
            // Export all annotations present in the document to an XFDF file.
            doc.ExportAnnotationsToXfdf(xfdfPath);

            // (Optional) modify the XFDF file here before re‑importing.

            // Import the annotations back from the XFDF file into the document.
            doc.ImportAnnotationsFromXfdf(xfdfPath);

            // Save the document as a PDF file.
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Annotations exported to '{xfdfPath}' and PDF saved to '{outputPdf}'.");
    }
}