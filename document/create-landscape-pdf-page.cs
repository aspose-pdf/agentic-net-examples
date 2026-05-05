using System;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string outputPath = "landscape.pdf";

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document pdf = new Document())
        {
            // Add a new page to the document
            Page page = pdf.Pages.Add();

            // Set the page size to A4 landscape (swap width/height)
            page.SetPageSize(PageSize.A4.Height, PageSize.A4.Width);

            // Alternatively, you can keep the portrait size and rotate the page content 90°.
            // Uncomment the line below if you prefer rotation over swapping dimensions.
            // page.Rotate = Rotation.on90;

            // Save the PDF to the specified file
            pdf.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to {outputPath}");
    }
}
