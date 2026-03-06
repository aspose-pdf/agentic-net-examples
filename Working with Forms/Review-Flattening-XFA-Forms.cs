using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms; // Provides access to the core Form class

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "flattened.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Check if the document contains an XFA form
            if (doc.Form.HasXfa)
            {
                // Create flatten settings (nested class inside Form)
                var flattenSettings = new Form.FlattenSettings
                {
                    // Regenerate appearances before flattening (optional)
                    UpdateAppearances = true,
                    // Do not hide buttons; set to true if you want them removed
                    HideButtons = false,
                    // Do not apply redactions; set to true if needed
                    ApplyRedactions = false,
                    // Call JavaScript events during flattening (default true)
                    CallEvents = true
                };

                // Flatten the XFA form using the specified settings
                doc.Flatten(flattenSettings);
            }
            else
            {
                // No XFA present – simple flattening is sufficient
                doc.Flatten();
            }

            // Save the flattened PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Flattened PDF saved to '{outputPath}'.");
    }
}