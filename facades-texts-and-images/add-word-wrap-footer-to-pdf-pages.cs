using System;
using System.IO;
using System.Linq;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

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

        // Bind the source PDF to the facade
        using (PdfFileMend mend = new PdfFileMend())
        {
            mend.BindPdf(inputPdf);

            // Enable word‑by‑word wrapping for AddText operations
            mend.IsWordWrap = true;

            // Create formatted text (uses System.Drawing.Color for the constructor)
            FormattedText footerText = new FormattedText(
                "This is a sample footer that will wrap word by word across the page width.",
                System.Drawing.Color.DarkGray,
                "Helvetica",
                EncodingType.Winansi,
                false,
                10); // font size

            // Determine page dimensions (assume all pages have the same size)
            // Use the first page as a reference
            using (Document tempDoc = new Document(inputPdf))
            {
                // PageInfo.Width and Height are double – cast to float for PdfFileMend.AddText
                float pageWidth  = (float)tempDoc.Pages[1].PageInfo.Width;
                float pageHeight = (float)tempDoc.Pages[1].PageInfo.Height;

                // Define footer rectangle (bottom margin of 20 units, left/right margins of 30 units)
                float leftMargin   = 30f;
                float rightMargin  = 30f;
                float bottomMargin = 20f;
                float footerHeight = 40f; // enough height for wrapped text

                float lowerLeftX = leftMargin;
                float lowerLeftY = bottomMargin;
                float upperRightX = pageWidth - rightMargin;
                float upperRightY = bottomMargin + footerHeight;

                // Build an array with all page numbers (1‑based indexing)
                int[] allPages = Enumerable.Range(1, tempDoc.Pages.Count).ToArray();

                // Add the formatted footer text to every page within the defined rectangle
                mend.AddText(footerText, allPages, lowerLeftX, lowerLeftY, upperRightX, upperRightY);
            }

            // Save the modified PDF
            mend.Save(outputPdf);
            mend.Close();
        }

        Console.WriteLine($"Footer added to all pages: {outputPdf}");
    }
}
