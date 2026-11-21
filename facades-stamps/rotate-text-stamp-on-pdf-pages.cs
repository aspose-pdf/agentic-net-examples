using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "stamped_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Extract original text for later verification
        string originalText;
        using (Document srcDoc = new Document(inputPath))
        {
            TextAbsorber absorber = new TextAbsorber();
            srcDoc.Pages.Accept(absorber);
            originalText = absorber.Text;
        }

        // Apply a 90‑degree rotated text stamp
        using (Document doc = new Document(inputPath))
        {
            // Create the stamp
            TextStamp stamp = new TextStamp("Sample Stamp");
            stamp.RotateAngle = 90f;                         // rotate 90 degrees
            stamp.HorizontalAlignment = HorizontalAlignment.Center;
            stamp.VerticalAlignment   = VerticalAlignment.Center;
            stamp.TextState.FontSize = 24;
            stamp.TextState.FontStyle = FontStyles.Bold;
            stamp.TextState.ForegroundColor = Aspose.Pdf.Color.Black;

            // Add the stamp to every page
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(stamp);
            }

            // Save the stamped PDF
            doc.Save(outputPath);
        }

        // Verify that the original text is still readable after stamping
        string postStampText;
        using (Document resultDoc = new Document(outputPath))
        {
            TextAbsorber verifier = new TextAbsorber();
            resultDoc.Pages.Accept(verifier);
            postStampText = verifier.Text;
        }

        bool textUnchanged = string.Equals(originalText, postStampText, StringComparison.Ordinal);
        Console.WriteLine($"Stamp applied with 90° rotation. Text unchanged: {textUnchanged}");
        Console.WriteLine($"Output saved to '{outputPath}'.");
    }
}