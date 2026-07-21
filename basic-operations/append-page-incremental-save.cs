using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Open the PDF with read/write access so that incremental updates are possible.
        // The FileStream must stay open for the lifetime of the Document.
        using (FileStream stream = new FileStream(pdfPath, FileMode.Open, FileAccess.ReadWrite))
        using (Document doc = new Document(stream))
        {
            // Append a new blank page at the end.
            Page newPage = doc.Pages.Add();

            // Optionally add some content to the new page.
            TextFragment tf = new TextFragment("Appended page via incremental update.")
            {
                // Position the text near the top of the page.
                Position = new Position(50, newPage.PageInfo.Height - 50)
            };
            newPage.Paragraphs.Add(tf);

            // Save incrementally. This writes only the changes (the new page) to the same file.
            doc.Save(); // Parameterless Save triggers incremental update mode.
        }

        Console.WriteLine("PDF updated with incremental save.");
    }
}