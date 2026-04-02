using System;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Create a sample PDF with one page
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // Set an initial BleedBox (e.g., 10 points inset from MediaBox)
            // MediaBox default size is 612x792 points (A4). Width = 592, Height = 782.
            Rectangle initialBleed = new Rectangle(10f, 10f, 592f, 782f);
            page.BleedBox = initialBleed;

            // Save the sample PDF
            doc.Save("sample.pdf");
        }

        // Open the PDF and adjust BleedBox for printer specifications (e.g., add 5 points margin)
        using (Document doc = new Document("sample.pdf"))
        {
            Page page = doc.Pages[1];

            // Retrieve current BleedBox
            Rectangle currentBleed = page.BleedBox;

            // Output current BleedBox values
            Console.WriteLine("Current BleedBox:");
            Console.WriteLine("LLX: " + currentBleed.LLX);
            Console.WriteLine("LLY: " + currentBleed.LLY);
            Console.WriteLine("URX: " + currentBleed.URX);
            Console.WriteLine("URY: " + currentBleed.URY);

            // Define adjustment (add 5 points on each side)
            float adjust = 5f;

            // Create new BleedBox with adjusted values
            Rectangle adjustedBleed = new Rectangle(
                (float)currentBleed.LLX - adjust,
                (float)currentBleed.LLY - adjust,
                (float)(currentBleed.URX - currentBleed.LLX) + 2f * adjust,
                (float)(currentBleed.URY - currentBleed.LLY) + 2f * adjust);

            // Apply adjusted BleedBox
            page.BleedBox = adjustedBleed;

            // Save the updated PDF
            doc.Save("output.pdf");
        }
    }
}