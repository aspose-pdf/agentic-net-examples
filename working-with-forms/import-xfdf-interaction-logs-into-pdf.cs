using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // for XfdfReader if needed

class Program
{
    static void Main()
    {
        const string pdfPath = "viewer.pdf";                 // Existing PDF to which logs will be applied
        const string xfdfPath = "interaction_logs.xfdf";     // XML‑based XFDF file containing interaction logs
        const string outputPath = "viewer_with_logs.pdf";    // Resulting PDF with imported annotations

        // Verify that required files exist
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

        try
        {
            // Load the target PDF document
            using (Document doc = new Document(pdfPath))
            {
                // Option 1: Directly import annotations from the XFDF file (XML format)
                doc.ImportAnnotationsFromXfdf(xfdfPath);

                // Option 2 (alternative): import via stream using XfdfReader
                // using (FileStream xfdfStream = File.OpenRead(xfdfPath))
                // {
                //     XfdfReader.ReadAnnotations(xfdfStream, doc);
                // }

                // Save the modified PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Interaction logs imported successfully. Output saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during processing: {ex.Message}");
        }
    }
}