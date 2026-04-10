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

        // Load the PDF document inside a using block (Document implements IDisposable)
        using (Document doc = new Document(inputPath))
        {
            // Retrieve the last page
            Page lastPage = doc.Pages[doc.Pages.Count];

            // Build the footer text (current date in MM-dd-yyyy format)
            string dateText = DateTime.Now.ToString("MM-dd-yyyy");
            TextStamp footerStamp = new TextStamp(dateText);

            // Style the stamp
            footerStamp.TextState.Font = FontRepository.FindFont("Helvetica");
            footerStamp.TextState.FontSize = 12;
            footerStamp.TextState.ForegroundColor = Aspose.Pdf.Color.Black;
            footerStamp.HorizontalAlignment = HorizontalAlignment.Center;
            footerStamp.VerticalAlignment = VerticalAlignment.Bottom;
            footerStamp.YIndent = 20f; // distance from the bottom edge

            // Apply the stamp only to the last page
            lastPage.AddStamp(footerStamp);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Footer stamp added to last page: {outputPath}");
    }
}
