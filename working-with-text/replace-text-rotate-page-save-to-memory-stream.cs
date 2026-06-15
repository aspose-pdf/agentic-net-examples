using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        // Ensure the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Perform modifications inside a using block to guarantee disposal
        using (Document doc = new Document(inputPath))
        {
            // ---------- Text replacement ----------
            // Search for the text "OldValue" and replace it with "NewValue"
            TextFragmentAbsorber absorber = new TextFragmentAbsorber("OldValue");
            doc.Pages.Accept(absorber);
            foreach (TextFragment fragment in absorber.TextFragments)
            {
                fragment.Text = "NewValue";
            }

            // ---------- Page rotation ----------
            // Rotate the first page 90 degrees clockwise
            if (doc.Pages.Count >= 1)
            {
                Page firstPage = doc.Pages[1]; // 1‑based indexing
                firstPage.Rotate = Rotation.on90; // use the Rotation enum
            }

            // ---------- Save to memory stream ----------
            using (MemoryStream outputStream = new MemoryStream())
            {
                // Save the modified PDF into the stream (no SaveOptions needed)
                doc.Save(outputStream);

                // Reset stream position if it will be read later
                outputStream.Position = 0;

                // Example: write the stream to a file (optional)
                File.WriteAllBytes("modified_output.pdf", outputStream.ToArray());

                Console.WriteLine("PDF modified and saved to memory stream successfully.");
            }
        }
    }
}