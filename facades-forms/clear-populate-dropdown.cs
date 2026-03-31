using System;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string fieldName = "CountryList";

        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Access the form fields collection directly from the document
            Form form = doc.Form;

            // Retrieve the dropdown (list box) field as a ChoiceField
            ChoiceField dropdown = form[fieldName] as ChoiceField;
            if (dropdown == null)
            {
                Console.Error.WriteLine($"Field '{fieldName}' not found or is not a dropdown.");
                return;
            }

            // Remove all existing options safely (iterate backwards)
            for (int i = dropdown.Options.Count - 1; i >= 0; i--)
            {
                // Each entry in Options is an Option object, not a string.
                // Use the Option.Value (or Option.Display) when deleting.
                var opt = dropdown.Options[i];
                dropdown.DeleteOption(opt.Value);
            }

            // New items to populate the dropdown
            string[] newItems = new string[] { "USA", "Canada", "Mexico" };
            foreach (string item in newItems)
            {
                dropdown.AddOption(item);
            }

            // Save the modified PDF
            doc.Save(outputPath);
            Console.WriteLine($"Dropdown '{fieldName}' cleared and repopulated. Saved to '{outputPath}'.");
        }
    }
}
