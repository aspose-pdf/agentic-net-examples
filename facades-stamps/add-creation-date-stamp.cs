using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class AddCreationDateStamp
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

        // Load the source PDF (use using for deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // Retrieve the document creation date and format it as yyyy-MM-dd
            string creationDate = doc.Info.CreationDate.ToString("yyyy-MM-dd");

            // Create a TextStamp for the header
            TextStamp stamp = new TextStamp(creationDate)
            {
                TextState = {
                    Font = FontRepository.FindFont("Helvetica"),
                    FontSize = 12,
                    ForegroundColor = Aspose.Pdf.Color.Black
                },
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                XIndent = 0,
                YIndent = 20 // points from the top edge
            };

            // Add the stamp to every page
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(stamp);
            }

            // Save the modified document
            doc.Save(outputPath);

            Console.WriteLine($"Creation date stamp added. Output saved to '{outputPath}'.");
        }
    }
}
