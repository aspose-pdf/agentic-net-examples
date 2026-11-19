using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // TextStamp lives here

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string headerXml = "header.xml"; // contains plain text for header
        const string footerXml = "footer.xml"; // contains plain text for footer
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))  { Console.Error.WriteLine($"Input PDF not found: {inputPdf}");  return; }
        if (!File.Exists(headerXml)) { Console.Error.WriteLine($"Header XML not found: {headerXml}"); return; }
        if (!File.Exists(footerXml)) { Console.Error.WriteLine($"Footer XML not found: {footerXml}"); return; }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(inputPdf))
            {
                // Read the XML files – in this simplified example they just contain the text to display
                string headerText = File.ReadAllText(headerXml);
                string footerText = File.ReadAllText(footerXml);

                // Apply a TextStamp for header and footer on every page
                for (int i = 1; i <= doc.Pages.Count; i++)
                {
                    Page page = doc.Pages[i];

                    // Header stamp – centered at the top of the page
                    var headerStamp = new TextStamp(headerText)
                    {
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment   = VerticalAlignment.Top,
                        TopMargin           = 10, // distance from the top edge
                        // You can customise font, size, color, etc. via TextState if needed
                    };
                    page.AddStamp(headerStamp);

                    // Footer stamp – centered at the bottom of the page
                    var footerStamp = new TextStamp(footerText)
                    {
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment   = VerticalAlignment.Bottom,
                        BottomMargin        = 10, // distance from the bottom edge
                    };
                    page.AddStamp(footerStamp);
                }

                // Save the modified PDF
                doc.Save(outputPdf);
            }

            Console.WriteLine($"PDF with header/footer saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
