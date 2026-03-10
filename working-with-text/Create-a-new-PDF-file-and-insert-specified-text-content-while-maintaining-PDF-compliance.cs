using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Output PDF file path
        const string outputPath = "output.pdf";
        // Text to insert into the PDF
        const string content = "Hello, Aspose PDF!";

        // Ensure the output directory exists
        string outputDir = Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Create a new PDF document (wrapped in using for proper disposal)
        using (Document doc = new Document())
        {
            // Add a blank page to the document
            Page page = doc.Pages.Add();

            // Create a text fragment with the desired content
            TextFragment tf = new TextFragment(content);
            // Set the position of the text on the page (x, y)
            tf.Position = new Position(100, 700);
            // Configure text appearance
            tf.TextState.FontSize = 12;
            tf.TextState.Font = FontRepository.FindFont("Helvetica");
            tf.TextState.ForegroundColor = Aspose.Pdf.Color.Black;

            // Append the text fragment to the page using TextBuilder
            TextBuilder builder = new TextBuilder(page);
            builder.AppendText(tf);

            // Save the document as a PDF file
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF created at '{outputPath}'.");
    }
}