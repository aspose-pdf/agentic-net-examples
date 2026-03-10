using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Input and output PDF paths
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Build dynamic content by concatenating strings with the + operator
            string firstName = "John";
            string lastName  = "Doe";
            string greeting  = "Hello, " + firstName + " " + lastName + "! Welcome to Aspose.Pdf.";

            // Create a TextFragment with the concatenated string
            TextFragment fragment = new TextFragment(greeting);
            // Position the text on the page (using Aspose.Pdf.Text.Position)
            fragment.Position = new Position(100, 700);
            // Optional styling
            fragment.TextState.FontSize = 12;
            fragment.TextState.Font = FontRepository.FindFont("Helvetica");
            fragment.TextState.ForegroundColor = Aspose.Pdf.Color.Blue;

            // Append the text to the first page using TextBuilder
            // Page indexing is 1‑based (rule: page-indexing-one-based)
            Page page = doc.Pages[1];
            TextBuilder builder = new TextBuilder(page);
            builder.AppendText(fragment);

            // Save the modified document (rule: document-disposal-with-using)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Document saved to '{outputPath}'.");
    }
}