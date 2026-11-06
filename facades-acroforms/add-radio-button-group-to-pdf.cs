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

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF document
        Document doc = new Document(inputPath);

        // Get the first page (Aspose.Pdf pages are 1‑based)
        Page page = doc.Pages[1];

        // Create a radio button group on the page. The constructor accepts only a Page (or Document).
        RadioButtonField radioGroup = new RadioButtonField(page)
        {
            PartialName = "MyRadioGroup"
        };

        // Add three options to the group. Use AddOption – the overload without a rectangle lets Aspose place the widgets automatically.
        radioGroup.AddOption("Option1");
        radioGroup.AddOption("Option2");
        radioGroup.AddOption("Option3");

        // Set the default selected option programmatically. "Selected" is 1‑based, so 2 selects "Option2".
        radioGroup.Selected = 2;

        // Add the radio button field to the document's form (page index is 1‑based).
        doc.Form.Add(radioGroup, 1);

        // Save the modified PDF
        doc.Save(outputPath);

        Console.WriteLine($"Radio button group added and saved to '{outputPath}'.");
    }
}
