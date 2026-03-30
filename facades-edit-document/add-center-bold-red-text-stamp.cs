using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
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

        using (Document doc = new Document(inputPath))
        {
            // Define text appearance: bold, red, size 24
            TextState textState = new TextState();
            textState.Font = FontRepository.FindFont("Helvetica");
            textState.FontSize = 24;
            textState.FontStyle = FontStyles.Bold;
            textState.ForegroundColor = Aspose.Pdf.Color.Red;

            // Create the text stamp with the defined appearance
            TextStamp stamp = new TextStamp("Sample Text", textState);
            stamp.HorizontalAlignment = HorizontalAlignment.Center;

            // Apply the stamp to page 5 (1‑based indexing)
            Page page = doc.Pages[5];
            page.AddStamp(stamp);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Text stamp added and saved to '{outputPath}'.");
    }
}