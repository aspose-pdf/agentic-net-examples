using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "MultiPage.pdf";

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add the first page and place text on it
            Page page1 = doc.Pages.Add();
            TextFragment tf1 = new TextFragment("First page text");
            page1.Paragraphs.Add(tf1);

            // Add the second page and place text on it
            Page page2 = doc.Pages.Add();
            TextFragment tf2 = new TextFragment("Second page text");
            page2.Paragraphs.Add(tf2);

            // Save the multi‑page PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
