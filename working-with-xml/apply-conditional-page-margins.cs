using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputHtml = "input.html";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputHtml))
        {
            Console.Error.WriteLine($"File not found: {inputHtml}");
            return;
        }

        // Load the HTML (margin‑area usage mode is not required for this example)
        HtmlLoadOptions loadOptions = new HtmlLoadOptions();

        // Use a using block for deterministic disposal (lifecycle rule)
        using (Document doc = new Document(inputHtml, loadOptions))
        {
            // Apply distinct margins per page based on page number (section logic)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Define margin values (in points)
                // First 5 pages: larger margins (50 points)
                // Remaining pages: smaller margins (20 points)
                double marginValue = i <= 5 ? 50 : 20;

                // Assemble a MarginInfo object with the required values
                // NOTE: In modern Aspose.PDF the properties are Top, Bottom, Left, Right
                MarginInfo marginInfo = new MarginInfo
                {
                    Top = marginValue,
                    Bottom = marginValue,
                    Left = marginValue,
                    Right = marginValue
                };

                // Assign the margin to the current page
                page.PageInfo.Margin = marginInfo;
            }

            // Save the document as PDF (basic save rule)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved with conditional margins to '{outputPdf}'.");
    }
}
