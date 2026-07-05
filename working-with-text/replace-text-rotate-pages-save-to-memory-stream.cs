using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (wrapped in using for deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // ---------- Text replacement ----------
            // Find all occurrences of the target text
            const string searchText  = "Hello";
            const string replaceText = "Hi";

            TextFragmentAbsorber absorber = new TextFragmentAbsorber(searchText);
            // Search the whole document
            doc.Pages.Accept(absorber);

            // Replace each found fragment
            foreach (TextFragment fragment in absorber.TextFragments)
            {
                fragment.Text = replaceText;
            }

            // ---------- Page rotation ----------
            // Rotate every page 90 degrees clockwise using the correct Rotate property
            foreach (Page page in doc.Pages)
            {
                page.Rotate = Rotation.on90; // Correct property and enum value
            }

            // ---------- Save to a memory stream ----------
            using (MemoryStream memory = new MemoryStream())
            {
                // Store the modified PDF into the stream
                doc.Save(memory);

                // Example usage: report the size of the generated PDF
                Console.WriteLine($"Modified PDF saved to memory stream ({memory.Length} bytes).");

                // Reset position if the stream will be read later
                memory.Position = 0;

                // (Optional) Write the stream to a file for verification
                File.WriteAllBytes("output_from_stream.pdf", memory.ToArray());
            }
        }
    }
}
