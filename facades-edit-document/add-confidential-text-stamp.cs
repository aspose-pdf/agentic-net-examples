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

        using (Document doc = new Document(inputPath))
        {
            // Define text appearance: red font
            TextState textState = new TextState();
            textState.Font = FontRepository.FindFont("Helvetica");
            textState.FontSize = 48;
            textState.ForegroundColor = Aspose.Pdf.Color.Red;

            // Create the text stamp with the desired value and appearance
            TextStamp stamp = new TextStamp("Confidential", textState);
            stamp.Opacity = 0.5f;      // semi‑transparent
            stamp.Background = true;   // place behind page content

            // Apply the stamp to every page in the document
            foreach (Page page in doc.Pages)
            {
                stamp.Put(page);
            }

            doc.Save(outputPath);
        }

        Console.WriteLine($"Stamp applied and saved to '{outputPath}'.");
    }
}
