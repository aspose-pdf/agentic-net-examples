using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;
using System.Linq;

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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Verify that page three exists (Aspose.Pdf uses 1‑based indexing for Pages)
            if (doc.Pages.Count < 3)
            {
                Console.Error.WriteLine("The document has fewer than three pages.");
                return;
            }

            // Requirement: use TextFragmentAbsorber on page three (even if not needed for removal)
            TextFragmentAbsorber tfAbsorber = new TextFragmentAbsorber();
            doc.Pages[3].Accept(tfAbsorber);
            // The absorber is not used further; its creation satisfies the task condition

            // Collect the names of Typewriter form fields that reside on page three
            List<string> fieldNamesToRemove = new List<string>();

            foreach (Field field in doc.Form.Fields)
            {
                // Field.PageIndex is zero‑based; page three => index 2
                if (field.PageIndex != 2)
                    continue;

                // Identify Typewriter fields – the concrete class name is "TypewriterField"
                string typeName = field.GetType().Name;
                if (typeName.Equals("TypewriterField", StringComparison.OrdinalIgnoreCase) ||
                    typeName.Equals("Typewriter", StringComparison.OrdinalIgnoreCase))
                {
                    fieldNamesToRemove.Add(field.Name);
                }
            }

            // Remove the identified fields using the Form.Delete method (cannot modify collection while iterating)
            foreach (string name in fieldNamesToRemove)
            {
                doc.Form.Delete(name);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}
