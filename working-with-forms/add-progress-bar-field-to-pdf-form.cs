using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Annotations; // needed for Border
using Aspose.Pdf.Text;        // needed for alignment enums

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";          // Existing PDF with form fields
        const string outputPath = "output_with_progress.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the existing PDF inside a using block (lifecycle rule)
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has a form; if not, a new one will be created automatically
            Form form = doc.Form;

            // Define the rectangle where the progress bar will be placed (page coordinates)
            // Example: lower‑left (50, 750), upper‑right (550, 770) on page 1
            Aspose.Pdf.Rectangle progressRect = new Aspose.Pdf.Rectangle(50, 750, 550, 770);

            // Create a TextBoxField that will act as the visual progress bar
            TextBoxField progressBar = new TextBoxField(doc, progressRect)
            {
                // Field identifiers
                Name = "ProgressBar",
                PartialName = "ProgressBar",

                // Initial value (e.g., 0%)
                Value = "0%",

                // Visual styling
                Color = Color.LightGray, // Background of the field
                TextHorizontalAlignment = HorizontalAlignment.Center,
                TextVerticalAlignment = VerticalAlignment.Center,

                // Make the field read‑only so users cannot edit it directly
                ReadOnly = true
            };

            // Border must be set after the field instance is created because Border requires the parent annotation
            progressBar.Border = new Border(progressBar) { Width = 1 };

            // Add the field to the form (using the overload that specifies the page number)
            form.Add(progressBar, 1);

            // OPTIONAL: If you need a separate appearance (e.g., a filled bar) you can add it:
            // Here we add a second appearance that could be toggled via JavaScript later.
            // The rectangle for the filled part (initially zero width)
            Aspose.Pdf.Rectangle filledRect = new Aspose.Pdf.Rectangle(50, 750, 50, 770);
            form.AddFieldAppearance(progressBar, 1, filledRect);

            // Save the modified PDF (lifecycle rule)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with progress bar saved to '{outputPath}'.");
    }
}
