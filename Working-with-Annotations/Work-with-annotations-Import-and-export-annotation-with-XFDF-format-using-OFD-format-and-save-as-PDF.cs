using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string ofdPath   = "input.ofd";
        const string xfdfPath  = "annotations.xfdf";
        const string outputPdf = "output.pdf";

        if (!File.Exists(ofdPath))
        {
            Console.Error.WriteLine($"File not found: {ofdPath}");
            return;
        }

        // Load the OFD file into a PDF Document (OFdLoadOptions is the appropriate load option)
        using (Document sourceDoc = new Document(ofdPath, new OfdLoadOptions()))
        {
            // Export any existing annotations to an XFDF file
            sourceDoc.ExportAnnotationsToXfdf(xfdfPath);
        }

        // Create a new PDF document and import the exported XFDF annotations
        using (Document targetDoc = new Document())
        {
            // Ensure there is at least one page to host the imported annotations
            targetDoc.Pages.Add();

            // Import annotations from the XFDF file
            targetDoc.ImportAnnotationsFromXfdf(xfdfPath);

            // Save the resulting PDF
            targetDoc.Save(outputPdf);
        }

        Console.WriteLine($"Annotations exported to '{xfdfPath}' and imported into '{outputPdf}'.");
    }
}