using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Drawing;

class AddCreationDateStamp
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
        Document doc = new Document(inputPath);

        // Use the document's creation date if available; otherwise fall back to current date
        DateTime creationDate = doc.Info.CreationDate != DateTime.MinValue ? doc.Info.CreationDate : DateTime.Now;
        string dateText = creationDate.ToString("yyyy-MM-dd");

        // Create a TextStamp that will be placed in the top‑left corner
        TextStamp dateStamp = new TextStamp(dateText)
        {
            HorizontalAlignment = HorizontalAlignment.Left,
            VerticalAlignment   = VerticalAlignment.Top,
            XIndent = 20f,   // left margin
            YIndent = 20f    // top margin
        };
        // TextState is read‑only; modify its members directly
        dateStamp.TextState.Font = FontRepository.FindFont("Helvetica");
        dateStamp.TextState.FontSize = 12;
        dateStamp.TextState.ForegroundColor = Color.Black;

        // Add the stamp to every page of the document
        foreach (Page page in doc.Pages)
        {
            page.AddStamp(dateStamp);
        }

        // Save the modified PDF
        doc.Save(outputPath);
        Console.WriteLine($"Creation‑date stamp added. Output saved to '{outputPath}'.");
    }
}
