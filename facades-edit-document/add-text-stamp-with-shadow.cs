using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Facades;   // Facades namespace is included as requested

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_text_stamp.pdf";
        const string stampText  = "Sample Stamp";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block (lifecycle rule)
        using (Document doc = new Document(inputPath))
        {
            // Get the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // Determine a Y position near the top of the page
            // Page height is obtained from PageInfo.Height
            double pageHeight = page.PageInfo.Height;
            double topMargin   = 30;               // distance from the top edge
            double xPos        = 100;              // horizontal position
            double yPosShadow  = pageHeight - topMargin - 2; // shadow offset downwards
            double yPosMain    = pageHeight - topMargin;     // main text position

            // ---------- Shadow stamp ----------
            TextStamp shadowStamp = new TextStamp(stampText);
            // Configure text appearance
            shadowStamp.TextState.Font = FontRepository.FindFont("Helvetica");
            shadowStamp.TextState.FontSize = 12;
            shadowStamp.TextState.ForegroundColor = Color.Gray; // shadow color
            // Position the shadow (slightly offset)
            shadowStamp.XIndent = xPos + 2;   // shift right for shadow effect
            shadowStamp.YIndent = yPosShadow;
            // Ensure the stamp is drawn on top (default Background = false)
            shadowStamp.Background = false;
            // Add the shadow stamp to the page
            page.AddStamp(shadowStamp);

            // ---------- Main stamp ----------
            TextStamp mainStamp = new TextStamp(stampText);
            mainStamp.TextState.Font = FontRepository.FindFont("Helvetica");
            mainStamp.TextState.FontSize = 12;
            mainStamp.TextState.ForegroundColor = Color.Black; // main text color
            mainStamp.XIndent = xPos;
            mainStamp.YIndent = yPosMain;
            mainStamp.Background = false;
            page.AddStamp(mainStamp);

            // Save the modified document (lifecycle rule)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Text stamp with shadow added. Output saved to '{outputPath}'.");
    }
}