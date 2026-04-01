using System;
using Aspose.Pdf;

public class ResizePagesToA5
{
    public static void Main()
    {
        // Create a new PDF document (or load an existing one)
        using (Document doc = new Document())
        {
            // Add sample pages (optional – remove if loading an existing PDF)
            doc.Pages.Add();
            doc.Pages.Add();
            doc.Pages.Add();

            // Loop through all pages and resize each to A5 size
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];
                page.Resize(PageSize.A5);
            }

            // Save the modified document
            doc.Save("output.pdf");
        }
    }
}