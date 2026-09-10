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

        // Load the PDF document
        Document doc = new Document(inputPath);
        // Get the last page
        Page lastPage = doc.Pages[doc.Pages.Count];

        // Create a text stamp with the current date (MM-dd-yyyy)
        TextStamp stamp = new TextStamp(DateTime.Now.ToString("MM-dd-yyyy"));
        stamp.TextState.Font = FontRepository.FindFont("Helvetica");
        stamp.TextState.FontSize = 12;
        // Use Aspose.Pdf.Color to avoid ambiguity with System.Drawing.Color
        stamp.TextState.ForegroundColor = Aspose.Pdf.Color.Black;
        stamp.HorizontalAlignment = HorizontalAlignment.Center;
        stamp.VerticalAlignment = VerticalAlignment.Bottom;
        stamp.YIndent = 10; // distance from the bottom edge

        // Add the stamp to the last page only
        lastPage.AddStamp(stamp);

        // Save the modified PDF
        doc.Save(outputPath);
        // Document does not have a Close() method; dispose if desired
        doc.Dispose();

        Console.WriteLine($"Footer added to last page: {outputPath}");
    }
}