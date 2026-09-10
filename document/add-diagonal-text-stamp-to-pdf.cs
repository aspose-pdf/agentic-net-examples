using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string message    = "CONFIDENTIAL";

        // Ensure the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a TextStamp that will display the custom message
            TextStamp stamp = new TextStamp(message);

            // Configure visual appearance of the stamp
            stamp.TextState.Font = FontRepository.FindFont("Helvetica");
            stamp.TextState.FontSize = 72;                                   // Large font size
            stamp.TextState.ForegroundColor = Aspose.Pdf.Color.Red;          // Text color
            stamp.HorizontalAlignment = HorizontalAlignment.Center;          // Center horizontally
            stamp.VerticalAlignment   = VerticalAlignment.Center;            // Center vertically
            stamp.RotateAngle = -45;                                          // Diagonal across the page
            stamp.Opacity = 0.3f;                                             // Semi‑transparent

            // Apply the stamp to every page in the document
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(stamp);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Stamped PDF saved to '{outputPath}'.");
    }
}