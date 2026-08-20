using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Annotations;

class DynamicHeaderFooter
{
    static void Main()
    {
        using (Document doc = new Document())
        {
            // NOTE: In evaluation mode Aspose.PDF allows a maximum of 4 elements in collections
            // (pages, annotations, etc.). Therefore we create only 4 pages here.
            for (int i = 0; i < 4; i++)
            {
                Page page = doc.Pages.Add();

                // Header
                HeaderFooter header = new HeaderFooter();
                header.Paragraphs.Add(new TextFragment($"Header – Page {page.Number}"));
                page.Header = header;

                // Footer
                HeaderFooter footer = new HeaderFooter();
                footer.Paragraphs.Add(new TextFragment(
                    $"Footer – {DateTime.Now:yyyy-MM-dd} – Page {page.Number}"));
                page.Footer = footer;

                // JavaScript that runs each time the page is opened.
                // This demonstrates a dynamic action per‑page.
                page.Actions.OnOpen = new JavascriptAction($"app.alert('Page opened: {page.Number}');");
            }

            // Save the PDF.
            doc.Save("DynamicHeaderFooter.pdf");
        }

        Console.WriteLine("PDF with dynamic header/footer created successfully.");
    }
}
