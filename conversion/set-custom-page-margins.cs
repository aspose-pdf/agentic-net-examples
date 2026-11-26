using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Text; // required for TextFragment

class Program
{
    static void Main()
    {
        // Define custom margins (in points) for the page.
        // 1 inch = 72 points. Example: 1 inch margins on all sides.
        const double leftMargin   = 72;   // 1 inch
        const double rightMargin  = 72;   // 1 inch
        const double topMargin    = 72;   // 1 inch
        const double bottomMargin = 72;   // 1 inch

        // Create a new PDF document.
        using (Document doc = new Document())
        {
            // Add a page.
            Page page = doc.Pages.Add();

            // Set the page margins. MarginInfo constructor order is (left, bottom, right, top).
            page.PageInfo.Margin = new MarginInfo(leftMargin, bottomMargin, rightMargin, topMargin);

            // Add a simple paragraph to demonstrate the margins.
            page.Paragraphs.Add(new TextFragment("Sample page with custom margins."));

            // Save the resulting PDF to disk.
            const string outputPath = "CustomMargins.pdf";
            doc.Save(outputPath);
            Console.WriteLine($"PDF created with custom margins: {outputPath}");
        }
    }
}
