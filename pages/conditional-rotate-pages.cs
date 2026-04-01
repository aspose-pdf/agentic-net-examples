using System;
using Aspose.Pdf;

public class Program
{
    public static void Main()
    {
        // Create a new PDF document with sample pages
        using (Document document = new Document())
        {
            // Add up to 4 pages to stay within evaluation mode limits
            // (Aspose.PDF evaluation mode throws an exception when a collection exceeds 4 items)
            int pagesToAdd = 4; // Change to 5 or more after applying a valid license
            for (int pageIndex = 0; pageIndex < pagesToAdd; pageIndex++)
            {
                document.Pages.Add();
            }

            // Rotate every third page (page numbers are 1‑based)
            // In a 4‑page document only page 3 will be rotated
            for (int i = 1; i <= document.Pages.Count; i++)
            {
                if (i % 3 == 0)
                {
                    document.Pages[i].Rotate = Rotation.on90;
                }
            }

            // Save the result
            document.Save("rotated.pdf");
        }
    }
}