using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class ExportAccessibility
{
    static void Main()
    {
        // Input PDF that contains accessibility (tagged) information
        const string inputPath = "input.pdf";

        // Output PDF – a copy with ensured tagged content saved
        const string outputPdfPath = "output_accessible.pdf";

        // Optional: XFDF file to export all annotations (including accessibility‑related ones)
        const string xfdfPath = "annotations.xfdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the source PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Access the tagged‑content API
                ITaggedContent tagged = doc.TaggedContent;

                // Example: set language and title for the tagged PDF (optional)
                tagged.SetLanguage("en-US");
                tagged.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

                // Ensure that any modifications to the logical structure are written
                // to the PDF. This call updates MCID, BDC operators, etc.
                tagged.Save();

                // Export all annotations (including accessibility‑related ones) to XFDF
                // This creates a separate XFDF file that can be imported later.
                doc.ExportAnnotationsToXfdf(xfdfPath);

                // Finally, save the PDF (the tagged content is already synchronized)
                doc.Save(outputPdfPath);
            }

            Console.WriteLine($"Accessible PDF saved to '{outputPdfPath}'.");
            Console.WriteLine($"Annotations exported to XFDF file '{xfdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}