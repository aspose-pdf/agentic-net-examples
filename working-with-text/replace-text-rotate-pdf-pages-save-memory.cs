using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Input PDF path
        const string inputPath = "input.pdf";

        // Ensure the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // MemoryStream that will hold the modified PDF
        using (MemoryStream outputStream = new MemoryStream())
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // ---------- Text replacement ----------
                // Find all occurrences of the target text
                const string searchText = "old text";
                const string replaceText = "new text";

                TextFragmentAbsorber absorber = new TextFragmentAbsorber(searchText);
                // Apply the absorber to the whole document
                doc.Pages.Accept(absorber);

                // Replace each found fragment with the new text
                foreach (TextFragment fragment in absorber.TextFragments)
                {
                    fragment.Text = replaceText;
                }

                // ---------- Page rotation ----------
                // Rotate every page 90 degrees clockwise using the Rotation enum
                foreach (Page page in doc.Pages)
                {
                    page.Rotate = Rotation.on90; // correct enum usage
                }

                // ---------- Save to memory ----------
                // Store the modified PDF into the MemoryStream
                doc.Save(outputStream);
            }

            // Reset the stream position if the caller needs to read from it
            outputStream.Position = 0;

            // Example usage: write the stream to a file (optional)
            const string outputPath = "modified_output.pdf";
            using (FileStream file = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
            {
                outputStream.CopyTo(file);
            }

            Console.WriteLine($"Modified PDF saved to '{outputPath}' and also available in memory.");
        }
    }
}
