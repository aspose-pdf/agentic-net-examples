using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        using (Document doc = new Document())
        {
            // Add a new page (first page)
            Page page = doc.Pages.Add();

            // Create a header footer object
            HeaderFooter header = new HeaderFooter();

            // Configure margin for the header using MarginInfo
            MarginInfo margin = new MarginInfo();
            header.Margin = margin;

            // Create a text fragment for the header content
            TextFragment headerText = new TextFragment("Sample Header");
            headerText.TextState.FontSize = 12;
            headerText.TextState.Font = FontRepository.FindFont("Arial");

            // Add the text fragment to the header's paragraph collection
            header.Paragraphs.Add(headerText);

            // Assign the header to the first page
            page.Header = header;

            // Save the PDF document
            doc.Save("output.pdf");
        }
    }
}