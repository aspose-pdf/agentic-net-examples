using System;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Create a new PDF document
        using (Document document = new Document())
        {
            // Add up to 4 pages (evaluation mode limit)
            for (int i = 1; i <= 4; i++)
            {
                Page page = document.Pages.Add();
                // Rotate even pages by 90 degrees, odd pages remain unrotated
                if (i % 2 == 0)
                {
                    page.Rotate = Rotation.on90; // correct enum value
                }
                else
                {
                    page.Rotate = Rotation.None;
                }
            }

            // Save the resulting PDF
            document.Save("rotated_pages.pdf");
        }

        Console.WriteLine("PDF created with page rotations based on parity.");
    }
}
