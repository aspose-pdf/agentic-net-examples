using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Input OFD file (OFD is supported only as an input format)
        const string inputOfdPath = "input.ofd";
        // Output PDF file (the document will be saved as PDF)
        const string outputPdfPath = "output.pdf";
        // XFDF file to export the annotations
        const string xfdfPath = "annotations.xfdf";

        if (!File.Exists(inputOfdPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputOfdPath}");
            return;
        }

        // Load the OFD file using the appropriate load options
        using (Document doc = new Document(inputOfdPath, new OfdLoadOptions()))
        {
            // Ensure the document has at least one page
            if (doc.Pages.Count == 0)
            {
                Console.Error.WriteLine("The loaded document contains no pages.");
                return;
            }

            // Add a simple text annotation to the first page
            // Use fully qualified types to avoid ambiguity with System.Drawing
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
            TextAnnotation txtAnn = new TextAnnotation(doc.Pages[1], rect)
            {
                Title    = "Note",
                Contents = "Extra annotation added to OFD‑derived PDF.",
                Color    = Aspose.Pdf.Color.Yellow
            };
            doc.Pages[1].Annotations.Add(txtAnn);

            // Export all annotations (including the one just added) to an XFDF file
            doc.ExportAnnotationsToXfdf(xfdfPath);

            // Save the modified document as PDF (OFD cannot be saved)
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Annotations exported to '{xfdfPath}'.");
        Console.WriteLine($"Modified document saved as PDF to '{outputPdfPath}'.");
    }
}