using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // for TextStamp

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document
        Document pdfDocument = new Document(inputPath);

        // Prepare the header stamp – display the file name of the PDF
        string fileName = Path.GetFileName(inputPath);
        TextStamp headerStamp = new TextStamp(fileName)
        {
            // Position the stamp at the top of the page (header)
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Top,
            YIndent = 20f // distance from the top edge
        };
        // Configure the visual appearance of the stamp
        headerStamp.TextState.Font = FontRepository.FindFont("Helvetica");
        headerStamp.TextState.FontSize = 12;
        headerStamp.TextState.ForegroundColor = Aspose.Pdf.Color.Black;

        // Add the stamp to every page in the document
        foreach (Page page in pdfDocument.Pages)
        {
            page.AddStamp(headerStamp);
        }

        // Save the stamped PDF
        pdfDocument.Save(outputPath);

        Console.WriteLine($"Stamped PDF saved to '{outputPath}'.");
    }
}
