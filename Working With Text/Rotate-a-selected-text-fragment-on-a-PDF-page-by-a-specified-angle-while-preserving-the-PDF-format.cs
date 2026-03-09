using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class RotateTextFragmentExample
{
    static void Main()
    {
        // Input PDF, output PDF, text to rotate and rotation angle (in degrees)
        const string inputPath  = "input.pdf";
        const string outputPath = "rotated_output.pdf";
        const string searchText = "Sample Text";   // text to find and rotate
        const float  angleDeg   = 45f;             // rotation angle (only 0,90,180,270 are supported by TextStamp.Rotate)

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block (ensures proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages (Aspose.Pdf uses 1‑based indexing)
            for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
            {
                Page page = doc.Pages[pageNum];

                // Find occurrences of the target text on the current page
                TextFragmentAbsorber absorber = new TextFragmentAbsorber(searchText);
                page.Accept(absorber);

                // Process each found fragment
                foreach (TextFragment fragment in absorber.TextFragments)
                {
                    // Get the bounding rectangle of the fragment
                    Aspose.Pdf.Rectangle rect = fragment.Rectangle;

                    // Remove the original text within the rectangle
                    absorber.RemoveAllText(page, rect);

                    // Create a TextStamp with the same text content
                    TextStamp stamp = new TextStamp(fragment.Text)
                    {
                        // Position the stamp at the lower‑left corner of the original rectangle
                        XIndent = rect.LLX,
                        YIndent = rect.LLY
                    };

                    // Apply rotation – TextStamp only supports multiples of 90° via the Rotate property.
                    // For other angles you would need to work with a transformation matrix, which is beyond this example.
                    stamp.Rotate = GetRotation(angleDeg);

                    // Copy visual appearance from the original fragment (only properties that exist in the current API)
                    stamp.TextState.Font = fragment.TextState.Font;
                    stamp.TextState.FontSize = fragment.TextState.FontSize;
                    stamp.TextState.ForegroundColor = fragment.TextState.ForegroundColor;
                    stamp.TextState.BackgroundColor = fragment.TextState.BackgroundColor;
                    stamp.TextState.Underline = fragment.TextState.Underline;
                    stamp.TextState.FontStyle = fragment.TextState.FontStyle;

                    // Add the rotated stamp to the page
                    page.AddStamp(stamp);
                }
            }

            // Save the modified document (PDF format)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Rotated text saved to '{outputPath}'.");
    }

    /// <summary>
    /// Maps an arbitrary angle (in degrees) to the nearest supported Rotation enum value.
    /// TextStamp.Rotate only supports 0, 90, 180 and 270 degrees.
    /// </summary>
    private static Rotation GetRotation(float angleDeg)
    {
        // Normalize angle to [0,360)
        float normalized = ((angleDeg % 360) + 360) % 360;
        if (Math.Abs(normalized - 90) < 0.01) return Rotation.on90;
        if (Math.Abs(normalized - 180) < 0.01) return Rotation.on180;
        if (Math.Abs(normalized - 270) < 0.01) return Rotation.on270;
        // Default – no rotation (or you could throw if unsupported)
        return Rotation.None;
    }
}
