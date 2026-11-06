using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output_custom_flags.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document using the Document API (PdfAnnotationEditor does not expose GetAnnotations)
        Document doc = new Document(inputPdf);

        // Page numbers are 1‑based in Aspose.Pdf
        int pageNumber = 1;
        Page page = doc.Pages[pageNumber];

        if (page.Annotations == null || page.Annotations.Count == 0)
        {
            Console.WriteLine("No annotations found on the specified page.");
        }
        else
        {
            // Retrieve the first annotation (Annotations collection is also 1‑based)
            Annotation ann = page.Annotations[1];

            // Combine existing flags with standard custom flags (e.g., NoZoom and NoRotate)
            ann.Flags = ann.Flags | AnnotationFlags.NoZoom | AnnotationFlags.NoRotate;

            // Add a user‑defined flag by casting an integer value to AnnotationFlags
            const int CustomFlag = 0x8000; // example custom bit
            ann.Flags = (AnnotationFlags)((int)ann.Flags | CustomFlag);
        }

        // Save the modified PDF
        doc.Save(outputPdf);
        Console.WriteLine($"PDF saved with custom annotation flags to '{outputPdf}'.");
    }
}
