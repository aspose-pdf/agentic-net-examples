using System;
using System.IO;
using Aspose.Pdf;                 // Core PDF API
using Aspose.Pdf.Annotations;    // Provides XfdfReader (optional)

class Program
{
    static void Main()
    {
        const string pdfPath     = "input.pdf";      // Source PDF
        const string xfdfPath    = "annotations.xfdf"; // XFDF file containing annotations
        const string outputPath  = "output.pdf";     // Resulting PDF

        // Verify input files exist
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }
        if (!File.Exists(xfdfPath))
        {
            Console.Error.WriteLine($"XFDF file not found: {xfdfPath}");
            return;
        }

        // Load the PDF document (lifecycle: load)
        using (Document doc = new Document(pdfPath))
        {
            // Option 1: Use Document's built‑in method
            doc.ImportAnnotationsFromXfdf(xfdfPath);

            // Option 2 (alternative): use XfdfReader with a stream
            // using (FileStream xfdfStream = File.OpenRead(xfdfPath))
            // {
            //     XfdfReader.ReadAnnotations(xfdfStream, doc);
            // }

            // Save the modified PDF (lifecycle: save)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Annotations imported successfully. Saved to '{outputPath}'.");
    }
}