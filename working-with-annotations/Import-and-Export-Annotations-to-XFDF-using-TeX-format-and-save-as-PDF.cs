using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // for XFDF related types if needed

class Program
{
    static void Main()
    {
        // Paths for input TeX, intermediate XFDF, and final PDF
        const string texPath = "input.tex";
        const string xfdfPath = "annotations.xfdf";
        const string outputPdf = "output.pdf";

        // Verify the TeX source exists
        if (!File.Exists(texPath))
        {
            Console.Error.WriteLine($"TeX file not found: {texPath}");
            return;
        }

        // Load the TeX file and convert it to a PDF document
        TeXLoadOptions texLoadOptions = new TeXLoadOptions();
        using (Document doc = new Document(texPath, texLoadOptions))
        {
            // Export any existing annotations to an XFDF file
            doc.ExportAnnotationsToXfdf(xfdfPath);

            // Import the same XFDF back into the document (demonstrates round‑trip)
            doc.ImportAnnotationsFromXfdf(xfdfPath);

            // Save the resulting document as a PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved to '{outputPdf}'. Annotations exported/imported via '{xfdfPath}'.");
    }
}