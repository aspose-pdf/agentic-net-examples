using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Create a simple PDF with one blank page if it doesn't exist
        if (!File.Exists(inputPath))
        {
            using (var tempDoc = new Document())
            {
                tempDoc.Pages.Add(); // add a blank page
                tempDoc.Save(inputPath);
            }
        }

        // Load the PDF document
        Document doc = new Document(inputPath);

        // Define the radio button group properties
        string fieldName = "Gender";
        string[] options = new[] { "Male", "Female", "Other" };
        int defaultSelectionIndex = 0; // 0‑based index, will be converted to 1‑based for Selected

        // Get the page where the field will be placed
        Page page = doc.Pages[1]; // first page (Aspose pages are 1‑based)

        // Create the radio button field (constructor takes a Page)
        RadioButtonField radio = new RadioButtonField(page)
        {
            PartialName = fieldName
        };

        // Add each option. Use the overload that accepts a Rectangle to position the button.
        // Here we place the options vertically with a small gap.
        float startX = 100f;
        float startY = 700f;
        float btnWidth = 20f;
        float btnHeight = 20f;
        float verticalGap = 30f;

        for (int i = 0; i < options.Length; i++)
        {
            Rectangle optRect = new Rectangle(
                startX,
                startY - i * verticalGap,
                startX + btnWidth,
                startY - i * verticalGap + btnHeight);

            // AddOption(string optionName, Rectangle rect) adds the visual button for the option.
            radio.AddOption(options[i], optRect);
        }

        // Set the default selected option. Selected is 1‑based.
        radio.Selected = defaultSelectionIndex + 1;

        // Add the completed field to the document's form collection.
        doc.Form.Add(radio, 1);

        // Save the modified PDF
        doc.Save(outputPath);

        Console.WriteLine($"Radio button group '{fieldName}' added. Output saved to '{outputPath}'.");
    }
}
