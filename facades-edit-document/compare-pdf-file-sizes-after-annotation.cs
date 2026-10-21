using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string originalPath = "original.pdf";
        const string editedPath   = "edited.pdf";

        if (!File.Exists(originalPath))
        {
            Console.Error.WriteLine($"Original file not found: {originalPath}");
            return;
        }

        // Load the PDF with the high‑level Document API (PdfContentEditor does not expose AddText)
        Document pdf = new Document(originalPath);

        // Create a text fragment to act as a stamp
        TextFragment tf = new TextFragment("Edited");
        tf.TextState.FontSize = 24;
        tf.TextState.ForegroundColor = Aspose.Pdf.Color.Red; // cross‑platform color
        // Position is the lower‑left corner of the fragment
        tf.Position = new Position(100, 500);

        // Add the fragment to the first page (pages are 1‑based)
        pdf.Pages[1].Paragraphs.Add(tf);

        // Save the edited document
        pdf.Save(editedPath);

        // Compare file sizes to verify that changes were applied
        long originalSize = new FileInfo(originalPath).Length;
        long editedSize   = new FileInfo(editedPath).Length;

        Console.WriteLine($"Original size: {originalSize} bytes");
        Console.WriteLine($"Edited size:   {editedSize} bytes");

        if (originalSize != editedSize)
            Console.WriteLine("File sizes differ – changes have been applied.");
        else
            Console.WriteLine("File sizes are identical – no changes detected.");
    }
}
