using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Paths (replace with actual locations)
        string dataDir = "YOUR_DATA_DIRECTORY";
        string xslFoPath = Path.Combine(dataDir, "input.xslfo");
        string pdfPath = Path.Combine(dataDir, "output.pdf");
        string xfdfPath = Path.Combine(dataDir, "annotations.xfdf");

        if (!File.Exists(xslFoPath))
        {
            Console.Error.WriteLine($"XSL-FO file not found: {xslFoPath}");
            return;
        }

        // Load XSL-FO file into a PDF document
        XslFoLoadOptions loadOptions = new XslFoLoadOptions();
        using (Document pdfDoc = new Document(xslFoPath, loadOptions))
        {
            // Export any existing annotations to an XFDF file
            pdfDoc.ExportAnnotationsToXfdf(xfdfPath);

            // Import the same XFDF annotations back into the document
            pdfDoc.ImportAnnotationsFromXfdf(xfdfPath);

            // Save the final PDF document
            pdfDoc.Save(pdfPath);
        }

        Console.WriteLine($"PDF saved to '{pdfPath}'.");
        Console.WriteLine($"XFDF saved to '{xfdfPath}'.");
    }
}