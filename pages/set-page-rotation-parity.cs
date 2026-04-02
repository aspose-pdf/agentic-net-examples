using System;
using Aspose.Pdf;

public class Program
{
    public static void Main()
    {
        // Create a sample PDF with a few pages
        using (Document sampleDoc = new Document())
        {
            for (int i = 0; i < 4; i++)
            {
                sampleDoc.Pages.Add();
            }
            sampleDoc.Save("input.pdf");
        }

        // Load the PDF and set rotation based on page‑number parity
        using (Document doc = new Document("input.pdf"))
        {
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];
                if (i % 2 == 1) // odd pages
                {
                    page.Rotate = Rotation.on90; // correct enum member
                }
                else // even pages
                {
                    page.Rotate = Rotation.None;
                }
            }
            doc.Save("output.pdf");
        }
    }
}