using System;
using Aspose.Pdf;

public class Program
{
    public static void Main()
    {
        // Create a sample PDF with up to 4 pages (evaluation mode limit)
        using (Document doc = new Document())
        {
            // Add 4 pages to the document (Aspose.PDF evaluation mode allows a maximum of 4 elements in a collection)
            for (int pageIndex = 0; pageIndex < 4; pageIndex++)
            {
                doc.Pages.Add();
            }

            // Optional: save the original document
            doc.Save("sample.pdf");

            // Delete the last three pages using PageCollection.Delete on each index
            int originalCount = doc.Pages.Count; // should be 4
            // Delete from the highest index downwards to keep indices valid after each removal
            for (int i = originalCount; i > originalCount - 3; i--)
            {
                doc.Pages.Delete(i);
            }

            // Save the modified document (will contain only the first page)
            doc.Save("output.pdf");
        }
    }
}
