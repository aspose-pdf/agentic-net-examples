using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string outputPath = "AcroFormWithMargins.pdf";

        // Create a new PDF document with a single page
        using (Document doc = new Document())
        {
            // Add a page (1‑based indexing)
            Page page = doc.Pages.Add();

            // Set page size (A4) and margins (in points)
            page.PageInfo.Width = 595f;   // 8.27 inches * 72
            page.PageInfo.Height = 842f;  // 11.69 inches * 72

            // Define margins (left, right, top, bottom)
            float leftMargin   = 50f;
            float rightMargin  = 50f;
            float topMargin    = 50f;
            float bottomMargin = 50f;
            page.PageInfo.Margin = new MarginInfo(leftMargin, rightMargin, topMargin, bottomMargin);

            // Initialize FormEditor on the created document
            using (FormEditor formEditor = new FormEditor(doc))
            {
                // Example: add a text field positioned relative to the margins
                // Field rectangle: lower‑left (llx, lly), upper‑right (urx, ury)
                float fieldWidth  = 200f;
                float fieldHeight = 20f;

                // Position the field 10 points right of the left margin and 30 points above the bottom margin
                float llx = leftMargin + 10f;
                float lly = bottomMargin + 30f;
                float urx = llx + fieldWidth;
                float ury = lly + fieldHeight;

                // Add the text field on page 1
                formEditor.AddField(FieldType.Text, "CustomerName", 1, llx, lly, urx, ury);

                // Add a second field (e.g., date) positioned below the first one
                float secondLly = ury + 15f; // 15 points gap
                float secondUry = secondLly + fieldHeight;
                formEditor.AddField(FieldType.Text, "InvoiceDate", 1, llx, secondLly, urx, secondUry);

                // Save the modified document directly via FormEditor
                formEditor.Save(outputPath);
            }
        }

        Console.WriteLine($"PDF with AcroForm fields saved to '{outputPath}'.");
    }
}