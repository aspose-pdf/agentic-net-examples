using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the document to obtain page count (required for iteration)
        using (Document doc = new Document(inputPdf))
        {
            // FormEditor works in the Facades API and allows copying a field to a new location.
            // The constructor takes the source PDF and the destination PDF paths.
            using (FormEditor formEditor = new FormEditor(inputPdf, outputPdf))
            {
                int pageCount = doc.Pages.Count;

                // Define a bottom margin (in points). 1 inch = 72 points.
                const float bottomMargin = 20f; // 20 points from the bottom edge

                // Iterate over each page and copy the existing field "FooterNote"
                // to the calculated bottom‑margin coordinates.
                for (int pageNum = 1; pageNum <= pageCount; pageNum++)
                {
                    // X coordinate (abscissa) – left edge (0). Adjust as needed.
                    float x = 0f;

                    // Y coordinate (ordinate) – bottom margin.
                    float y = bottomMargin;

                    // CopyInnerField copies the field to the same page at the specified position.
                    // Passing null for newFieldName lets the API generate a unique name.
                    formEditor.CopyInnerField("FooterNote", null, pageNum, x, y);
                }

                // Persist the changes to the output PDF.
                formEditor.Save();
            }
        }

        Console.WriteLine($"Field \"FooterNote\" moved to bottom margin on all pages. Output saved to '{outputPdf}'.");
    }
}