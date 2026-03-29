using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // Define text appearance: Arial, size 14, blue color
            TextState textState = new TextState();
            textState.Font = FontRepository.FindFont("Arial");
            textState.FontSize = 14;
            textState.ForegroundColor = Aspose.Pdf.Color.Blue;

            // Multiline stamp content
            string stampContent = "First line\nSecond line\nThird line";

            // Create the TextStamp with the content and the defined TextState
            TextStamp stamp = new TextStamp(stampContent, textState);

            // Position the stamp on the page (example coordinates)
            stamp.XIndent = 100;
            stamp.YIndent = 500;
            stamp.HorizontalAlignment = HorizontalAlignment.Center;
            stamp.VerticalAlignment = VerticalAlignment.Center;

            // Add the stamp to the first page of the document
            Page page = doc.Pages[1];
            page.AddStamp(stamp);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Text stamp added and saved to '{outputPath}'.");
    }
}