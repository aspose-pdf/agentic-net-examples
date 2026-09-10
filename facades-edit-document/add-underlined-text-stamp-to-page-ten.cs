using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string stampText  = "UNDERLINED TEXT";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Bind the source PDF using the Facades API
        PdfFileStamp fileStamp = new PdfFileStamp();
        try
        {
            fileStamp.BindPdf(inputPath);

            // Access the underlying Document object
            Document doc = fileStamp.Document;

            // Ensure the document has at least 10 pages
            if (doc.Pages.Count < 10)
            {
                Console.Error.WriteLine("The document does not contain page 10.");
                return;
            }

            // Create a TextStamp with the desired text
            TextStamp textStamp = new TextStamp(stampText);

            // Underline decoration
            textStamp.TextState.Underline = true;

            // Yellow background behind the text
            textStamp.TextState.BackgroundColor = Aspose.Pdf.Color.Yellow;

            // Centered alignment (both horizontal and vertical)
            textStamp.HorizontalAlignment = HorizontalAlignment.Center;
            textStamp.VerticalAlignment   = VerticalAlignment.Center;

            // Optional: make the stamp appear on top of page content
            textStamp.Background = false;

            // Add the stamp to page 10 only
            Page pageTen = doc.Pages[10];
            pageTen.AddStamp(textStamp);

            // Save the modified PDF using the Facades API
            fileStamp.Save(outputPath);
        }
        finally
        {
            // Ensure resources are released
            fileStamp.Close();
        }

        Console.WriteLine($"Text stamp added to page 10 and saved as '{outputPath}'.");
    }
}