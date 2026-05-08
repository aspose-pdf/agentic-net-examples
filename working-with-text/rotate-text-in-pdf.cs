using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "rotated_text.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the existing PDF document
        using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document(inputPath))
        {
            // Add a new page (or use an existing one)
            Aspose.Pdf.Page page = doc.Pages.Add();

            // Create a text fragment with the desired content
            Aspose.Pdf.Text.TextFragment textFragment = new Aspose.Pdf.Text.TextFragment("Rotated Text");

            // Set the position where the text will be placed
            textFragment.Position = new Aspose.Pdf.Text.Position(200, 400);

            // Configure font and size
            textFragment.TextState.Font = Aspose.Pdf.Text.FontRepository.FindFont("Helvetica");
            textFragment.TextState.FontSize = 24;

            // Rotate the text by 45 degrees using the Rotation property of TextState
            textFragment.TextState.Rotation = 45;

            // Append the text fragment to the page using TextBuilder
            Aspose.Pdf.Text.TextBuilder builder = new Aspose.Pdf.Text.TextBuilder(page);
            builder.AppendText(textFragment);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Document saved to '{outputPath}'.");
    }
}