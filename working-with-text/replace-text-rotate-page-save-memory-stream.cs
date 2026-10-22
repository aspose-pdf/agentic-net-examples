using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

namespace AsposePdfSample
{
    class Program
    {
        static void Main()
        {
            const string inputPath   = "input.pdf";
            const string searchText  = "Hello";
            const string replaceText = "Hi";

            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine($"File not found: {inputPath}");
                return;
            }

            // Load the PDF document with deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // ----- Text replacement -----
                // Find all occurrences of the target text
                TextFragmentAbsorber absorber = new TextFragmentAbsorber(searchText);
                doc.Pages.Accept(absorber);
                // Replace each found fragment
                foreach (TextFragment fragment in absorber.TextFragments)
                {
                    fragment.Text = replaceText;
                }

                // ----- Page rotation -----
                // Aspose.Pdf uses 1‑based page indexing
                for (int i = 1; i <= doc.Pages.Count; i++)
                {
                    Page page = doc.Pages[i];
                    page.Rotate = Rotation.on90; // rotate 90° clockwise
                }

                // ----- Save to MemoryStream -----
                using (MemoryStream ms = new MemoryStream())
                {
                    // Saving to a stream defaults to PDF format
                    doc.Save(ms);
                    ms.Position = 0; // reset for downstream consumers

                    Console.WriteLine($"Modified PDF saved to memory stream (size: {ms.Length} bytes).");
                    // The MemoryStream 'ms' now contains the updated PDF.
                }
            }
        }
    }
}
