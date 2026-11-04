using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

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

        // Open the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Assume the collapsible section is controlled by a button field named "CollapsibleSection"
            // Retrieve the field from the form collection
            var collapsibleField = doc.Form["CollapsibleSection"] as ButtonField;

            if (collapsibleField != null)
            {
                // Set the field's hidden flag so the associated content starts collapsed
                // Use AnnotationFlags.Hidden (from Aspose.Pdf.Annotations) for the flag value
                collapsibleField.Flags |= AnnotationFlags.Hidden;
            }
            else
            {
                Console.Error.WriteLine("CollapsibleSection field not found or not a ButtonField.");
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with initial collapsed state to '{outputPath}'.");
    }
}
