using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Drawing;

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

        // Load the PDF document inside a using block (document-disposal-with-using rule)
        using (Document doc = new Document(inputPath))
        {
            // Ensure there is at least one page (page-indexing-one-based rule)
            Page page = doc.Pages[1];

            // ---------- TextBoxField ----------
            // Create a text box field, set its rectangle and value
            TextBoxField txtField = new TextBoxField(page, new Aspose.Pdf.Rectangle(100, 600, 300, 650));
            txtField.Value = "Sample text";
            doc.Form.Add(txtField);

            // ---------- CheckboxField ----------
            // Create a checkbox field, set its rectangle
            CheckboxField chkField = new CheckboxField(page, new Aspose.Pdf.Rectangle(100, 500, 120, 520));
            // Set the checkbox value using one of the allowed states (checks the box)
            if (chkField.AllowedStates.Count > 0)
                chkField.Value = chkField.AllowedStates[0];
            else
                chkField.Checked = true; // fallback if no states are defined
            doc.Form.Add(chkField);

            // ---------- RadioButtonField ----------
            // Create a radio button field (no rectangle needed for the group itself)
            RadioButtonField radField = new RadioButtonField(page);
            // Add two options with their own rectangles
            radField.AddOption("Option A", new Aspose.Pdf.Rectangle(100, 400, 120, 420));
            radField.AddOption("Option B", new Aspose.Pdf.Rectangle(100, 380, 120, 400));
            // Select the first option by setting the Value property
            radField.Value = "Option A";
            doc.Form.Add(radField);

            // Save the modified PDF (basic PDF save, no SaveOptions needed)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}