using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "rotated_stamp.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // Create a text stamp with the desired content
            TextStamp stamp = new TextStamp("CONFIDENTIAL");
            // Place the stamp (coordinates are in points)
            stamp.XIndent = 100; // horizontal offset from the left edge
            stamp.YIndent = 400; // vertical offset from the bottom edge
            // Rotate the stamp by 45 degrees for diagonal placement
            stamp.RotateAngle = 45.0;
            // Configure visual appearance of the stamp text
            stamp.TextState.Font = FontRepository.FindFont("Helvetica");
            stamp.TextState.FontSize = 36;
            stamp.TextState.FontStyle = FontStyles.Bold;
            stamp.TextState.ForegroundColor = Aspose.Pdf.Color.FromRgb(1.0, 0.0, 0.0); // red
            // Add the stamp to the first page (1‑based indexing)
            doc.Pages[1].AddStamp(stamp);

            // Save the modified document as PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Saved rotated stamp PDF to '{outputPath}'.");
    }
}