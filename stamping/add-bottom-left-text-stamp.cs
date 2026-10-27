using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Create a text stamp with the desired content
                TextStamp stamp = new TextStamp("Your Text Here");

                // Align to bottom‑left corner
                stamp.HorizontalAlignment = HorizontalAlignment.Left;
                stamp.VerticalAlignment   = VerticalAlignment.Bottom;

                // Apply a 10‑point margin from the left and bottom edges
                stamp.LeftMargin   = 10;
                stamp.BottomMargin = 10;

                // Optionally customize appearance (font size, color, etc.)
                // stamp.TextState.FontSize = 12;
                // stamp.TextState.ForegroundColor = Aspose.Pdf.Color.Black;

                // Add the stamp to every page in the document
                foreach (Page page in doc.Pages)
                {
                    page.AddStamp(stamp);
                }

                // Save the modified PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Text stamp applied and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}