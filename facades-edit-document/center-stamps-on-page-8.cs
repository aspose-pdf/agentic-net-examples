using System;
using System.IO;
using Aspose.Pdf;                     // Document, Page
using Aspose.Pdf.Facades;            // PdfContentEditor, StampInfo

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_centered.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF to obtain page dimensions (must be disposed properly)
        using (Document doc = new Document(inputPath))
        {
            // Verify that page 8 exists (Aspose.Pdf uses 1‑based indexing)
            if (doc.Pages.Count < 8)
            {
                Console.Error.WriteLine("The document contains fewer than 8 pages.");
                return;
            }

            // Retrieve size of page 8
            Page targetPage = doc.Pages[8];
            double pageWidth  = targetPage.PageInfo.Width;
            double pageHeight = targetPage.PageInfo.Height;

            // Use PdfContentEditor to manipulate existing stamps
            using (PdfContentEditor editor = new PdfContentEditor())
            {
                // Bind the source PDF
                editor.BindPdf(inputPath);

                // Get all stamps on page 8
                StampInfo[] stamps = editor.GetStamps(8);
                if (stamps == null || stamps.Length == 0)
                {
                    Console.WriteLine("No stamps found on page 8.");
                }
                else
                {
                    // Center each stamp on the page
                    for (int i = 0; i < stamps.Length; i++)
                    {
                        // Stamp index is 1‑based
                        int stampIndex = i + 1;

                        // Desired coordinates (center of the page)
                        double x = pageWidth  / 2.0;
                        double y = pageHeight / 2.0;

                        // Move the stamp to the calculated position
                        editor.MoveStamp(8, stampIndex, x, y);
                    }
                }

                // Save the modified PDF (output path)
                editor.Save(outputPath);
            }
        }

        Console.WriteLine($"Stamp repositioned and saved to '{outputPath}'.");
    }
}