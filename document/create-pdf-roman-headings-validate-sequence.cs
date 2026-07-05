using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "RomanHeadings.pdf";

        // ---------- Create PDF with Roman numeral headings ----------
        using (Document doc = new Document())
        {
            // Add a single page
            Page page = doc.Pages.Add();

            // Headings to be added (Roman numerals)
            string[] headings = {
                "I. Introduction",
                "II. Methodology",
                "III. Results",
                "IV. Discussion",
                "V. Conclusion"
            };

            // Starting vertical position (top of the page)
            double yPos = 800;

            foreach (string heading in headings)
            {
                // Create a text fragment for the heading
                TextFragment tf = new TextFragment(heading);
                tf.TextState.Font = FontRepository.FindFont("Helvetica");
                tf.TextState.FontSize = 24;
                tf.TextState.FontStyle = FontStyles.Bold;
                tf.Position = new Position(50, yPos); // left margin 50, current y position

                // Add the fragment to the page
                page.Paragraphs.Add(tf);

                // Move down for the next heading
                yPos -= 40;
            }

            // Save the document
            doc.Save(outputPath);
        }

        // ---------- Validate the Roman numeral sequence ----------
        using (Document doc = new Document(outputPath))
        {
            // Extract all text from the document
            TextAbsorber absorber = new TextAbsorber();
            doc.Pages.Accept(absorber);
            string extractedText = absorber.Text;

            // Split extracted text into lines
            string[] lines = extractedText.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            // Expected Roman numeral prefixes in order
            string[] expectedPrefixes = { "I.", "II.", "III.", "IV.", "V." };

            bool sequenceIsValid = true;

            for (int i = 0; i < expectedPrefixes.Length && i < lines.Length; i++)
            {
                if (!lines[i].StartsWith(expectedPrefixes[i]))
                {
                    sequenceIsValid = false;
                    Console.WriteLine($"Mismatch at line {i + 1}: expected prefix '{expectedPrefixes[i]}' but found '{lines[i]}'");
                }
            }

            Console.WriteLine(sequenceIsValid
                ? "Roman numeral headings are in the correct order."
                : "Heading sequence validation failed.");
        }
    }
}