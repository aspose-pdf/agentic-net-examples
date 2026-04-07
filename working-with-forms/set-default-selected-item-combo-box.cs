using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";      // PDF containing the "Country" combo box
        const string outputPdf = "output.pdf";     // PDF with the default selection set

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Bind the PDF to a FormEditor (Facades API)
        using (FormEditor editor = new FormEditor())
        {
            editor.BindPdf(inputPdf);

            // Retrieve the combo box field named "Country"
            ComboBoxField countryField = editor.Document.Form["Country"] as ComboBoxField;
            if (countryField == null)
            {
                Console.Error.WriteLine("Combo box field 'Country' not found.");
                return;
            }

            // Find the index (1‑based) of the option "United States"
            int selectedIndex = 0;
            int currentIndex = 1; // Options are 1‑based for the Selected property
            foreach (var option in countryField.Options)
            {
                // Each option is a ChoiceOption; its display text is in the Name property
                if (option.Name.Equals("United States", StringComparison.OrdinalIgnoreCase))
                {
                    selectedIndex = currentIndex;
                    break;
                }
                currentIndex++;
            }

            if (selectedIndex == 0)
            {
                Console.Error.WriteLine("Option 'United States' not found in the combo box.");
                return;
            }

            // Set the default selected item
            countryField.Selected = selectedIndex;

            // Save the modified PDF
            editor.Save(outputPdf);
        }

        Console.WriteLine($"Default selection set and saved to '{outputPdf}'.");
    }
}