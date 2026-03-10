using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths for input MHT, intermediate XFDF, and final PDF
        const string mhtPath = "input.mht";
        const string xfdfPath = "annotations.xfdf";
        const string pdfPath = "output.pdf";

        // Verify the MHT source exists
        if (!File.Exists(mhtPath))
        {
            Console.Error.WriteLine($"MHT file not found: {mhtPath}");
            return;
        }

        // Load the MHT file into a PDF document using MhtLoadOptions
        MhtLoadOptions loadOptions = new MhtLoadOptions();
        using (Document doc = new Document(mhtPath, loadOptions))
        {
            // Export any existing annotations to an XFDF file
            doc.ExportAnnotationsToXfdf(xfdfPath);

            // (Optional) Import annotations back from the XFDF file.
            // This step can be used after the XFDF is edited externally.
            doc.ImportAnnotationsFromXfdf(xfdfPath);

            // Save the resulting PDF document
            doc.Save(pdfPath);
        }

        Console.WriteLine($"PDF saved to '{pdfPath}'.");
        Console.WriteLine($"Annotations exported to '{xfdfPath}'.");
    }
}