using System;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string outputPath = "output.pdf";

        // Create a new PDF document and ensure it is disposed properly
        using (Document doc = new Document())
        {
            // NOTE: In Aspose.PDF evaluation mode a collection can contain at most 4 elements.
            // Therefore we create only 4 pages. A full license removes this limitation.
            for (int i = 0; i < 4; i++)
            {
                // Add a new page and set its size to A4
                Page page = doc.Pages.Add();
                page.SetPageSize(PageSize.A4.Width, PageSize.A4.Height);
            }

            // Save the resulting PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with {4} A4 pages saved to '{outputPath}'.");
    }
}
