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
        const string text       = "Hello, Aspose.Pdf!";
        const double x = 100; // X coordinate (points, from left)
        const double y = 500; // Y coordinate (points, from bottom)

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF; using ensures proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a TextFragment containing the desired string
            TextFragment tf = new TextFragment(text);

            // Position the fragment at the specified coordinates
            tf.Position = new Position(x, y);

            // Add the fragment to the first page's paragraph collection
            doc.Pages[1].Paragraphs.Add(tf);

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Text added and saved to '{outputPath}'.");
    }
}