using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal.
        using (Document doc = new Document(inputPdf))
        {
            // ---------- Extract total text ----------
            TextAbsorber absorber = new TextAbsorber();
            doc.Pages.Accept(absorber);
            int totalTextLength = absorber.Text?.Length ?? 0;

            // ---------- Count images and graphics ----------
            int totalImageCount = 0;
            int totalGraphicCount = 0; // XObject resources (forms, patterns, etc.) are treated as graphics.

            for (int i = 1; i <= doc.Pages.Count; i++) // 1‑based indexing
            {
                Page page = doc.Pages[i];

                // Images
                totalImageCount += page.Resources.Images?.Count ?? 0;

                // Graphics (XObject resources). In newer Aspose.Pdf versions the collection is named Forms.
                // Fall back to Forms if XObjects is not available.
                totalGraphicCount += page.Resources.Forms?.Count ?? 0;
            }

            // ---------- Output the summary ----------
            Console.WriteLine("PDF Summary Report");
            Console.WriteLine("-------------------");
            Console.WriteLine($"File: {Path.GetFileName(inputPdf)}");
            Console.WriteLine($"Total pages: {doc.Pages.Count}");
            Console.WriteLine($"Total text length (characters): {totalTextLength}");
            Console.WriteLine($"Total image count: {totalImageCount}");
            Console.WriteLine($"Total graphic (XObject/Form) count: {totalGraphicCount}");
        }
    }
}
