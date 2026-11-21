using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Paths for the source PDF and the stamped output
        const string inputPath  = "input.pdf";
        const string outputPath = "stamped_output.pdf";

        // Verify the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPath}");
            return;
        }

        // Load the PDF document
        Document doc = new Document(inputPath);

        // Build the header text – the file name of the source PDF
        string fileName = Path.GetFileName(inputPath);

        // Create a TextStamp that will act as the header
        TextStamp headerStamp = new TextStamp(fileName);
        headerStamp.TextState.Font = FontRepository.FindFont("Helvetica");
        headerStamp.TextState.FontSize = 12;
        headerStamp.TextState.ForegroundColor = Color.Black;
        headerStamp.HorizontalAlignment = HorizontalAlignment.Center;
        headerStamp.VerticalAlignment = VerticalAlignment.Top;
        headerStamp.YIndent = 20f; // optional offset from the top edge

        // Add the stamp to every page of the document
        foreach (Page page in doc.Pages)
        {
            page.AddStamp(headerStamp);
        }

        // Save the stamped PDF to the output path
        doc.Save(outputPath);

        Console.WriteLine($"Stamped PDF saved to '{outputPath}'.");
    }
}