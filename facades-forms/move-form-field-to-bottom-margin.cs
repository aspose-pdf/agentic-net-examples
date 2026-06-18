using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Facades; // Facades namespace included as requested

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string fieldName = "FooterNote";

        // Desired margins (points). Adjust as needed.
        const float bottomMargin = 20f;   // distance from bottom edge
        const float leftMargin = 50f;     // distance from left edge
        const float rightMargin = 50f;    // distance from right edge
        const float fieldHeight = 15f;    // approximate height of the field

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Retrieve the form field by name. The indexer returns a WidgetAnnotation, so cast to Field.
            Field field = doc.Form[fieldName] as Field;
            if (field == null)
            {
                Console.Error.WriteLine($"Field '{fieldName}' not found in the document.");
                return;
            }

            // Iterate through all pages (1‑based indexing)
            for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
            {
                Page page = doc.Pages[pageNum];
                // PageInfo.Width is double – cast to float for our calculations.
                float pageWidth = (float)page.PageInfo.Width;

                // Calculate rectangle positioned in the bottom margin
                float llx = leftMargin;                         // lower‑left X
                float lly = bottomMargin;                       // lower‑left Y
                float urx = pageWidth - rightMargin;            // upper‑right X
                float ury = bottomMargin + fieldHeight;         // upper‑right Y

                // Fully qualified rectangle to avoid ambiguity (Aspose.Pdf.Rectangle expects double values).
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(llx, lly, urx, ury);

                // Set the field appearance on the current page
                doc.Form.AddFieldAppearance(field, pageNum, rect);
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Field '{fieldName}' moved to bottom margin and saved as '{outputPath}'.");
    }
}
