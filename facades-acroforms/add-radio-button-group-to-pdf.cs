using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms; // <-- required for form field classes

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "radio_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // Get the first page (or any page you want the radio button to appear on)
            Page page = doc.Pages[1];

            // Create a radio button field attached to the page
            RadioButtonField radio = new RadioButtonField(page);
            // Set the field's partial name (identifier)
            radio.PartialName = "SampleRadioGroup";

            // Add three options to the radio button group
            radio.AddOption("Option1");
            radio.AddOption("Option2");
            radio.AddOption("Option3");

            // Set the default selected option (1‑based index). Here we select the second option.
            radio.Selected = 2;

            // Add the radio button field to the document's form collection (position 1 on the page)
            doc.Form.Add(radio, 1);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Radio button group added and saved to '{outputPath}'.");
    }
}
