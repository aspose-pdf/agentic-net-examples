using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_progress.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has a form (creates one if missing)
            Form form = doc.Form;

            // Define the rectangle where the progress bar (as a text box) will be placed
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle progressRect = new Aspose.Pdf.Rectangle(50, 750, 300, 770);

            // Create a TextBoxField on the first page at the defined rectangle
            TextBoxField progressField = new TextBoxField(doc.Pages[1], progressRect)
            {
                // Set a unique name for the field
                Name = "ProgressBar",
                // Initial value (0%)
                Value = "0%"
            };

            // Add the field to the form (using the overload that specifies the page number)
            form.Add(progressField, 1);

            // Optionally, add a visual appearance (border, background) to make it look like a bar
            // Here we use AddFieldAppearance to ensure the field is rendered on the page
            form.AddFieldAppearance(progressField, 1, progressRect);

            // Simulate user completing sections of the form
            // For demonstration, assume there are 5 sections
            int totalSections = 5;
            for (int completed = 1; completed <= totalSections; completed++)
            {
                // Calculate progress percentage
                int percent = (completed * 100) / totalSections;

                // Update the field's value to reflect current progress
                progressField.Value = $"{percent}%";

                // In a real scenario, you might save after each update or trigger a UI refresh.
                // Here we simply continue the loop.
            }

            // Save the modified PDF (Document.Save without SaveOptions writes PDF)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with progress bar saved to '{outputPath}'.");
    }
}