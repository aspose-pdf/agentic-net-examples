using System;
using System.IO;
using Aspose.Pdf;

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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Get the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            try
            {
                // Attempt to set an invalid rotation (45 degrees) – this will throw
                ApplyRotation(page, 45);
                Console.WriteLine("Invalid rotation applied (unexpected).");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Caught invalid rotation value: {ex.Message}");
                // Apply a valid rotation instead (90 degrees)
                ApplyRotation(page, 90);
                Console.WriteLine("Applied valid rotation of 90 degrees.");
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Saved rotated PDF to '{outputPath}'.");
    }

    /// <summary>
    /// Applies a rotation to the specified page after validating the angle.
    /// Aspose.Pdf only supports 0, 90, 180, and 270 degrees via the Rotation enum.
    /// </summary>
    private static void ApplyRotation(Page page, int angle)
    {
        // Validate that the angle is one of the allowed values
        Rotation rotation = angle switch
        {
            0   => Rotation.None,   // Correct enum member for 0°
            90  => Rotation.on90,
            180 => Rotation.on180,
            270 => Rotation.on270,
            _   => throw new ArgumentException($"Rotation angle must be 0, 90, 180, or 270 degrees. Received: {angle}")
        };

        // Apply the rotation using the correct property
        page.Rotate = rotation;
    }
}
