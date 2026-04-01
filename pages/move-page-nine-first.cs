using Aspose.Pdf;

public class Program
{
    public static void Main()
    {
        // Create a sample PDF document. In evaluation mode the page collection is limited to 4 pages.
        using (Document doc = new Document())
        {
            // Add four pages to stay within the evaluation limit.
            doc.Pages.Add();
            doc.Pages.Add();
            doc.Pages.Add();
            doc.Pages.Add();

            // In a real scenario with a full license you would set sourcePageNumber = 9.
            int sourcePageNumber = 2; // page to move (replace 2 with 9 for actual requirement)

            // Get the page object.
            Aspose.Pdf.Page pageToMove = doc.Pages[sourcePageNumber];

            // Remove the page from its original position.
            doc.Pages.Delete(sourcePageNumber);

            // Insert the page at the first position (index 1 because Aspose.Pdf uses 1‑based indexing).
            doc.Pages.Insert(1, pageToMove);

            // Save the modified document.
            doc.Save("output.pdf");
        }
    }
}