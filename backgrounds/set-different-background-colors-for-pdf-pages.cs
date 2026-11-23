using System;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string outputPath = "output.pdf";

        // Ensure the Document is disposed properly
        using (var doc = new Document())
        {
            // Add the first page and set its background color via Page.Background
            var page1 = doc.Pages.Add();
            page1.Background = Color.LightGray; // same background for first page

            // Add the second page and set a different background color via Page.Background
            var page2 = doc.Pages.Add();
            page2.Background = Color.LightBlue; // different background for second page

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
