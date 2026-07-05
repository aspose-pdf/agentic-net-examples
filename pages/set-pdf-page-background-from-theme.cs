using System;
using System.IO;
using Aspose.Pdf; // Aspose.Pdf namespace contains Document, Page, Color, etc.

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";   // source PDF
        const string outputPdf = "output.pdf";  // destination PDF
        const string configPath = "theme.config"; // simple config file containing the theme name

        // Validate input files
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(configPath))
        {
            Console.Error.WriteLine($"Config file not found: {configPath}");
            return;
        }

        // Read the theme name (first line of the config file)
        string theme = File.ReadAllText(configPath).Trim();

        // Map theme names to background colors
        Aspose.Pdf.Color bgColor;
        switch (theme.ToLowerInvariant())
        {
            case "dark":
                bgColor = Aspose.Pdf.Color.Black;
                break;
            case "light":
                bgColor = Aspose.Pdf.Color.White;
                break;
            case "blue":
                bgColor = Aspose.Pdf.Color.LightBlue;
                break;
            default:
                // Fallback color if the theme is unknown
                bgColor = Aspose.Pdf.Color.LightGray;
                break;
        }

        // Load the PDF, set each page's background, and save
        using (Document doc = new Document(inputPdf)) // document-disposal-with-using rule
        {
            // Pages are 1‑based (page-indexing-one-based rule)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                doc.Pages[i].Background = bgColor; // Page.Background property
            }

            doc.Save(outputPdf); // save as PDF (no SaveOptions needed for PDF)
        }

        Console.WriteLine($"PDF saved with '{theme}' theme background to '{outputPdf}'.");
    }
}