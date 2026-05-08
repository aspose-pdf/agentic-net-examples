using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string fieldName  = "Country";
        const string targetItem = "United States";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // Retrieve the form field by name
            var field = doc.Form[fieldName];
            if (field == null)
            {
                Console.Error.WriteLine($"Field '{fieldName}' not found.");
                return;
            }

            // Determine field type (ComboBox or ListBox) and set the selected item
            if (field is ComboBoxField comboBox)
            {
                SetSelectedItem(comboBox, targetItem);
            }
            else if (field is ListBoxField listBox)
            {
                SetSelectedItem(listBox, targetItem);
            }
            else
            {
                Console.Error.WriteLine($"Field '{fieldName}' is not a dropdown (ComboBox/ListBox).");
                return;
            }

            doc.Save(outputPath);
        }

        Console.WriteLine($"Default selection set to '{targetItem}' and saved to '{outputPath}'.");
    }

    // Helper method for ComboBoxField
    private static void SetSelectedItem(ComboBoxField comboBox, string itemName)
    {
        // Options collection holds the available items
        for (int i = 0; i < comboBox.Options.Count; i++)
        {
            // Option.Name contains the display text
            if (string.Equals(comboBox.Options[i].Name, itemName, StringComparison.OrdinalIgnoreCase))
            {
                // Selected property is zero‑based for ComboBoxField
                comboBox.Selected = i;
                return;
            }
        }

        Console.Error.WriteLine($"Item '{itemName}' not found in ComboBox '{comboBox.PartialName}'.");
    }

    // Helper method for ListBoxField
    private static void SetSelectedItem(ListBoxField listBox, string itemName)
    {
        // Options collection holds the available items
        for (int i = 0; i < listBox.Options.Count; i++)
        {
            if (string.Equals(listBox.Options[i].Name, itemName, StringComparison.OrdinalIgnoreCase))
            {
                // Selected property is 1‑based for ListBoxField
                listBox.Selected = i + 1;
                return;
            }
        }

        Console.Error.WriteLine($"Item '{itemName}' not found in ListBox '{listBox.PartialName}'.");
    }
}