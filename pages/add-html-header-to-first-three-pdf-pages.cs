using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_header.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the source PDF
        using (Document doc = new Document(inputPath))
        {
            // Define the HTML header (embedded CSS styling)
            const string htmlHeader = @"
                <div style='font-family:Helvetica; font-size:14pt; color:#0066CC; text-align:center; margin-bottom:10pt;'>
                    <span style='font-weight:bold;'>Sample HTML Header</span>
                </div>";

            // Apply the header to the first three pages only
            for (int i = 1; i <= Math.Min(3, doc.Pages.Count); i++)
            {
                Page page = doc.Pages[i];

                // Create an HtmlFragment containing the styled header
                HtmlFragment headerFragment = new HtmlFragment(htmlHeader);

                // Insert the fragment at the beginning of the page content so it appears as a header
                page.Paragraphs.Insert(0, headerFragment);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with HTML header to '{outputPath}'.");
    }
}