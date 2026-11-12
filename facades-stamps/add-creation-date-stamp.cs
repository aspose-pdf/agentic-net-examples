using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "stamped_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF to read its creation date (Document must be disposed via using)
        string creationDateText;
        using (Document doc = new Document(inputPath))
        {
            // If the creation date is not set, use current date as fallback
            DateTime creationDate = doc.Info.CreationDate != DateTime.MinValue
                                    ? doc.Info.CreationDate
                                    : DateTime.Now;
            creationDateText = creationDate.ToString("yyyy-MM-dd");
        }

        // Create a text stamp with the formatted creation date
        TextStamp dateStamp = new TextStamp(creationDateText)
        {
            // Position at top‑left corner of the page
            HorizontalAlignment = HorizontalAlignment.Left,
            VerticalAlignment = VerticalAlignment.Top,
            // Optional offsets from the page edges
            XIndent = 10,
            YIndent = 10,
            // Appearance settings
            Background = false,
            Opacity = 0.8
        };
        // TextState is read‑only; modify the existing instance instead of assigning a new one
        dateStamp.TextState.ForegroundColor = Color.Black;
        dateStamp.TextState.FontSize = 12;
        // Optional: set a specific font if desired
        // dateStamp.TextState.Font = FontRepository.FindFont("Helvetica");

        // Load the document again, apply the stamp to every page, and save the result
        using (Document pdfDoc = new Document(inputPath))
        {
            foreach (Page page in pdfDoc.Pages)
            {
                page.AddStamp(dateStamp);
            }
            pdfDoc.Save(outputPath);
        }

        Console.WriteLine($"Stamp added. Output saved to '{outputPath}'.");
    }
}
