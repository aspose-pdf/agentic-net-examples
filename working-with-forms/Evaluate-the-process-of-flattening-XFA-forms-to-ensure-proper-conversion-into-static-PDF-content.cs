using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "flattened_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Determine if the document contains an XFA form
            if (doc.Form.HasXfa)
            {
                Console.WriteLine("Document contains XFA form.");

                // If the document does not require rendering, ignore the flag
                if (!doc.Form.NeedsRendering)
                {
                    doc.Form.IgnoreNeedsRendering = true;
                }

                // Configure flattening settings
                var flattenSettings = new Form.FlattenSettings
                {
                    ApplyRedactions = false,   // Do not apply redaction annotations
                    CallEvents = true,         // Execute JavaScript events (default)
                    HideButtons = false,       // Keep button appearances
                    UpdateAppearances = true   // Regenerate field appearances for accuracy
                };

                // Flatten the form using the custom settings
                doc.Flatten(flattenSettings);
                Console.WriteLine("Form flattened with custom settings.");
            }
            else
            {
                Console.WriteLine("Document does not contain XFA form. Performing standard flatten.");
                // Simple flatten without custom settings
                doc.Flatten();
            }

            // Save the resulting static PDF
            doc.Save(outputPath);
            Console.WriteLine($"Flattened PDF saved to '{outputPath}'.");
        }
    }
}