using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "TitlePage.pdf";

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a new page that will serve as the title page
            Page titlePage = doc.Pages.Add();

            // Set a light‑gray background via Page.Background (the correct property in recent Aspose.Pdf versions)
            titlePage.Background = Aspose.Pdf.Color.FromRgb(0.95f, 0.95f, 0.95f);

            // Create a text fragment for the title
            TextFragment title = new TextFragment("My Custom Title");

            // Custom font and size
            title.TextState.Font = FontRepository.FindFont("Times New Roman");
            title.TextState.FontSize = 36;

            // Custom text colour (blue shade)
            title.TextState.ForegroundColor = Aspose.Pdf.Color.FromRgb(0.2f, 0.4f, 0.8f);

            // Horizontal centering – supported directly on TextState
            title.TextState.HorizontalAlignment = HorizontalAlignment.Center;

            // Vertical centering – achieved by positioning the fragment at half the page height
            float pageHeight = (float)titlePage.PageInfo.Height; // explicit cast from double to float
            float yPos = (pageHeight - title.TextState.FontSize) / 2; // approximate centre
            title.Position = new Position(0, yPos);

            // Add the title fragment to the page's paragraph collection
            titlePage.Paragraphs.Add(title);

            // Save the document as a PDF file
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with title page saved to '{outputPath}'.");
    }
}
