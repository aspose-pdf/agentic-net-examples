using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Facades; // included as requested

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Wrap Document in a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Ensure the document has at least two pages (Aspose.Pdf uses 1‑based indexing)
                if (doc.Pages.Count < 2)
                {
                    Console.Error.WriteLine("The PDF has fewer than 2 pages.");
                    return;
                }

                // Get page 2
                Page page2 = doc.Pages[2];

                // Define the rectangle where the checkbox will appear
                // (lower‑left X,Y, upper‑right X,Y) – fully qualified type to avoid ambiguity
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 700, 120, 720);

                // Create the checkbox field (class name is CheckboxField, not CheckBoxField)
                CheckboxField checkBox = new CheckboxField(page2, rect);
                checkBox.PartialName = "AgreeTerms";
                checkBox.Checked = false; // default unchecked state

                // Add the field to the document's form collection
                doc.Form.Add(checkBox);

                // Save the modified PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Checkbox field 'AgreeTerms' added on page 2. Saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}