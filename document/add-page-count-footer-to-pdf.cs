using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            int totalPages = doc.Pages.Count; // total page count

            // Pages are 1‑based in Aspose.Pdf
            for (int i = 1; i <= totalPages; i++)
            {
                Page page = doc.Pages[i];

                // Create a footer stamp showing "Page X of Y"
                TextStamp footer = new TextStamp($"Page {i} of {totalPages}");
                footer.HorizontalAlignment = HorizontalAlignment.Center;
                footer.BottomMargin = 10;
                // Configure the text appearance via the existing TextState instance
                footer.TextState.FontSize = 10;
                footer.TextState.ForegroundColor = Color.Gray;

                // Add the footer stamp to the current page
                page.AddStamp(footer);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Custom footer added. Saved to '{outputPath}'.");
    }
}
