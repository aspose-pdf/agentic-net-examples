using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Path for the output PDF
        const string outputPath = "custom_page.pdf";

        // Desired custom page dimensions (Width and Height in points)
        // 1 point = 1/72 inch. Example: 600pt width (~8.33in) and 800pt height (~11.11in)
        const double pageWidth = 600; // points
        const double pageHeight = 800; // points

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page with the custom dimensions
            Page page = doc.Pages.Add();
            page.PageInfo.Width = pageWidth;
            page.PageInfo.Height = pageHeight;

            // Add a simple paragraph to verify the page is created
            TextFragment tf = new TextFragment("This PDF uses custom page dimensions.");
            tf.TextState.FontSize = 14;
            page.Paragraphs.Add(tf);

            // Save the PDF with the custom page size
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF generated with custom dimensions: {outputPath}");
    }
}
