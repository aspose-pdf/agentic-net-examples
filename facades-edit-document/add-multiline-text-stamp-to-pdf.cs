using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "stamped_output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the source PDF (required for proper disposal)
        using (Document doc = new Document(inputPdf))
        {
            // Prepare text appearance
            TextState textState = new TextState
            {
                Font = FontRepository.FindFont("Arial"),
                FontSize = 14,
                ForegroundColor = Aspose.Pdf.Color.Blue
            };

            // Multiline content (use newline characters)
            string multilineText = "First line\nSecond line\nThird line";

            // Create a TextStamp with the content and the defined TextState
            TextStamp textStamp = new TextStamp(multilineText, textState)
            {
                // Optional: position the stamp (example: 100 points from left, 500 from bottom)
                XIndent = 100,
                YIndent = 500,
                // Ensure the stamp is drawn as text (default is true)
                Draw = true
            };

            // Add the stamp to every page of the document
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(textStamp);
            }

            // Save the stamped PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Stamped PDF saved to '{outputPdf}'.");
    }
}
