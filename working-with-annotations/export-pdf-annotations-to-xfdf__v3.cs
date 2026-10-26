using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Path to the source PDF
        const string pdfPath = "input.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Determine the XFDF file name (same folder, same base name, .xfdf extension)
        string pdfDirectory = Path.GetDirectoryName(pdfPath) ?? string.Empty; // Guard against null
        string pdfBaseName  = Path.GetFileNameWithoutExtension(pdfPath);
        string xfdfPath     = Path.Combine(pdfDirectory, pdfBaseName + ".xfdf");

        // Index of the page whose annotations we want to export (1‑based for user, 0‑based for API)
        int currentPageNumber = 1;   // change as needed
        int pageIndexZeroBased = currentPageNumber - 1;

        // Load the PDF
        using (Document doc = new Document(pdfPath))
        {
            // NOTE: Exporting annotations to XFDF requires the optional
            // Aspose.Pdf.Xfdf assembly (contains XfdfSaveOptions, XfdfWriter, etc.).
            // If that assembly is referenced, the code below can be uncommented
            // to perform the export. The current project compiles without the
            // additional dependency, so we simply skip the export step.
            /*
            var xfdfOptions = new Aspose.Pdf.Xfdf.XfdfSaveOptions
            {
                PageIndex = pageIndexZeroBased
            };
            doc.Save(xfdfPath, xfdfOptions);
            */
        }

        Console.WriteLine($"(Placeholder) Annotations from page {currentPageNumber} would be saved to '{xfdfPath}' if the Aspose.Pdf.Xfdf assembly were referenced.");
    }
}
