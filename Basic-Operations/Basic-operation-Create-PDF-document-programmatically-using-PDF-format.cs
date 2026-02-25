using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Output PDF file path
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "output.pdf");

        // Create a new PDF document
        var pdfDocument = new Aspose.Pdf.Document();

        // Add a page to the document
        Aspose.Pdf.Page page = pdfDocument.Pages.Add();

        // Create a text fragment with the desired content
        var textFragment = new Aspose.Pdf.Text.TextFragment("Hello, Aspose.PDF!");

        // Set the position of the text on the page (X = 100, Y = 700)
        textFragment.Position = new Aspose.Pdf.Text.Position(100, 700);

        // Optional: set font size
        textFragment.TextState.FontSize = 12;

        // Append the text fragment to the page using TextBuilder
        var textBuilder = new Aspose.Pdf.Text.TextBuilder(page);
        textBuilder.AppendText(textFragment);

        // Save the PDF document to the specified file
        pdfDocument.Save(outputPath);

        Console.WriteLine($"PDF created successfully at: {outputPath}");
    }
}