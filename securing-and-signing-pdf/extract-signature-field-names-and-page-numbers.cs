using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate over pages to obtain the page number for each signature field
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];
                foreach (Annotation annotation in page.Annotations)
                {
                    // SignatureField derives from WidgetAnnotation, so we can cast directly
                    if (annotation is SignatureField signature)
                    {
                        // SignatureField.Name gives the field name; pageIndex is the 1‑based page number
                        Console.WriteLine($"Signature field: '{signature.Name}' on page {pageIndex}");
                    }
                }
            }
        }
    }
}
