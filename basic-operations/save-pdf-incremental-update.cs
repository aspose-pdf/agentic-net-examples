using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_incremental.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Copy the original file so we can preserve it and write incrementally to a new file
        File.Copy(inputPath, outputPath, true);

        // Load the copied file; using ensures proper disposal
        using (Document doc = new Document(outputPath))
        {
            // Append a new page (pages are 1‑based)
            Page newPage = doc.Pages.Add();

            // Add simple text to the new page
            TextFragment tf = new TextFragment("Appended page");
            tf.TextState.FontSize = 14;
            tf.TextState.ForegroundColor = Aspose.Pdf.Color.Blue;
            newPage.Paragraphs.Add(tf);

            // Save incrementally – parameterless Save() writes using incremental update technique
            doc.Save();

            // Optional: confirm that the document now has incremental updates
            Console.WriteLine("Has incremental update: " + doc.HasIncrementalUpdate());
        }

        Console.WriteLine($"PDF saved incrementally to '{outputPath}'.");
    }
}