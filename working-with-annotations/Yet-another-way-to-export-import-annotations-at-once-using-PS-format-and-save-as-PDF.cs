using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the source PDF inside a using block for deterministic disposal.
        using (Document doc = new Document(inputPdf))
        {
            // Export all annotations to an in‑memory XFDF stream.
            using (MemoryStream xfdfStream = new MemoryStream())
            {
                doc.ExportAnnotationsToXfdf(xfdfStream);
                xfdfStream.Position = 0; // rewind for reading

                // (Optional) Clear existing annotations if you want to demonstrate a round‑trip.
                // doc.Pages.ForEach(p => p.Annotations.Clear());

                // Import the annotations back from the XFDF stream.
                doc.ImportAnnotationsFromXfdf(xfdfStream);
            }

            // Save the resulting PDF. No PS format is used because Aspose.Pdf does not support
            // saving to PostScript; annotations are handled via XFDF instead.
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Annotations exported/imported and PDF saved to '{outputPdf}'.");
    }
}