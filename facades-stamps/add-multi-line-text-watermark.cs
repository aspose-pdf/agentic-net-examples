using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text; // for EncodingType

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "watermarked.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Initialize PdfFileMend with the loaded document
            using (PdfFileMend mend = new PdfFileMend(doc))
            {
                // Create FormattedText with the first line of the watermark
                // Note: FormattedText constructor expects System.Drawing.Color for the text color
                FormattedText ft = new FormattedText(
                    "First line of watermark",               // text
                    System.Drawing.Color.Gray,               // text color
                    "Helvetica",                             // font name
                    EncodingType.Winansi,                    // encoding
                    false,                                   // embed font flag
                    48);                                     // font size

                // Add additional lines using AddNewLineText
                ft.AddNewLineText("Second line of watermark");
                ft.AddNewLineText("Third line of watermark");

                // Enable word wrap if needed
                mend.IsWordWrap = true;

                // Prepare an array with all page numbers (1‑based indexing)
                int pageCount = doc.Pages.Count;
                int[] pages = new int[pageCount];
                for (int i = 0; i < pageCount; i++)
                    pages[i] = i + 1;

                // Define the rectangle covering the whole page
                float llx = 0;
                float lly = 0;
                float urx = (float)doc.Pages[1].PageInfo.Width;
                float ury = (float)doc.Pages[1].PageInfo.Height;

                // Add the multi‑line formatted text as a watermark to all pages
                mend.AddText(ft, pages, llx, lly, urx, ury);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
    }
}