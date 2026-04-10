using System;
using System.IO;
using Aspose.Pdf; // Aspose.Pdf.Color, Document, Page

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";   // source PDF
        const string configPath    = "theme.txt";   // contains theme name
        const string outputPdfPath = "output.pdf";  // result PDF

        // Validate files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(configPath))
        {
            Console.Error.WriteLine($"Config file not found: {configPath}");
            return;
        }

        // Read theme name from configuration file
        string theme = File.ReadAllText(configPath).Trim();

        // Map theme to a background color (using Aspose.Pdf.Color)
        Color backgroundColor;
        switch (theme.ToLowerInvariant())
        {
            case "light":
                backgroundColor = Color.White;
                break;
            case "dark":
                backgroundColor = Color.Gray;
                break;
            case "sepia":
                backgroundColor = Color.PapayaWhip;
                break;
            default:
                // Fallback to white if theme is unknown
                backgroundColor = Color.White;
                break;
        }

        // Load the PDF, set each page's background, and save
        using (Document doc = new Document(inputPdfPath)) // document-disposal-with-using
        {
            // Pages collection is 1‑based (page-indexing-one-based)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                doc.Pages[i].Background = backgroundColor; // Page.Background property
            }

            doc.Save(outputPdfPath); // save as PDF (no extra SaveOptions needed)
        }

        Console.WriteLine($"PDF saved with '{theme}' theme background to '{outputPdfPath}'.");
    }
}